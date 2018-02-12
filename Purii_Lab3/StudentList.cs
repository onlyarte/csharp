using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Purii_Lab3
{
    class StudentList
    {
        public List<Student> list;

        public StudentList()
        {
            list = new List<Student>();
        }

        public void InitList()
        {
            // create list of students
            list.Add(new Student("Karpenko", 1));
            list.Add(new Student("Kamarov", 1));
            list.Add(new Student("Lisko", 1));
            list.Add(new Student("Deruk", 1));
            list.Add(new Student("Kostenko", 1));

            // fill students' grades with random int 61-100
            Random rnd = new Random();
            string[] subjects = new string[] { "OOP", "FP", "LP", "ODM" };

            foreach (Student stud in list)
            {
                foreach (string subject in subjects)
                {
                    stud.AddGrade(subject, rnd.Next(61, 100));
                }
            }
        }

        public void EraseList()
        {
            list.Clear();
        }

        public void AddStudent(Student student)
        {
            list.Add(student);
        }

        public bool Save(string filePath)
        {
            try
            {
                Stream stream = File.Create(filePath);
                XmlSerializer XMLSer = new XmlSerializer(typeof(List<Student>));
                XMLSer.Serialize(stream, list);
                stream.Close();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Load(string filePath)
        {
            try
            {
                Stream stream = File.OpenRead(filePath);
                XmlSerializer XMLSer = new XmlSerializer(typeof(List<Student>));
                list = (List<Student>)XMLSer.Deserialize(stream);
                stream.Close();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public override string ToString()
        {
            string res = "\n\n";
            foreach (Student stud in list)
            {
                res += stud + "\n";
            }
            return res;
        }
    }
}
