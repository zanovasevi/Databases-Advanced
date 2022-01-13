namespace BookShop
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            DbInitializer.ResetDatabase(db);

            int year = int.Parse(Console.ReadLine());
            //string command = Console.ReadLine();
            //string date = Console.ReadLine();
            //string input = Console.ReadLine().ToLower();

            Console.WriteLine(GetBooksNotReleasedIn(db, year));
        }

        // задача 4 + int year = int.Parse(Console.ReadLine());

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var books = context.Books
                .Where(x => x.ReleaseDate.Value.Year != year)
                .Select(x => new
                {
                    x.BookId,
                    x.Title
                })
                .OrderBy(x => x.BookId)
                .ToList();

            var sb = new StringBuilder();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }

            return sb.ToString().TrimEnd();
        }

        // задача 5

        //public static string GetBooksByCategory(BookShopContext context, string input)
        //{
        //    var categories = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        //    var books = context.Books
        //        .Where(x => x.BookCategories
        //        .Any(x => categories.Contains(x.Category.Name)))
        //        .Select(x => new
        //        {
        //            x.Title
        //        })
        //        .OrderBy(x => x.Title)
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach (var book in books)
        //    {
        //        sb.AppendLine(book.Title);
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // задача 6

        //public static string GetBooksReleasedBefore(BookShopContext context, string date)
        //{
        //    var targetDate = DateTime.ParseExact(date, "dd-MM-yyyy",
        //        CultureInfo.InvariantCulture);

        //    var books = context.Books
        //        .Where(x => x.ReleaseDate.Value < targetDate)
        //        .Select(x => new
        //        {
        //            x.Title,
        //            x.EditionType,
        //            x.Price,
        //            x.ReleaseDate
        //        })
        //        .OrderByDescending(x => x.ReleaseDate)
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach (var book in books)
        //    {
        //        sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:F2}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // задача 7

        //public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        //{
        //    var names = context.Authors
        //        .Where(x => x.FirstName.EndsWith(input))
        //        .Select(x => new
        //        {
        //            FullName = x.FirstName + " " + x.LastName
        //        })
        //        .OrderBy(x => x.FullName)
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach (var name in names)
        //    {
        //        sb.AppendLine(name.FullName);
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // задача 8 - Book Search

        //public static string GetBookTitlesContaining(BookShopContext context, string input)
        //{
        //    var books = context.Books
        //        .Where(x => x.Title.Contains(input))
        //        .Select(x => new
        //        {
        //            x.Title
        //        })
        //        .OrderBy(x => x.Title)
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach (var book in books)
        //    {
        //        sb.AppendLine(book.Title);
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // задача 9

        //public static string GetBooksByAuthor(BookShopContext context, string input)
        //{
        //    var books = context.Books
        //        .Where(x => EF.Functions.Like(x.Author.LastName, $"{input}%"))
        //        .OrderBy(x => x.BookId)
        //        .Select(x => new
        //        {
        //            x.BookId,
        //            x.Title,
        //            FullName = x.Author.FirstName + " " + x.Author.LastName
        //        })
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach(var book in books)
        //    {
        //        sb.AppendLine($"{book.Title} ({book.FullName})");
        //    }

        //    return sb.ToString().TrimEnd();
        //}
    }
}
