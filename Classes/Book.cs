using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Classes
{
    public class Book
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public int ID { get; set; }
        public string Genre { get; set; }
        public int PublishedYear { get; set; }
        public int ISBN { get; set; }
        public List<int> Reviews { get; set; }

        public Book(string title, Author author, int id, string genre, int publishedYear, int isbn, List<int> reviews)
        {
            Title = title;
            Author = author;
            ID = id;
            Genre = genre;
            PublishedYear = publishedYear;
            ISBN = isbn;
            Reviews = reviews;
        }
    }
}
