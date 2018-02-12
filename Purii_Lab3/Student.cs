using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;


namespace Purii_Lab3
{
    [Serializable()]
    public class Student
    {
        public string surname { get; set; }
        public int year { get; set; }
        public List<Grade> grades { get; set; }
        [NonSerialized()] public double averageGrade;

        public Student() { }

        public Student(string surname, int year)
        {
            this.surname = surname;
            this.year = year;
            grades = new List<Grade>();
            averageGrade = 0.0;
        }

        public void AddGrade(string subject, int grade)
        {
            grades.Add(new Grade(subject, grade));
            averageGrade = (double)grades.ConvertAll<int>((a) => a.grade).Sum() / grades.Count();
        }

        public override string ToString()
        {
            string stud = surname + ": " + averageGrade;
            foreach (Grade grade in grades)
            {
                stud += "\n[" + grade.subject + ": " + grade.grade + "]";
            }
            stud += "\n";
            return stud;
        }

        [OnDeserialized()]
        internal void SetNonSerialized(StreamingContext context)
        {
            averageGrade = (double)grades.ConvertAll<int>((a) => a.grade).Sum() / grades.Count();
        }

        [Serializable()]
        public class Grade
        {
            public string subject;
            public int grade;

            public Grade() { }

            public Grade(string subject, int grade)
            {
                this.subject = subject;
                this.grade = grade;
            }
        }

    }
}
