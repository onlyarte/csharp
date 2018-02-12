using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purii_Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filePath = "D:\\storage.xml";

            StudentList sl = new StudentList();
            sl.InitList();

            if (sl.Save(filePath))
            {
                Console.WriteLine("=== saved to xml ===");
            } else
            {
                Console.WriteLine("=== failed to save to xml");
            }

            Console.WriteLine("=== before erase ===");
            Console.WriteLine(sl);

            sl.EraseList();
            Console.WriteLine("=== after erase ===");
            Console.WriteLine(sl);

            if (sl.Load(filePath))
            {
                Console.WriteLine("=== loaded from xml ===");
            }
            else
            {
                Console.WriteLine("=== failed to load from xml");
            }

            Console.WriteLine("=== after loading from xml ===");
            Console.WriteLine(sl);

            Console.ReadLine();
        }
    }
}
