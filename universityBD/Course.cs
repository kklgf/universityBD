using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace universityBD
{
    class Course
    {
        [Key]
        public int CourseID { get; set; }
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }
        public String Name { get; set; }
        public int ECTS { get; set; }

        public static Course NewCourse()
        {
            Console.WriteLine("\nYou need to specify those values.");
            Console.WriteLine("Department (chose from existing):");
            int DepartmentID = Department.SearchToAdd().DepartmentID;
            Console.Write("Enter the new course's NAME: ");
            String Name = Console.ReadLine();
            Console.Write("ECTS: ");
            int ECTS = int.Parse(Console.ReadLine());
            Course course = new Course
            {
                DepartmentID = DepartmentID,
                Name = Name,
                ECTS = ECTS
            };
            return course;
        }

        public static void SeeAll()
        {
            Console.WriteLine("Showing all the COURSES in the database:");
            UniversityContext database = new UniversityContext();
            var query = database.Courses;
            print(query);
        }

        public static void print(IQueryable<Course> query)
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("\nID".PadRight(5) + "| " + "Name".PadRight(15) + "| " + "ECTS".PadRight(5) + "| " + "Department Name".PadRight(15));
            Console.WriteLine("----------------------------------------------");
            foreach (var item in query)
            {
                var department = (Department)database.Departments.Where(e => e.DepartmentID == item.DepartmentID).FirstOrDefault();
                Console.WriteLine(item.CourseID.ToString().PadRight(4) + "| " + item.Name.PadRight(15) + "| " + item.ECTS.ToString().PadRight(5) +
                    "| " + department.Name.PadRight(15));
            }
        }

        public static void Search()
        {
            System.Linq.IQueryable<universityBD.Course> query = null;
            UniversityContext database = new UniversityContext();
            bool run = true;
            while (run)
            {
                run = false;
                Console.WriteLine("\nBy which value you want to search?");
                Console.WriteLine("1. ID");
                Console.WriteLine("2. Name");
                Console.WriteLine("3. ECTS");
                Console.WriteLine("4. Department");
                Console.WriteLine("0. Cancel");
                Console.WriteLine("\n###############################");
                Console.Write("Your choice: ");
                int action = int.Parse(Console.ReadLine());
                Console.WriteLine("###############################");
                query = database.Courses;
                switch (action)
                {
                    case 1:
                        Console.Write("ID: ");
                        int id = int.Parse(Console.ReadLine());
                        query = database.Courses.Where(c => c.CourseID == id);
                        break;
                    case 2:
                        Console.Write("Name: ");
                        String Name = Console.ReadLine();
                        query = database.Courses.Where(c => c.Name.Contains(Name));
                        break;
                    case 3:
                        Console.Write("ECTS: ");
                        int ECTS = int.Parse(Console.ReadLine());
                        query = database.Courses.Where(c => c.ECTS == ECTS);
                        break;
                    case 4:
                        Console.WriteLine("Department (chose from existing):");
                        int DepartmentID = Department.SearchToAdd().DepartmentID;
                        query = database.Courses.Where(c => c.DepartmentID == DepartmentID);
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

        public static Course SearchToAdd()
        {
            UniversityContext database = new UniversityContext();
            Course result = null;
            bool run = true;
            while (run)
            {
                Search();
                Console.WriteLine("Now chose Course by inserting it's ID. Write '0' to abort.");
                Console.Write("Your choice: ");
                int id = int.Parse(Console.ReadLine());
                switch (id)
                {
                    case 0:
                        result = null;
                        run = false;
                        break;
                    default:
                        var query = database.Courses.Where(c => c.CourseID == id).FirstOrDefault(); ;
                        if (query != null)
                        {
                            run = false;
                            result = query;
                        }
                        else
                        {
                            Console.WriteLine("There is not Course with ID = " + id);
                            Console.WriteLine("Try again");
                        }
                        break;
                }
            }
            return result;
        }
    }
}
