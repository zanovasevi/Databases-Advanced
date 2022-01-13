namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			StringBuilder sb = new StringBuilder();

			var games = JsonConvert.DeserializeObject<IEnumerable<GameImportModel>>(jsonString);

			foreach(var jsonGame in games)
            {
				if(!IsValid(jsonGame) || jsonGame.Tags.Count() == 0)
                {
					sb.AppendLine("Invalid Data");
					continue;
                }

				DateTime release;

				bool isReleaseValid = DateTime.TryParseExact(jsonGame.ReleaseDate,
					"yyyy-MM-dd", CultureInfo.InvariantCulture,
					DateTimeStyles.None, out release);

				var developer = context.Developers.FirstOrDefault(x => x.Name == jsonGame.Developer);
				if(developer == null)
                {
					developer = new Developer
					{
						Name = jsonGame.Developer
					};
                }

				var genre = context.Genres.FirstOrDefault(x => x.Name == jsonGame.Genre);
				if (genre == null)
				{
					genre = new Genre
					{
						Name = jsonGame.Genre
					};
				}

				var game = new Game
				{
					Name = jsonGame.Name,
					Price = jsonGame.Price,
					ReleaseDate = release,
					Developer = developer,
					Genre = genre,
				};

				foreach(var jsonTag in jsonGame.Tags)
                {
					var tag = context.Tags.FirstOrDefault(x => x.Name == jsonTag);
					if (tag == null)
					{
						tag = new Tag
						{
							Name = jsonTag
						};
					}
					game.GameTags.Add(new GameTag { Tag = tag });
				}

				context.Games.Add(game);
				context.SaveChanges();
				sb.AppendLine($"Added {jsonGame.Name} ({jsonGame.Genre}) with {jsonGame.Tags.Count()} tags");
            }

			return sb.ToString().TrimEnd();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			StringBuilder sb = new StringBuilder();

			var users = JsonConvert.DeserializeObject<IEnumerable<UserCardInputModel>>(jsonString);

			foreach(var userDto in users)
            {
				if(!IsValid(userDto) || !userDto.Cards.All(IsValid))
                {
					sb.AppendLine("Invalid Data");
					continue;
                }

				var user = new User
				{
					FullName = userDto.FullName,
					Username = userDto.Username,
					Email = userDto.Email,
					Age = userDto.Age,
					Cards = userDto.Cards.Select(x => new Card
					{
						Number = x.Number,
						Cvc = x.CVC,
						Type = Enum.Parse<CardType>(x.Type)
					})
					.ToArray()
				};

				context.Users.Add(user);
				context.SaveChanges();
				sb.AppendLine($"Imported {userDto.Username} with {userDto.Cards.Count()} cards");
			}
			return sb.ToString().TrimEnd();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			StringBuilder sb = new StringBuilder();

			var xmlSerializer = new XmlSerializer(typeof(PurchaseInputModel[]),
				new XmlRootAttribute("Purchases"));

			var purchaseDtos = xmlSerializer.Deserialize(new StringReader(xmlString)) as PurchaseInputModel[];

			foreach(var purchaseDto in purchaseDtos)
            {
				if(!IsValid(purchaseDto))
                {
					sb.AppendLine("Invalid Data");
					continue;
                }

                DateTime purchaseDate;
                var currentPurchase = DateTime.TryParseExact(purchaseDto.Date,
                    "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out purchaseDate);


                var purchase = new Purchase
				{
					Type = /*purchaseDto.Type.Value,*/Enum.Parse<PurchaseType>(purchaseDto.Type),
					ProductKey = purchaseDto.Key,
					Date = purchaseDate
				};

				purchase.Card = context.Cards.FirstOrDefault(x => x.Number == purchaseDto.Card);
				purchase.Game = context.Games.FirstOrDefault(x => x.Name == purchaseDto.Title);

				context.Purchases.Add(purchase);

				var username = context.Users.Where(x => x.Id == purchase.Card.UserId)
					.Select(x => x.Username).FirstOrDefault();
				sb.AppendLine($"Imported {purchaseDto.Title} for {username}");
            }
			context.SaveChanges();
			return sb.ToString().TrimEnd();
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}