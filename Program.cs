using LibraryManagementSystem.Classes;
using System.Text.Json;
using System;
using System.IO;
using Figgle;
using Spectre.Console;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dataJSONfilePath = "LibraryData.json";
            HandleLibraryDB handleLibraryDB;

            try
            {
                string jsonData = File.ReadAllText(dataJSONfilePath);
                handleLibraryDB = JsonSerializer.Deserialize<HandleLibraryDB>(jsonData) ?? throw new Exception("Deserialization returned null.");

                BookManager bookManager = new BookManager();
                var allAuthors = new LibraryGenericFunctions<Author>(handleLibraryDB.AllAuthorsFromDB);
                var allBooks = new LibraryGenericFunctions<Book>(handleLibraryDB.AllBooksFromDB);

                while (true)
                {
                    Console.Clear(); // Clear the console to redraw the header

                    string header = FiggleFonts.ThreePoint.Render("Welcome to your Library");

                    // Use AnsiConsole.Markup to render the header with styles
                    AnsiConsole.Markup($"[bold springgreen3]{header}[/]\n");

                    var menuOptions = new[]
                    {
                        "[bold springgreen3]1 - Add new book[/]",
                        "[bold springgreen3]2 - Add new author[/]",
                        "[bold springgreen3]3 - Update book info[/]",
                        "[bold springgreen3]4 - Update author info[/]",
                        "[bold springgreen3]5 - Remove a book from list[/]",
                        "[bold springgreen3]6 - Remove an author from list[/]",
                        "[bold springgreen3]7 - List all books and authors[/]",
                        "[bold springgreen3]8 - Search and filter books[/]",
                        "[bold springgreen3]9 - Exit and save data[/]"
                    };

                    // Using Spectre.Console's SelectionPrompt to handle menu navigation with arrows
                    var selectedOption = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("[bold springgreen4]Choose an option:[/]")
                            .PageSize(10)
                            .AddChoices(menuOptions)
                            .HighlightStyle(new Style(foreground: Color.Gold3_1, background: Color.Grey23))
                    );

                    var submenus = new Submenus();

                    switch (selectedOption)
                    {
                        case "[bold springgreen3]1 - Add new book[/]":
                            bookManager.AddNewBook(allBooks, allAuthors);
                            break;
                        case "[bold springgreen3]2 - Add new author[/]":
                            bookManager.AddNewAuthor(allAuthors);
                            break;
                        case "[bold springgreen3]3 - Update book info[/]":
                            //bookManager.UpdateBookInfo(allBooks);
                            submenus.ShowUpdateBookSubmenu(bookManager, allBooks);
                            break;
                        case "[bold springgreen3]4 - Update author info[/]":
                            //bookManager.UpdateAuthorInfo(allAuthors);
                            submenus.ShowUpdateAuthorSubmenu(bookManager, allAuthors);
                            break;
                        case "[bold springgreen3]5 - Remove a book from list[/]":
                            bookManager.RemoveBook(allBooks);
                            break;
                        case "[bold springgreen3]6 - Remove an author from list[/]":
                            bookManager.RemoveAuthor(allAuthors);
                            break;
                        case "[bold springgreen3]7 - List all books and authors[/]":
                            //bookManager.ListAllBooksAndAuthors(allBooks, allAuthors);
                            submenus.ShowListBooksAndAuthorsSubmenu(bookManager, allBooks, allAuthors);
                            break;
                        case "[bold springgreen3]8 - Search and filter books[/]":
                            //bookManager.SearchAndFilterBooks(allBooks, allAuthors);
                            submenus.ShowSearchAndFilterBooksSubmenu(bookManager, allBooks, allAuthors);
                            break;
                        case "[bold springgreen3]9 - Exit and save data[/]":
                            string updatedLibraryDB = JsonSerializer.Serialize(handleLibraryDB, new JsonSerializerOptions { WriteIndented = true });
                            File.WriteAllText(dataJSONfilePath, updatedLibraryDB);
                            Console.WriteLine("Library data saved. Exiting program.");
                            return;
                        default:
                            Console.WriteLine("Invalid option, please try again.");
                            break;
                    }
                    Console.WriteLine("\nPress any key to return to the menu.");
                    Console.ReadKey();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The file '{dataJSONfilePath}' was not found.");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error: Failed to parse JSON data. Details: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
