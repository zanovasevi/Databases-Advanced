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

            //int year = int.Parse(Console.ReadLine());
            //string command = Console.ReadLine();
            //string date = Console.ReadLine();
            //string input = Console.ReadLine().ToLower();
            //int lengthCheck = int.Parse(Console.ReadLine());

            Console.WriteLine(RemoveBooks(db)); 
            
        }

        // задача 4 + int year = int.Parse(Console.ReadLine());

        //public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        //{
        //    var books = context.Books
        //        .Where(x => x.ReleaseDate.Value.Year != year)
        //        .Select(x => new
        //        {
        //            x.BookId,
        //            x.Title
        //        })
        //        .OrderBy(x => x.BookId)
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach (var book in books)
        //    {
        //        sb.AppendLine(book.Title);
        //    }

        //    return sb.ToString().TrimEnd();
        //}

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

        // задача 10

        //public static int CountBooks(BookShopContext context, int lengthCheck)
        //{
        //    var books = context.Books
        //        .Where(x => x.Title.Length > lengthCheck)
        //        .Select(x => x.Title)
        //        .ToArray();

        //    return books.Count();
        //}

        // задача 11

        //public static string CountCopiesByAuthor(BookShopContext context)
        //{
        //    var authors = context.Authors
        //        .Select(x => new
        //        {
        //            x.FirstName,
        //            x.LastName,
        //            TotalCopies = x.Books.Sum(x => x.Copies)
        //        })
        //        .OrderByDescending(x => x.TotalCopies)
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach(var au in authors)
        //    {
        //        sb.AppendLine($"{au.FirstName} {au.LastName} - {au.TotalCopies}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // задача 12

        //public static string GetTotalProfitByCategory(BookShopContext context)
        //{
        //    var categories = context.Categories
        //        .Select(x => new
        //        {
        //            x.Name,
        //            Profit = x.CategoryBooks.Sum(x => x.Book.Price * x.Book.Copies)
        //        })
        //        .OrderByDescending(x => x.Profit)
        //        .ThenBy(x => x.Name)
        //        .ToArray();

        //    var sb = new StringBuilder();

        //    foreach(var cat in categories)
        //    {
        //        sb.AppendLine($"{cat.Name} ${cat.Profit:F2}");
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // задача 13

        //public static string GetMostRecentBooks(BookShopContext context)
        //{
        //    var categories = context.Categories
        //        .Select(x => new
        //        {
        //            x.Name,
        //            Books = x.CategoryBooks
        //            .Select(x => x.Book)
        //            .OrderByDescending(x => x.ReleaseDate)
        //            .Take(3)
        //            .Select(x => new
        //            {
        //                x.Title,
        //                ReleaseDate = x.ReleaseDate.Value.Year
        //            })
        //            .ToList()
        //        })
        //        .OrderBy(x => x.Name)
        //        .ToList();

        //    var sb = new StringBuilder();

        //    foreach(var cat in categories)
        //    {
        //        sb.AppendLine($"--{cat.Name}");

        //        foreach(var book in cat.Books)
        //        {
        //            sb.AppendLine($"{book.Title} ({book.ReleaseDate})");
        //        }
        //    }

        //    return sb.ToString().TrimEnd();
        //}

        // задача 14

        //public static void IncreasePrices(BookShopContext context)
        //{
        //    var books = context.Books
        //        .Where(x => x.ReleaseDate.Value.Year < 2010)
        //        .ToArray();

        //    foreach(var book in books)
        //    {
        //        book.Price += 5;
        //    }

        //    context.SaveChanges();
        //}

        // задача 15

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Copies < 4200)
                .ToList();

            context.RemoveRange(books);
            int count = books.Count();
            context.SaveChanges();
            return count;
        }
    }
}
