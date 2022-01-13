using System.IO;
using System.Linq;
using System.Xml.Serialization;
using ProductShop.Data;
using ProductShop.IO.Input;
using ProductShop.Models;
using System;
using ProductShop.IO.Output;
using ProductShop.Converter;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            //string userXml = File.ReadAllText("../../../Datasets/users.xml");
            //string productXml = File.ReadAllText("../../../Datasets/products.xml");
            //string categoryXml = File.ReadAllText("../../../Datasets/categories.xml");
            //string categoryProductXml = File.ReadAllText("../../../Datasets/categories-products.xml");

            //ImportUsers(context, userXml);
            //ImportProducts(context, productXml);
            //ImportCategories(context, categoryXml);
            //ImportCategoryProducts(context, categoryProductXml);

            var result = GetUsersWithProducts(context);
            Console.WriteLine(result);
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .Select(u => new UserRootDTO
                {
                    Count = context.Users.Count(),
                    Users = context.Users.Select(u => new UserProductOutputModel
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Age = u.Age,
                        SoldProducts = u.ProductsSold.Select(p => new SoldProductsOM
                        {
                            Count = u.ProductsSold.Count(),
                            Products = p.CategoryProducts.Select(cp => new ProductOM
                            {
                                Name = cp.Product.Name,
                                Price = cp.Product.Price
                            })
                            .OrderByDescending(x => x.Price)
                            .Take(10)
                            .ToArray()
                        })
                        .ToArray()
                    })
                    .ToArray()
                })
                .ToArray();

            var result = XmlConverter.Serialize(users, "Users");

            return result;
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(x => new CategoryOutputModel
                {
                    Name = x.Name,
                    Count = x.CategoryProducts.Count(),
                    AveragePrice = x.CategoryProducts.Select(a => a.Product).Average(x => x.Price),
                    TotalRevenue = x.CategoryProducts.Select(a => a.Product).Sum(x => x.Price)
                })
                .OrderByDescending(x => x.Count)
                .ThenBy(x => x.TotalRevenue)
                .ToList();

            var result = XmlConverter.Serialize(categories, "Categories");

            return result;
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(x => x.ProductsSold.Count() >= 1)
                .Select(x => new UserOutputModel
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Products = x.ProductsSold.Select(p => new SoldProducts
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                    .ToArray()
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Take(5)
                .ToList();

            var result = XmlConverter.Serialize(users, "Users");

            return result;
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .Select(x => new ProductOutputModel
                {
                    Name = x.Name,
                    Price = x.Price,
                    BuyerName = x.Buyer.FirstName + " " + x.Buyer.LastName
                })
                .OrderBy(x => x.Price)
                .Take(10)
                .ToList();

            var result = XmlConverter.Serialize(products, "Products");
            
            return result;

        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(CategoryProductInputModel[]), new XmlRootAttribute("CategoryProducts"));

            var categoryProductDtos = xmlSerializer.Deserialize(new StringReader(inputXml)) as CategoryProductInputModel[];

            var categoryIds = context.Categories.Select(x => x.Id).ToList();
            var productIds = context.Products.Select(x => x.Id).ToList();

            var catProducts = categoryProductDtos
                .Where(x => categoryIds.Contains(x.CategoryId) && productIds.Contains(x.ProductId))
                .Select(x => new CategoryProduct
                {
                    CategoryId = x.CategoryId,
                    ProductId = x.ProductId
                })
                .ToList();

            context.CategoryProducts.AddRange(catProducts);
            context.SaveChanges();

            return $"Successfully imported {catProducts.Count()}";
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var xmlSerializer = new XmlSerializer(typeof(CategoryInputModel[]), new XmlRootAttribute("Categories"));

            var categoryDtos = xmlSerializer.Deserialize(new StringReader(inputXml)) as CategoryInputModel[];

            var categories = categoryDtos
                .Where(x => x.Name != null)
                .Select(x => new Category
                {
                    Name = x.Name
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
