using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dataJSONfilePath = "LibraryData.json";
            string allDataAsJSONtype = File.ReadAllText(dataJSONfilePath);

            HandleLibraryDB handleLibraryDB = JsonSerializer.Deserialize<HandleLibraryDB>(allDataAsJSONtype)!;

            List<Book> allBooks = handleLibraryDB.AllBooksFromDB;
            List<Author> allAuthors = handleLibraryDB.AllAuthorsFromDB;
            BookManager bookManager = new BookManager();

            if (handleLibraryDB != null && handleLibraryDB.AllBooksFromDB != null)
            {
                Console.WriteLine("Welcome to the library. Choose an option below: \n");

                while (true)
                {
                    PrintOutUserMenu();

                    string menuOptionChoosed = Console.ReadLine()!;

                    switch (menuOptionChoosed)
                    {
                        case "1":
                            bookManager.AddNewBook(handleLibraryDB.AllBooksFromDB, handleLibraryDB.AllAuthorsFromDB);
                            break;
                        case "2":
                            bookManager.AddNewAuthor(handleLibraryDB.AllAuthorsFromDB);
                            break;
                        case "3":
                            bookManager.UpdateBookInfo(handleLibraryDB.AllBooksFromDB, handleLibraryDB.AllAuthorsFromDB);
                            break;
                        case "4":
                            bookManager.UpdateAuthorInfo(handleLibraryDB.AllAuthorsFromDB);
                            break;
                        case "5":
                            bookManager.RemoveBook(handleLibraryDB.AllBooksFromDB);
                            break;
                        case "6":
                            bookManager.RemoveAuthor(handleLibraryDB.AllAuthorsFromDB);
                            break;
                        case "7":
                            bookManager.ListAllBooksAndAuthors(handleLibraryDB.AllBooksFromDB, handleLibraryDB.AllAuthorsFromDB);
                            break;
                        case "8":
                            bookManager.SearchAndFilterBooks(handleLibraryDB.AllBooksFromDB, handleLibraryDB.AllAuthorsFromDB);
                            break;
                        case "9":
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
            else
            {
                Console.WriteLine("Failed to read data from the JSON file.");
            }
        }

        private static void PrintOutUserMenu()
        {
            Console.Clear();
            Console.WriteLine("1 - Add new book");
            Console.WriteLine("2 - Add new author");
            Console.WriteLine("3 - Update book info");
            Console.WriteLine("4 - Update author info");
            Console.WriteLine("5 - Remove a book from list");
            Console.WriteLine("6 - Remove an author from list");
            Console.WriteLine("7 - List all books and authors");
            Console.WriteLine("8 - Search and filter books");
            Console.WriteLine("9 - Exit and save data");
        }
    }
}
