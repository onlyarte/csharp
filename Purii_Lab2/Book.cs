using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purii_Lab2
{
    class Book
    {
        public string title;
        public string author;
        public int year;
        public string isbn;

        public Book() { }
        
        public Book(string title, string author, int year, string isbn)
        {
            this.title = title;
            this.author = author;
            this.year = year;
            this.isbn = isbn;
        }

        public override string ToString()
        {
            return "{ " + title + " | " + author + " | " + year + " | " + isbn + " }";
        }
    }
}
