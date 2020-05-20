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
            Console.Write("Name: ");
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
            Console.WriteLine("\nID".PadRight(5) + "| " + "Name".PadRight(20));
            Console.WriteLine("-------------------------------");
            foreach (var item in query)
            {
                Console.WriteLine(item.DepartmentID.ToString().PadRight(4) + "| " + item.Name.PadRight(20));
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
                Console.WriteLine("0. Cancel");
                Console.WriteLine("\n###############################");
                Console.Write("Your choice: ");
                int action = int.Parse(Console.ReadLine());
                Console.WriteLine("###############################");
                switch (action)
                {
                    case 1:
                        Console.Write("Department ID: ");
                        int id = int.Parse(Console.ReadLine());
                        query = database.Departments.Where(d => d.DepartmentID == id);
                        break;
                    case 2:
                        Console.Write("Department Name: ");
                        String Name = Console.ReadLine();
                        query = database.Departments.Where(d => d.Name.Contains(Name));
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("\n###############################");
                        Console.WriteLine("ERROR: CHOSEN INCORRECT VALUE");
                        Console.WriteLine("###############################");
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
                Console.Write("Your choice: ");
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
