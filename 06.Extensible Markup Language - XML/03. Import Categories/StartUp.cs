using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ProductShop.Data;
using ProductShop.Dtos.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            string userXml = File.ReadAllText("../../../Datasets/users.xml");
            string productXml = File.ReadAllText("../../../Datasets/products.xml");
            string categoryXml = File.ReadAllText("../../../Datasets/categories.xml");

            ImportUsers(context, userXml);
            ImportProducts(context, productXml);

            var result = ImportCategories(context, categoryXml);
            System.Console.WriteLine(result);
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(CategoryInputModel[]), new XmlRootAttribute("Categories"));

            var categoryDtos = xmlSerializer.Deserialize(new StringReader(inputXml)) as CategoryInputModel[];

            var categories = categoryDtos
                .Where(x => x.Name != null)
                .Select(x => new Category
                {
                    Name = x.Name,
                })
                .ToList();

            context.Categories.AddRange(categories);
            context.SaveChanges();

            return $"Successfully imported {categories.Count()}";
        }

        public static string ImportProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(ProductInputModel[]), new XmlRootAttribute("Products"));

            var productDtos = xmlSerializer.Deserialize(new StringReader(inputXml)) as ProductInputModel[];

            var products = productDtos
                .Select(x => new Product
                {
                    Name = x.Name,
                    Price = x.Price,
                    SellerId = x.SellerId,
                    BuyerId = x.BuyerId
                })
                .ToList();

            context.Products.AddRange(products);
            context.SaveChanges();

            return $"Successfully imported {products.Count()}";
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(UserInputModel[]), new XmlRootAttribute("Users"));

            var userDtos = xmlSerializer.Deserialize(new StringReader(inputXml)) as UserInputModel[];

            var users = userDtos
                .Select(x => new User
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age
                })
                .ToList();

            context.Users.AddRange(users);
            context.SaveChanges();

            return $"Successfully imported {users.Count()}";
        }
    }
}
