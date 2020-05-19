using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace universityBD
{
    class Department
    {
        public int DepartmentID { get; set; }
        public String Name { get; set; }

        public static Department NewDepartment()
        {
            Console.WriteLine("\nYou need to specify this value.");
            Console.WriteLine("Enter new department's NAME:");
            String Name = Console.ReadLine();
            Department department = new Department
            {
                Name = Name
            };
            return department;
        }

        public static void SeeAll()
        {
            Console.WriteLine("Showing all the DEPARTMENTS in the database:");
            UniversityContext database = new UniversityContext();
            var query = database.Departments;
            print(query);
        }

        public static void print(IQueryable<Department> query)
        {
            Console.WriteLine("ID".PadRight(4) + "| " + "Name".PadRight(15));
            Console.WriteLine("-------------------");
            foreach (var item in query)
            {
                Console.WriteLine(item.DepartmentID.ToString().PadRight(4) + "| " + item.Name.PadRight(15));
            }
        }

        public static void Search()
        {
            System.Linq.IQueryable<universityBD.Department> query = null;
            UniversityContext database = new UniversityContext();
            bool run = true;
            while (run)
            {
                run = false;
                Console.WriteLine("\nBy which value you want to search?");
                Console.WriteLine("1. ID");
                Console.WriteLine("2. Name");
                Console.WriteLine("O. Cancel");
                int action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        Console.WriteLine("ID:");
                        int id = int.Parse(Console.ReadLine());
                        query = database.Departments.Where(d => d.DepartmentID == id);
                        break;
                    case 2:
                        Console.WriteLine("Name:");
                        String Name = Console.ReadLine();
                        query = database.Departments.Where(d => d.Name.Contains(Name));
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("ERROR: CHOSEN INCORRECT VALUE");
                        run = true;
                        break;
                }
            }
            print(query);
        }
        public static Department SearchToAdd()
        {
            UniversityContext database = new UniversityContext();
            Department result = null;
            bool run = true;
            while (run)
            {
                Search();
                Console.WriteLine("Now chose Department by inserting it's ID. Write '0' to abort.");
                int id = int.Parse(Console.ReadLine());
                switch (id)
                {
                    case 0:
                        result = null;
                        break;
                    default:
                        var query = database.Departments.Where(d => d.DepartmentID == id).FirstOrDefault(); ;
                        if (query != null)
                        {
                            run = false;
                            result = query;
                        }
                        else
                        {
                            Console.WriteLine("There is not Department with ID = " + id);
                            Console.WriteLine("Try again");
                        }
                        break;
                }
            }
            return result;
        }
    }
}
