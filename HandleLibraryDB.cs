using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public class HandleLibraryDB
    {
        [JsonPropertyName("books")]
        public List<Book> AllBooksFromDB { get; set; } = new List<Book>();

        [JsonPropertyName("authors")]
        public List<Author> AllAuthorsFromDB { get; set; } = new List<Author>();

        public HandleLibraryDB() { }
    }
}
