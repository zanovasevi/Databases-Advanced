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

            Console.WriteLine(GetBooksByPrice(db));
        }

        // задача 2

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


        // задача 3

        //public static string GetGoldenBooks(BookShopContext context)
        //{
        //    var books = context.Books
        //        .Where(x => x.Copies < 5000 && x.EditionType == EditionType.Gold)
        //        .Select(x => new
        //        {
        //            x.BookId,
        //            x.Title
        //        })
        //        .OrderBy(x => x.BookId)
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach(var book in books)
        //    {
        //        sb.AppendLine(book.Title);
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // задача 4

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Price > 40)
                .Select(x => new
                {
                    x.Title,
                    x.Price
                })
                .OrderByDescending(x => x.Price)
                .ToList();

            var sb = new StringBuilder();

            foreach(var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:F2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
