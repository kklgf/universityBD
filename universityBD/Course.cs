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
            Console.WriteLine("Name:");
            String Name = Console.ReadLine();
            Console.WriteLine("ECTS:");
            int ECTS = int.Parse(Console.ReadLine());
            Course course = new Course
            {
                DepartmentID = DepartmentID,
                Name = Name,
                ECTS = ECTS
            };
            return course;
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
                Console.WriteLine("O. Cancel");
                int action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        Console.WriteLine("ID:");
                        int id = int.Parse(Console.ReadLine());
                        query = database.Courses.Where(c => c.CourseID == id);
                        break;
                    case 2:
                        Console.WriteLine("Name:");
                        String Name = Console.ReadLine();
                        query = database.Courses.Where(c => c.Name.Contains(Name));
                        break;
                    case 3:
                        Console.WriteLine("ECTS:");
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
                        Console.WriteLine("ERROR: CHOSEN INCORRECT VALUE");
                        run = true;
                        break;
                }
            }
            Console.WriteLine("ID, Name, ECTS, Department");
            foreach (var item in query)
            {
                Console.WriteLine(item.CourseID + ", " + item.Name + ", " + item.ECTS + ", " + item.Department.Name);
            }
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
                int id = int.Parse(Console.ReadLine());
                switch (id)
                {
                    case 0:
                        result = null;
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
