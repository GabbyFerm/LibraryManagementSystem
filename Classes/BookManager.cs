using System.Collections.Generic;
using Spectre.Console;

namespace LibraryManagementSystem.Classes
{
    public class BookManager
    {
        public void AddNewBook(LibraryGenericFunctions<Book> allBooks, LibraryGenericFunctions<Author> allAuthors)
        {
            try
            {
                Console.WriteLine("Type the title of the book you want to add: ");
                string addNewBookTitle = Console.ReadLine()!;

                bool ifBookTitleExists = true;
                while (ifBookTitleExists)
                {
                    if (allBooks.ListAll().Any(book => book.Title.Equals(addNewBookTitle, StringComparison.OrdinalIgnoreCase)))
                    {
                        Console.WriteLine($"A book with the title {addNewBookTitle} already exists. Please try a different title.");

                        Console.WriteLine("Type a different title for the book: ");
                        addNewBookTitle = Console.ReadLine()!;
                    }
                    else
                    {
                        ifBookTitleExists = false;
                    }
                }

                Author? authorOfThisBook = GetOrAddAuthor(allAuthors);

                int addNewbookId = 0;
                bool bookIdExists = true;

                while (bookIdExists)
                {
                    addNewbookId = GetValidatedIntegerInput("Type the Id of the book you want to add:");

                    if (allBooks.ListAll().Any(book => book.Id == addNewbookId))
                    {
                        Console.WriteLine($"Abook with the Id {addNewbookId} already exists. Please try a different Id.");
                    }
                    else
                    {
                        bookIdExists = false;
                    }
                }

                string addNewBookGenre = "";
                bool validGenreInput = false;

                while (!validGenreInput)
                {
                    Console.WriteLine("Type the genre of the book you want to add: ");
                    addNewBookGenre = Console.ReadLine()!;

                    if (addNewBookGenre.All(characters => char.IsLetter(characters) || char.IsWhiteSpace(characters)))
                    {
                        validGenreInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Error, the genre can only contain letters. Please try again.");
                    }
                }

                int addNewBookPublishedYear = GetValidatedIntegerInput("Type the published year of the book you want to add:");

                int addNewBookIsbn = GetValidatedIntegerInput("Type the ISBN number of the book you want to add:");

                int addNewBookReview = GetValidatedIntegerInput("Add a review of the book (1-5):");

                List<int> bookReviews = new List<int>();
                bookReviews.Add(addNewBookReview);

                allBooks.Add(new Book(addNewBookTitle, authorOfThisBook, addNewbookId, addNewBookGenre, addNewBookPublishedYear, addNewBookIsbn, bookReviews));
                Console.WriteLine($"Book {addNewBookTitle} has been added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured while adding book " + ex.Message);
            }
        }
        private Author GetOrAddAuthor(LibraryGenericFunctions<Author> allAuthors)
        {
            Console.WriteLine("Type the name of the author of the book you want to add: ");
            string addNewBookAuthorName = Console.ReadLine()!;

            Author? authorOfThisBook = allAuthors.ListAll().FirstOrDefault(author => author.Name.Equals(addNewBookAuthorName, StringComparison.OrdinalIgnoreCase));

            if (authorOfThisBook == null)
            {
                Console.WriteLine("Author not found. You will now be prompted to add this author.");
                return AddNewAuthor(allAuthors);
            }

            return authorOfThisBook;
        }
        public Author AddNewAuthor(LibraryGenericFunctions<Author> allAuthors)
        {
            string addNewAuthorName = "";
            bool authorExists = true;

            while (authorExists)
            {
                Console.WriteLine("Type the name of the author you want to add: ");
                addNewAuthorName = Console.ReadLine()!;

                if (allAuthors.ListAll().Any(author => author.Name == addNewAuthorName))
                {
                    Console.WriteLine($"An author with the name {addNewAuthorName} already exists. Please try a different name.");
                }
                else
                {
                    authorExists = false;
                }
            }

            int addNewAuthorId = 0;
            bool idExists = true;

            while (idExists)
            {
                addNewAuthorId = GetValidatedIntegerInput("Type the Id of the author you want to add:");

                if (allAuthors.ListAll().Any(author => author.Id == addNewAuthorId))
                {
                    Console.WriteLine($"An author with the Id {addNewAuthorId} already exists. Please try a different Id.");
                }
                else
                {
                    idExists = false;
                }
            }

            Console.WriteLine("Type the country of the author: ");
            string addNewAuthorCountry = Console.ReadLine()!;

            Author authorToAdd = new Author(addNewAuthorName, addNewAuthorId, addNewAuthorCountry);
            allAuthors.Add(authorToAdd);
            Console.WriteLine($"Author {addNewAuthorName} has been added successfully.");
            return authorToAdd;
        }
        public void UpdateBookGenre(LibraryGenericFunctions<Book> allBooks)
        {
            Console.WriteLine("Enter the title of the book:");
            string bookTitle = Console.ReadLine()!;

            var book = allBooks.ListAll().FirstOrDefault(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                Console.WriteLine("Book not found in the list.\n");
                return;
            }

            Console.WriteLine("Enter the new genre:");
            book.Genre = Console.ReadLine()!;
            Console.WriteLine("Genre has been updated.");
            allBooks.Update(book);
        }
        public void AddReviewToBook(LibraryGenericFunctions<Book> allBooks)
        {
            Console.WriteLine("Enter the title of the book:");
            string bookTitle = Console.ReadLine()!;

            var book = allBooks.ListAll().FirstOrDefault(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                Console.WriteLine("Book not found in the list.\n");
                return;
            }
            Console.WriteLine("Enter the review (1-5):");

            int review;
            if (int.TryParse(Console.ReadLine(), out review) && review >= 1 && review <= 5)
            {
                book.Reviews.Add(review);
                Console.WriteLine("Review has been added.");
            }
            else
            {
                Console.WriteLine("Invalid review. Please enter a number between 1 and 5.");
            }
            allBooks.Update(book);
        }
        public void UpdateAuthorName(LibraryGenericFunctions<Author> allAuthors)
        {
            Console.WriteLine("Enter the name of the author:");
            string authorName = Console.ReadLine()!;

            var author = allAuthors.ListAll().FirstOrDefault(a => a.Name.Equals(authorName, StringComparison.OrdinalIgnoreCase));

            if (author == null)
            {
                Console.WriteLine("Author not found in the list.\n");
                return;
            }

            Console.WriteLine("Enter the new name for the author:");
            string newAuthorName = Console.ReadLine()!;
            author.Name = newAuthorName;
            Console.WriteLine($"Author's name has been updated to {newAuthorName}.");
            allAuthors.Update(author);
        }
        public void UpdateAuthorCountry(LibraryGenericFunctions<Author> allAuthors)
        {
            Console.WriteLine("Enter the name of the author:");
            string authorName = Console.ReadLine()!;

            var author = allAuthors.ListAll().FirstOrDefault(a => a.Name.Equals(authorName, StringComparison.OrdinalIgnoreCase));

            if (author == null)
            {
                Console.WriteLine("Author not found in the list.\n");
                return;
            }

            Console.WriteLine("Enter the new country for the author:");
            string newCountry = Console.ReadLine()!;
            author.Country = newCountry;
            Console.WriteLine($"Author's country has been updated to {newCountry}.");
            allAuthors.Update(author);
        }
        public void RemoveBook(LibraryGenericFunctions<Book> allBooks)
        {
            Console.WriteLine("Type the title of the book you want to remove:");
            string bookTitleToRemove = Console.ReadLine()!;

            allBooks.RemoveByCondition(book => book.Title.Equals(bookTitleToRemove, StringComparison.OrdinalIgnoreCase));
        }
        public void RemoveAuthor(LibraryGenericFunctions<Author> allAuthors)
        {
            Console.WriteLine("Type the name of the author you want to remove:");
            string authorNameToRemove = Console.ReadLine()!;

            allAuthors.RemoveByCondition(author => author.Name.Equals(authorNameToRemove, StringComparison.OrdinalIgnoreCase));
        }
        public void ListAllBooks(LibraryGenericFunctions<Book> allBooks)
        {
            var sortedBooks = allBooks.ListAll().OrderBy(book => book.Title);
            var table = new Table();
            table.AddColumn("[bold springgreen4]Title[/]");
            table.AddColumn("[bold springgreen4]Author[/]");
            table.AddColumn("[bold springgreen4]Author Country[/]");
            table.AddColumn("[bold springgreen4]Id[/]");
            table.AddColumn("[bold springgreen4]Genre[/]");
            table.AddColumn("[bold springgreen4]Published Year[/]");
            table.AddColumn("[bold springgreen4]ISBN[/]");
            table.AddColumn("[bold springgreen4]Average Review[/]");

            // Lägg till varje bok i tabellen via PrintBookInfo
            foreach (var book in sortedBooks)
            {
                PrintBookInfo(table, book);
            }

            // Anpassa tabellens utseende och skriv ut
            table.Border = TableBorder.Rounded;
            table.LeftAligned();
            table.BorderColor(Color.SpringGreen3);
            AnsiConsole.Write(table);
        }
        public void ListAllAuthors(LibraryGenericFunctions<Book> allBooks, LibraryGenericFunctions<Author> allAuthors)
        {
            var sortedAuthors = allAuthors.ListAll().OrderBy(author => author.Name);
            if (sortedAuthors.Any())
            {
                // Skapa och konfigurera en tabell för att visa författare och deras böcker
                var table = new Table();
                table.AddColumn("[bold springgreen4]Author Name[/]");
                table.AddColumn("[bold springgreen4]Country[/]");
                table.AddColumn("[bold springgreen4]Author Id[/]");
                table.AddColumn("[bold springgreen4]Books[/]");

                Console.WriteLine("Authors listed in alphabetical order:\n");

                // Lägg till varje författare och deras böcker i tabellen
                foreach (var author in sortedAuthors)
                {
                    PrintAuthorInfo(table, author, allBooks);
                }

                // Anpassa och skriv ut tabellen
                table.Border = TableBorder.Rounded;
                table.LeftAligned();
                table.BorderColor(Color.SpringGreen3);
                AnsiConsole.Write(table);
            }
            else
            {
                Console.WriteLine("No authors found.");
            }
        }
        private void PrintAuthorInfo(Table table, Author author, LibraryGenericFunctions<Book> allBooks)
        {
            var authorBooks = allBooks.ListAll().Where(book => book.Author.Id == author.Id);

            // Skapa en sträng med alla boktitlar för denna författare, separerade med kommatecken
            string booksList = string.Join(", ", authorBooks.Select(book => book.Title));

            // Lägg till en rad i tabellen med författarens information och deras böcker
            table.AddRow(
                author.Name,
                author.Country,
                author.Id.ToString(),
                string.IsNullOrEmpty(booksList) ? "No books" : booksList
            );
        }
        private void PrintBookInfo(Table table, Book book)
        {
            double averageReview = CalculateAverageReview(book);
            table.AddRow(
                book.Title,
                book.Author.Name,
                book.Author.Country,
                book.Id.ToString(),
                book.Genre,
                book.PublishedYear.ToString(),
                book.ISBN.ToString(),
                averageReview.ToString("F1")
            );
        }
        public void FilterBooksByGenre(LibraryGenericFunctions<Book> allBooks)
        {
            Console.WriteLine("Enter the genre to filter by: ");
            string genreFilter = Console.ReadLine()!;

            var filteredBooksByGenre = allBooks.ListAll().Where(book => book.Genre.Equals(genreFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            if (filteredBooksByGenre.Any())
            {
                // Skapa och konfigurera en tabell för att visa böcker
                var table = new Table();
                table.AddColumn("[bold springgreen4]Title[/]");
                table.AddColumn("[bold springgreen4]Author[/]");
                table.AddColumn("[bold springgreen4]Author Country[/]");
                table.AddColumn("[bold springgreen4]Id[/]");
                table.AddColumn("[bold springgreen4]Genre[/]");
                table.AddColumn("[bold springgreen4]Published Year[/]");
                table.AddColumn("[bold springgreen4]ISBN[/]");
                table.AddColumn("[bold springgreen4]Average Review[/]");

                Console.WriteLine($"Books in {genreFilter} genre:");

                // Lägg till varje bok i tabellen via PrintBookInfo
                foreach (var book in filteredBooksByGenre)
                {
                    PrintBookInfo(table, book);
                }

                // Anpassa och skriv ut tabellen
                table.Border = TableBorder.Rounded;
                table.LeftAligned();
                table.BorderColor(Color.SpringGreen3);
                AnsiConsole.Write(table);
            }
            else
            {
                Console.WriteLine($"No books found in the {genreFilter} genre.");
            }
        }
        public void ListBooksByAverageReview(LibraryGenericFunctions<Book> allBooks)
        {
            Console.WriteLine("Enter the minimum average review (1-5): ");
            int minReview = GetValidatedIntegerInput("Enter a valid number between 1 and 5 for average review filter:");

            var booksAboveReview = allBooks.ListAll().Where(book => CalculateAverageReview(book) > minReview).ToList();

            if (booksAboveReview.Any())
            {
                // Skapa och konfigurera en tabell för att visa böcker
                var table = new Table();
                table.AddColumn("[bold springgreen4]Title[/]");
                table.AddColumn("[bold springgreen4]Author[/]");
                table.AddColumn("[bold springgreen4]Author Country[/]");
                table.AddColumn("[bold springgreen4]Id[/]");
                table.AddColumn("[bold springgreen4]Genre[/]");
                table.AddColumn("[bold springgreen4]Published Year[/]");
                table.AddColumn("[bold springgreen4]ISBN[/]");
                table.AddColumn("[bold springgreen4]Average Review[/]");

                Console.WriteLine($"Books with average review above {minReview}:");

                // Lägg till varje bok i tabellen via PrintBookInfo
                foreach (var book in booksAboveReview)
                {
                    PrintBookInfo(table, book);
                }

                // Anpassa och skriv ut tabellen
                table.Border = TableBorder.Rounded;
                table.LeftAligned();
                table.BorderColor(Color.SpringGreen3);
                AnsiConsole.Write(table);
            }
            else
            {
                Console.WriteLine($"No books found with an average review above {minReview}.");
            }
        }
        public void SortBooksByYear(LibraryGenericFunctions<Book> allBooks)
        {
            var sortedBooksByYear = allBooks.ListAll().OrderBy(book => book.PublishedYear).ToList();

            if (sortedBooksByYear.Any())
            {
                // Skapa och konfigurera en tabell för att visa böcker
                var table = new Table();
                table.AddColumn("[bold springgreen4]Title[/]");
                table.AddColumn("[bold springgreen4]Author[/]");
                table.AddColumn("[bold springgreen4]Author Country[/]");
                table.AddColumn("[bold springgreen4]Id[/]");
                table.AddColumn("[bold springgreen4]Genre[/]");
                table.AddColumn("[bold springgreen4]Published Year[/]");
                table.AddColumn("[bold springgreen4]ISBN[/]");
                table.AddColumn("[bold springgreen4]Average Review[/]");

                Console.WriteLine("Books sorted by published year:");

                // Lägg till varje bok i tabellen via PrintBookInfo
                foreach (var book in sortedBooksByYear)
                {
                    PrintBookInfo(table, book);
                }

                // Anpassa och skriv ut tabellen
                table.Border = TableBorder.Rounded;
                table.LeftAligned();
                table.BorderColor(Color.SpringGreen3);
                AnsiConsole.Write(table);
            }
            else
            {
                Console.WriteLine("No books found.");
            }
        }
        public double CalculateAverageReview(Book book)
        {
            if (book.Reviews.Count == 0)
            {
                return 0;
            }
            return book.Reviews.Average();
        }
        public int GetValidatedIntegerInput(string promptMessage)
        {
            int validatedInput = 0;
            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine(promptMessage);
                string input = Console.ReadLine()!;

                if (input.All(char.IsDigit))
                {
                    validatedInput = Convert.ToInt32(input);
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Error, input must only contain numbers. Please try again.");
                }
            }
            return validatedInput;
        }
    }
}
