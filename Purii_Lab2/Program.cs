using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purii_Lab2
{
    class Program
    {
        static Library lib;

        static void Main(string[] args)
        {
            lib = new Library();

            lib.AddBook(new Book("A little life", "Hanya Yanagihara", 2015, "978-617-7489-90-9"));
            lib.AddBook(new Book("The Great Alone: A Novel", "Kristin Hannah", 1974, "978-617-7489-90-9"));
            lib.AddBook(new Book("The Man with the Golden Arm", "Nelson Algren", 1950, "978-617-7489-90-9"));
            lib.AddBook(new Book("The Collected Stories of William Faulkner", "William Faulkner", 1951, "978-617-7489-90-9"));
            lib.AddBook(new Book("Gravity's Rainbow", "Thomas Pynchon", 1974, "978-617-7489-90-9"));
            lib.AddBook(new Book("Steps", "Jerzy Kosinski", 1969, "978-617-7489-90-9"));
            lib.AddBook(new Book("Three Junes", "Julia Glass", 2002, "978-617-7489-90-9"));
            lib.AddBook(new Book("Europe Central", "William T. Vollmann", 2005, "978-617-7489-90-9"));
            lib.AddBook(new Book("All the Pretty Horses", "Cormac McCarthy", 1992, "978-617-7489-90-9"));
            lib.AddBook(new Book("The Underground Railroad", "Colson Whitehead", 2016, "978-617-7489-90-9"));
            lib.AddBook(new Book("Salvage the Bones", "Jesmyn Ward", 2011, "978-617-7489-90-9"));

            MainPane();
        }

        static void ListPane()
        {
            Console.WriteLine("=== Listing ===");
            Console.WriteLine("1 - Book titles");
            Console.WriteLine("2 - Authors");
            Console.WriteLine("3 - Return");

            string choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    var res = lib.ListBookTitles();
                    foreach(string title in res)
                    {
                        Console.WriteLine(title);
                    }
                    Console.WriteLine("Done! Press any key to return...");
                    Console.ReadKey();
                    ListPane();
                    break;
                case "2":
                    var res1 = lib.ListAuthors();
                    foreach (string author in res1)
                    {
                        Console.WriteLine(author);
                    }
                    Console.WriteLine("Done! Press any key to return...");
                    Console.ReadKey();
                    ListPane();
                    break;
                case "3":
                    MainPane();
                    break;
                default:
                    Console.WriteLine("Wrong choice!");
                    ListPane();
                    break;
            }
        }

        static void SearchPane()
        {
            Console.WriteLine("=== Searching ===");
            Console.WriteLine("1 - By title");
            Console.WriteLine("2 - By author");
            Console.WriteLine("3 - By year");
            Console.WriteLine("4 - Return");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Book title: ");
                    string query = Console.ReadLine();
                    var res = lib.SearchByTitle(query);
                    foreach (Book book in res)
                    {
                        Console.WriteLine(book);
                    }
                    Console.WriteLine("Done! Press any key to return...");
                    Console.ReadKey();
                    SearchPane();
                    break;
                case "2":
                    Console.WriteLine("Book author: ");
                    string query1 = Console.ReadLine();
                    var res1 = lib.SearchByAuthor(query1);
                    foreach (Book book in res1)
                    {
                        Console.WriteLine(book);
                    }
                    Console.WriteLine("Done! Press any key to return...");
                    Console.ReadKey();
                    SearchPane();
                    break;
                case "3":
                    Console.WriteLine("Year from: ");
                    int query2 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Year to: ");
                    int query3 = Convert.ToInt32(Console.ReadLine());
                    var res2 = lib.SearchByYear(query2, query3);
                    foreach (Book book in res2)
                    {
                        Console.WriteLine(book);
                    }
                    Console.WriteLine("Done! Press any key to return...");
                    Console.ReadKey();
                    SearchPane();
                    break;
                case "4":
                    MainPane();
                    break;
                default:
                    Console.WriteLine("Wrong choice!");
                    SortPane();
                    break;
            }
        }

        static void SortPane()
        {
            Console.WriteLine("=== Sorting ===");
            Console.WriteLine("1 - By author");
            Console.WriteLine("2 - By year");
            Console.WriteLine("3 - Return");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    var res = lib.SortByAuthor();
                    foreach(Book book in res)
                    {
                        Console.WriteLine(book);
                    }
                    Console.WriteLine("Done! Press any key to return...");
                    Console.ReadKey();
                    SortPane();
                    break;
                case "2":
                    var res1 = lib.SortByYear();
                    foreach (Book book in res1)
                    {
                        Console.WriteLine(book);
                    }
                    Console.WriteLine("Done! Press any key to return...");
                    Console.ReadKey();
                    SortPane();
                    break;
                case "3":
                    MainPane();
                    break;
                default:
                    Console.WriteLine("Wrong choice!");
                    SortPane();
                    break;
            }
        }

        static void MainPane()
        {
            Console.WriteLine("=== LIBRARY ===");
            Console.WriteLine("1 - Sorting");
            Console.WriteLine("2 - Searching");
            Console.WriteLine("3 - Listing");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SortPane();
                    break;
                case "2":
                    SearchPane();
                    break;
                case "3":
                    ListPane();
                    break;
                default:
                    Console.WriteLine("Wrong choice!");
                    MainPane();
                    break;
            }
        }
    }
}
