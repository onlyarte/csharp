using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purii_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            // create list of students
            List<Student> studList = new List<Student>();
            studList.Add(new Student("Karpenko"));
            studList.Add(new Student("Kamarov"));
            studList.Add(new Student("Lisko"));
            studList.Add(new Student("Deruk"));
            studList.Add(new Student("Kostenko"));

            // fill students' grades with random int 61-100
            Random rnd = new Random();
            string[] subjects = new string[] { "OOP", "FP", "LP", "ODM" };

            foreach(Student stud in studList)
            {
                foreach(string subject in subjects)
                {
                    stud.AddGrade(subject, rnd.Next(61, 100));
                }
            }

            // sort in descending order
            studList.Sort((a, b) => b.GetAverageGrage().CompareTo(a.GetAverageGrage()));

            // log results in console
            foreach(Student stud in studList)
            {
                Console.WriteLine(stud);
            }

            Console.ReadLine();
        }
    }
}
