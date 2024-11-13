# Library Management System

Welcome to the Library Management System! This C# application allows you to manage a library's books and authors through a console-based interface. You can add new books and authors, update existing information, remove entries, and perform searches and filters on the books.

# Features
- Add new book: Add new books to the library, including title, author, genre, and other details.
- Add new author: Add authors to the system and link them to their books.
- Update book and author info: Edit details for books and authors.
- Remove books and authors: Remove books and authors from the system.
- List all books and authors: View a list of all books and their associated authors.
- Search and filter books: Search for books based on different criteria like genre, title, or author.
- Data persistence: Library data is saved to a JSON file and loaded on startup.

# Technologies Used
- C#: Main programming language.
- .NET Core: Framework used to build the application.
- JSON: Data storage format for books and authors.
- Spectre.Console: Library for creating styled terminal output, including menus and tables.
- Figgle: Library for rendering stylized text (for headers).

# Error Handling
- If the JSON file is not found or cannot be parsed, an error message will be displayed.
- If any unexpected error occurs during the program's execution, it will be caught and displayed.
