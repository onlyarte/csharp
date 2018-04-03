using Purii_Lab9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Purii_Lab9.Data
{
    public class DbInitializer
    {
        public static void Initialize(CompanyContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Employees.Any())
            {
                Console.WriteLine("Found");
                return;   // DB has been seeded
            }

            var employees = new Employee[]
            {
            new Employee{ID=0,FullName="May", Position="CEO", Department="Main", Salary=9000.00},
            new Employee{ID=1,FullName="Lan", Position="CEO", Department="Main", Salary=10000.00},
            };
            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();
        }
    }
}
