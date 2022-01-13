namespace BookShop
{
    using System;
    using System.Linq;
    using System.Text;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            //string command = Console.ReadLine();

            Console.WriteLine(GetGoldenBooks(db));
        }

        //public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        //{
        //    var ageRestriction = Enum.Parse<AgeRestriction>(command, true);

        //    var books = context.Books
        //        .Where(books => books.AgeRestriction == ageRestriction)
        //        .Select(book => book.Title)
        //        .OrderBy(x => x)
        //        .ToList();

        //    var result = string.Join(Environment.NewLine, books);

        //    return result;
        //}

        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Copies < 5000 && x.EditionType == EditionType.Gold)
                .Select(x => new
                {
                    x.BookId,
                    x.Title
                })
                .OrderBy(x => x.BookId)
                .ToList();

            var sb = new StringBuilder();

            foreach(var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
