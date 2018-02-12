using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purii_Lab1
{
    class Student
    {
        private string surname;
        private List<Grade> grades;

        public Student(string surname)
        {
            this.surname = surname;
            grades = new List<Grade>();
        }

        public void AddGrade(string subject, int grade)
        {
            grades.Add(new Grade(subject, grade));
        }

        public double GetAverageGrage()
        {
            return (double) grades.ConvertAll<int>((a) => a.grade).Sum() / grades.Count();
        }

        public override string ToString()
        {
            string stud = surname + ": " + GetAverageGrage();
            foreach(Grade grade in grades)
            {
                stud += "\n[" + grade.subject + ": " + grade.grade + "]";
            }
            stud += "\n\n";
            return stud;
        }

        public class Grade
        {
            public string subject;
            public int grade;

            public Grade(string subject, int grade)
            {
                this.subject = subject;
                this.grade = grade;
            }
        }

    }
}
