using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Classes
{
    public class Submenus
    {
        public void ShowUpdateBookSubmenu(BookManager bookManager, LibraryGenericFunctions<Book> allBooks)
        {
            var submenuOptions = new[]
            {
                "[bold springgreen3]1 - Update genre[/]",
                "[bold springgreen3]2 - Add review[/]"
            };

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold springgreen4]Choose a book update option:[/]")
                    .PageSize(10)
                    .AddChoices(submenuOptions)
                    .HighlightStyle(new Style(foreground: Color.Gold3_1, background: Color.Grey23))
            );

            switch (selectedOption)
            {
                case "[bold springgreen3]1 - Update genre[/]":
                    bookManager.UpdateBookGenre(allBooks);
                    break;
                case "[bold springgreen3]2 - Add review[/]":
                    bookManager.AddReviewToBook(allBooks);
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
        public void ShowUpdateAuthorSubmenu(BookManager bookManager, LibraryGenericFunctions<Author> allAuthors)
        {
            var submenuOptions = new[]
            {
                "[bold springgreen3]1 - Update name[/]",
                "[bold springgreen3]2 - Update country[/]"
            };

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold springgreen4]Choose an author update option:[/]")
                    .PageSize(10)
                    .AddChoices(submenuOptions)
                    .HighlightStyle(new Style(foreground: Color.Gold3_1, background: Color.Grey23))
            );

            switch (selectedOption)
            {
                case "[bold springgreen3]1 - Update name[/]":
                    bookManager.UpdateAuthorName(allAuthors);
                    break;
                case "[bold springgreen3]2 - Update country[/]":
                    bookManager.UpdateAuthorCountry(allAuthors);
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
        public void ShowSearchAndFilterBooksSubmenu(BookManager bookManager, LibraryGenericFunctions<Book> allBooks, LibraryGenericFunctions<Author> allAuthors)
        {
            var submenuOptions = new[]
            {
                "[bold springgreen3]1 - Filter books by genre[/]",
                "[bold springgreen3]2 - List all books with an average review above specific number[/]",
                "[bold springgreen3]3 - Sort books by published year[/]"
            };

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold springgreen4]Choose a filter option:[/]")
                    .PageSize(10)
                    .AddChoices(submenuOptions)
                    .HighlightStyle(new Style(foreground: Color.Gold3_1, background: Color.Grey23))
            );

            switch (selectedOption)
            {
                case "[bold springgreen3]1 - Filter books by genre[/]":
                    bookManager.FilterBooksByGenre(allBooks);
                    break;
                case "[bold springgreen3]2 - List all books with an average review above specific number[/]":
                    bookManager.ListBooksByAverageReview(allBooks);
                    break;
                case "[bold springgreen3]3 - Sort books by published year[/]":
                    bookManager.SortBooksByYear(allBooks);
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
        public void ShowListBooksAndAuthorsSubmenu(BookManager bookManager, LibraryGenericFunctions<Book> allBooks, LibraryGenericFunctions<Author> allAuthors)
        {
            var submenuOptions = new[]
            {
                "[bold springgreen3]1 - List all books[/]",
                "[bold springgreen3]2 - List all authors[/]"
            };

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold springgreen4]Choose an option:[/]")
                    .PageSize(10)
                    .AddChoices(submenuOptions)
                    .HighlightStyle(new Style(foreground: Color.Gold3_1, background: Color.Grey23))
            );

            switch (selectedOption)
            {
                case "[bold springgreen3]1 - List all books[/]":
                    bookManager.ListAllBooks(allBooks);
                    break;
                case "[bold springgreen3]2 - List all authors[/]":
                    bookManager.ListAllAuthors(allBooks, allAuthors);
                    break;
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
        }
    }
}
