using System.Collections.Generic;

namespace LibraryManagementSystem.Classes
{
    public class BookManager
    {
        public void AddNewBook(LibraryGenericFunctions<Book> allBooks, LibraryGenericFunctions<Author> allAuthors)
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
        private Author GetOrAddAuthor(LibraryGenericFunctions<Author> allAuthors)
        {
            Console.WriteLine("Type the name of the author of the book you want to add: ");
            string addNewBookAuthorName = Console.ReadLine()!;

            Author? authorOfThisBook = allAuthors.ListAll().FirstOrDefault(author => author.Name.Equals(addNewBookAuthorName, StringComparison.OrdinalIgnoreCase));

            if (authorOfThisBook == null)
            {
                Console.WriteLine("Author not found. You will now be prompted to add this author.");
                AddNewAuthor(allAuthors);
                authorOfThisBook = allAuthors.ListAll().FirstOrDefault(author => author.Name.Equals(addNewBookAuthorName, StringComparison.OrdinalIgnoreCase));

                if (authorOfThisBook == null)
                {
                    Console.WriteLine("Failed to retrieve the author after adding. Please try again.\n");
                    return null!;
                }
            }

            return authorOfThisBook;
        }
        public void AddNewAuthor(LibraryGenericFunctions<Author> allAuthors)
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

            allAuthors.Add(new Author(addNewAuthorName, addNewAuthorId, addNewAuthorCountry));
            Console.WriteLine($"Author {addNewAuthorName} has been added successfully.");
        }
        public void UpdateBookInfo(LibraryGenericFunctions<Book> allBooks)
        {
            Console.WriteLine("Enter the title of the book you want to update:");
            string bookTitle = Console.ReadLine()!;

            var book = allBooks.ListAll().FirstOrDefault(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));
            if (book == null)
            {
                Console.WriteLine("Book not found in the list.\n");
                return;
            }
            Console.WriteLine("What details do you want to update? Choose an option below:");
            Console.WriteLine("1 - Update genre");
            Console.WriteLine("2 - Add review");
            string updateBookInfoChoice = Console.ReadLine()!;

            switch (updateBookInfoChoice)
            {
                case "1":
                    Console.WriteLine("Enter the new genre:");
                    book.Genre = Console.ReadLine()!;
                    Console.WriteLine("Genre has been updated.");
                    break;
                case "2":
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
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter either 1 or 2.");
                    return;
            }
            allBooks.Update(book);
        }
        public void UpdateAuthorInfo(LibraryGenericFunctions<Author> allAuthors)
        {
            Console.WriteLine("Enter the name of the author you want to update:");
            string authorName = Console.ReadLine()!;

            var author = allAuthors.ListAll().FirstOrDefault(a => a.Name.Equals(authorName, StringComparison.OrdinalIgnoreCase));

            if (author == null)
            {
                Console.WriteLine("Author not found in the list.\n");
                return;
            }

            Console.WriteLine("What details do you want to update? Choose an option below:");
            Console.WriteLine("1 - Update name");
            Console.WriteLine("2 - Update country");
            string updateAuthorInfoChoice = Console.ReadLine()!;
            switch (updateAuthorInfoChoice)
            {
                case "1":
                    Console.WriteLine("Enter the new name for the author:");
                    string newAuthorName = Console.ReadLine()!;
                    author.Name = newAuthorName;
                    Console.WriteLine($"Author's name has been updated to {newAuthorName}.");
                    break;
                case "2":
                    Console.WriteLine("Enter the new country for the author:");
                    string newCountry = Console.ReadLine()!;
                    author.Country = newCountry;
                    Console.WriteLine($"Author's country has been updated to {newCountry}.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter either 1 or 2.");
                    break;
            }
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
        public void ListAllBooksAndAuthors(LibraryGenericFunctions<Book> allBooks, LibraryGenericFunctions<Author> allAuthors)
        {
            Console.WriteLine("What info do you want to list?");
            Console.WriteLine("1 - All books");
            Console.WriteLine("2 - All authors");

            string ListInfoMenuChoice = Console.ReadLine()!;
            switch (ListInfoMenuChoice)
            {
                case "1":
                    var sortedBooks = allBooks.ListAll().OrderBy(book => book.Title);
                    Console.WriteLine("Books listed in alphabetical order:\n");
                    foreach (var book in sortedBooks)
                    {
                        PrintBookInfo(book);
                    }
                    break;
                case "2":
                    var sortedAuthors = allAuthors.ListAll().OrderBy(author => author.Name);
                    Console.WriteLine("Authors listed in alphabetical order:\n");

                    foreach (var author in sortedAuthors)
                    {
                        PrintAuthorInfo(author, allBooks);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter either 1 or 2.");
                    break;
            }
        }
        private void PrintAuthorInfo(Author author, LibraryGenericFunctions<Book> allBooks)
        {
            Console.WriteLine("Author Information:");
            Console.WriteLine($"Author: {author.Name}");
            Console.WriteLine($"Author Id: {author.Id}");
            Console.WriteLine($"Country: {author.Country}\n");

            var booksByAuthor = allBooks.ListAll().Where(book => book.Author.Id == author.Id).ToList();
            if (booksByAuthor.Count > 0)
            {
                Console.WriteLine("Books:");
                foreach (var book in booksByAuthor)
        {
            double averageReview = CalculateAverageReview(book);

            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Id: {book.Id}");
            Console.WriteLine($"Genre: {book.Genre}");
            Console.WriteLine($"Published Year: {book.PublishedYear}");
            Console.WriteLine($"ISBN: {book.ISBN}");
            Console.WriteLine($"Average review: {averageReview:F1}");
            Console.WriteLine("--------------------------------");
        }
            }
            else
            {
                Console.WriteLine("No books available for this author.\n");
            }
        }
        private void PrintBookInfo(Book book)
        {
            double averageReview = CalculateAverageReview(book);
            Console.WriteLine("Book Information:");
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Author: {book.Author.Name} / Country: {book.Author.Country}");
            Console.WriteLine($"Id: {book.Id}");
            Console.WriteLine($"Genre: {book.Genre}");
            Console.WriteLine($"Published Year: {book.PublishedYear}");
            Console.WriteLine($"ISBN: {book.ISBN}");
            Console.WriteLine($"Average review: {averageReview:F1}");
            Console.WriteLine("--------------------------------");
        }
        public void SearchAndFilterBooks(LibraryGenericFunctions<Book> allBooks, LibraryGenericFunctions<Author> allAuthors)
        {
            Console.WriteLine("Search & filter books. Choose an option below: ");
            Console.WriteLine("1 - Filter books by genre");
            Console.WriteLine("2 - List all books with an average review above specific number");
            Console.WriteLine("3 - Sort books by published year");
            string searchFilterOptionChoosed = Console.ReadLine()!;

            switch (searchFilterOptionChoosed)
            {
                case "1":
                    Console.WriteLine("By what genre: ");
                    string listByGenre = Console.ReadLine()!;

                    var filteredBooksByGenre = allBooks.ListAll().Where(book => book.Genre.Equals(listByGenre, StringComparison.OrdinalIgnoreCase)).ToList();

                    if (filteredBooksByGenre.Any())
                    {
                        Console.WriteLine($"Books in {listByGenre}:");
                        foreach (var book in filteredBooksByGenre)
                        {
                            Console.WriteLine($"Title: {book.Title} | Author: {book.Author.Name} | Genre: {book.Genre}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No books found in that genre.");
                    }
                    break;
                case "2":
                    Console.WriteLine("Above what average number do you want to list books (1-5):");

                    int listByAverageNumber = 0;
                    bool validAverage = false;

                    while (!validAverage)
                    {
                        listByAverageNumber = GetValidatedIntegerInput("Please enter a number between 1 and 5:");

                        if (listByAverageNumber >= 1 && listByAverageNumber <= 5)
                        {
                            validAverage = true;
                        }
                        else
                        {
                            Console.WriteLine("Error, the average number must be between 1 and 5. Please try again.");
                        }
                    }

                    var booksAboveAverage = allBooks.ListAll().Where(book => book.Reviews.Count > 0 && book.Reviews.Average() > listByAverageNumber).ToList();

                    if (booksAboveAverage.Any())
                    {
                        Console.WriteLine($"Books with an average review above {listByAverageNumber}:");
                        foreach (var book in booksAboveAverage)
                        {
                            Console.WriteLine($"Title: {book.Title} | Average Review: {book.Reviews.Average():F1}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No books found with an average review above {listByAverageNumber}");
                    }

                    break;
                case "3":
                    var sortBooksByPublishedYear = allBooks.ListAll().OrderBy(book => book.PublishedYear).ToList();
                    Console.WriteLine("Books sorted by published year:");
                    foreach (var book in sortBooksByPublishedYear)
                    {
                        Console.WriteLine($"Title: {book.Title} | Author: {book.Author.Name} | Published Year: {book.PublishedYear}");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter either 1, 2 or 3.");
                    break;
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
