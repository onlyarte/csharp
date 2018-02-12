using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Purii_Lab2
{
    class Library
    {
        private List<Book> catalogue;

        public Library()
        {
            catalogue = new List<Book>();
        }

        public void AddBook(Book book)
        {
            catalogue.Add(book);
        }

        public bool Save(string fileName)
        {
            try
            {
                Stream stream = File.OpenWrite(Environment.CurrentDirectory + fileName);
                XmlSerializer XMLSer = new XmlSerializer(typeof(List<Book>));
                XMLSer.Serialize(stream, catalogue);
                stream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Load(string fileName)
        {
            try
            {
                Stream stream = File.OpenRead(Environment.CurrentDirectory + fileName);
                XmlSerializer XMLSer = new XmlSerializer(typeof(List<Book>));
                catalogue = (List<Book>) XMLSer.Deserialize(stream);
                stream.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Book> SearchByAuthor(string author)
        {
            return from book in catalogue
                   where book.author == author
                   select book;
        }

        public IEnumerable<Book> SearchByTitle(string title)
        {
            return from book in catalogue
                   where book.title == title
                   select book;
        }

        public IEnumerable<Book> SearchByYear(int start, int end)
        {
            return from book in catalogue
                   where book.year >= start && book.year <= end
                   select book;
        }

        public IEnumerable<Book> SortByYear()
        {
            return from book in catalogue
                   orderby book.year descending
                   select book;
        }

        public IEnumerable<Book> SortByAuthor()
        {
            return from book in catalogue
                   orderby book.author ascending
                   select book;
        }

        public IEnumerable<string> ListBookTitles()
        {
            return from book in catalogue
                   orderby book.title descending
                   select book.title;
        }

        public IEnumerable<string> ListAuthors()
        {
            return from book in catalogue
                   orderby book.author descending
                   select book.author;
        }
    }
}
