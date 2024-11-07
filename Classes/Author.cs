using LibraryManagementSystem.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Classes
{
    public class Author : IIdentifiable
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Country { get; set; }

        public Author(string name, int id, string country)
        {
            Name = name;
            Id = id;
            Country = country;
        }
    }
}
