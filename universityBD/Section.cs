using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace universityBD
{
    class Section
    {
        public int SectionID { get; set; }
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }
        [ForeignKey("Employees")]
        public int ProfesorID { get; set; }
        public Employee Employee { get; set; }
        public int Day { get; set; }
        public String StartTime { get; set; }
        public int Length { get; set; }
        public int Capacity { get; set; }

        public static Section NewSection()
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("\nYou need to specify those values.");
            Console.WriteLine("Course (chose from existing):");
            int CourseID = Course.SearchToAdd().CourseID;
            Console.WriteLine("ProfesorID (chose from existing):");
            int ProfesorID = Employee.SearchToAdd().EmployeeID;
            Console.WriteLine("Day:");
            int Day = int.Parse(Console.ReadLine());
            Console.WriteLine("StartTime:");
            String StartTime = Console.ReadLine();
            Console.WriteLine("Length:");
            int Length = int.Parse(Console.ReadLine());
            Console.WriteLine("Capacity:");
            int Capacity = int.Parse(Console.ReadLine());
            Section section = new Section
            {
                CourseID = CourseID,
                ProfesorID = ProfesorID,
                Day = Day,
                StartTime = StartTime,
                Length = Length,
                Capacity = Capacity
            };
            return section;
        }
        public static void Search()
        {
            System.Linq.IQueryable<universityBD.Section> query = null;
            UniversityContext database = new UniversityContext();
            bool run = true;
            while (run)
            {
                run = false;
                Console.WriteLine("\nBy which value you want to search?");
                Console.WriteLine("1. ID");
                Console.WriteLine("2. Course");
                Console.WriteLine("3. Profesor");
                Console.WriteLine("4. Day");
                Console.WriteLine("5. StartTime");
                Console.WriteLine("6. Length");
                Console.WriteLine("7. Capacity");
                Console.WriteLine("O. Cancel");
                int action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        Console.WriteLine("ID:");
                        int id = int.Parse(Console.ReadLine());
                        query = database.Sections.Where(s => s.SectionID == id);
                        break;
                    case 2:
                        Console.WriteLine("Course (chose from existing):");
                        int CourseID = Course.SearchToAdd().CourseID;
                        query = database.Sections.Where(s => s.CourseID == CourseID);
                        break;
                    case 3:
                        Console.WriteLine("Profesor (chose from existing):");
                        int EmployeeID = Employee.SearchToAdd().EmployeeID;
                        query = database.Sections.Where(s => s.ProfesorID == EmployeeID);
                        break;
                    case 4:
                        Console.WriteLine("Day:");
                        int Day = int.Parse(Console.ReadLine());
                        query = database.Sections.Where(s => s.Day == Day);
                        break;
                    case 5:
                        Console.WriteLine("StartTime:");
                        String StartTime = Console.ReadLine();
                        query = database.Sections.Where(s => s.StartTime.Contains(StartTime));
                        break;
                    case 6:
                        Console.WriteLine("Length:");
                        int Length = int.Parse(Console.ReadLine());
                        query = database.Sections.Where(s => s.Length == Length);
                        break;
                    case 7:
                        Console.WriteLine("Capacity:");
                        int Capacity = int.Parse(Console.ReadLine());
                        query = database.Sections.Where(s => s.Capacity == Capacity);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("ERROR: CHOSEN INCORRECT VALUE");
                        run = true;
                        break;
                }
            }
            Console.WriteLine("ID, Course, Profesor, Day, StartTime, Length, Capacity");
            foreach (var item in query)
            {
                var course = (Course)database.Courses.Where(e => e.CourseID == item.CourseID).FirstOrDefault();
                var employee = (Employee)database.Employees.Where(e => e.EmployeeID == item.ProfesorID).FirstOrDefault();
                Console.WriteLine(item.SectionID + ", " + course.Name + ", " + employee.Name + " " + employee.Surname +
                        ", " + item.Day + ", " + item.StartTime + ", " + item.Length + ", " + item.Capacity);
            }
        }
        public static Section SearchToAdd()
        {
            UniversityContext database = new UniversityContext();
            Section result = null;
            bool run = true;
            while (run)
            {
                Search();
                Console.WriteLine("Now chose Section by inserting it's ID. Write '0' to abort.");
                int id = int.Parse(Console.ReadLine());
                switch (id)
                {
                    case 0:
                        result = null;
                        break;
                    default:
                        var query = database.Sections.Where(s => s.SectionID == id).FirstOrDefault(); ;
                        if (query != null)
                        {
                            run = false;
                            result = query;
                        }
                        else
                        {
                            Console.WriteLine("There is not Section with ID = " + id);
                            Console.WriteLine("Try again");
                        }
                        break;
                }
            }
            return result;
        }


        public static int CountStudentsOnSection(Section section)
        {
            UniversityContext database = new UniversityContext();
            int result = 0;
            
            var foundEnrollments = from enrollments in database.Enrollments
                                   where enrollments.SectionID == section.SectionID
                                   select enrollments;
            foreach (var enrollment in foundEnrollments) { result++; }
            return result;
        }
        public static void AttendanceList()
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("First find the Section you're interested in: ");
            Search();
            Console.WriteLine("Now choose the Section by inserting it's ID. Write '0' to abort.");
            int id = int.Parse(Console.ReadLine());
            switch (id)
            {
                case 0:
                    break;
                default:
                    var section = (Section)database.Sections.Where(e => e.SectionID == id).FirstOrDefault();
                    var foundEnrollments = from enrollments in database.Enrollments
                                           where enrollments.SectionID == section.SectionID
                                           select enrollments;
                    foreach (var enrollment in foundEnrollments)
                    {
                        var student = (Student)database.Students.Where(e => e.StudentID == enrollment.StudentID).FirstOrDefault();
                        Console.WriteLine(student.Name + " " + student.Surname);
                    }
                    break;
            }
        }

        public static void FreePlaces()
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("First find the Section you're interested in: ");
            Search();
            Console.WriteLine("Now choose the Section by inserting it's ID. Write '0' to abort.");
            int id = int.Parse(Console.ReadLine());
            switch(id)
            {
                case 0:
                    break;
                default:
                    var section = (Section)database.Sections.Where(e => e.SectionID == id).FirstOrDefault();
                    int freePlaces = section.Capacity - CountStudentsOnSection(section);
                    Console.WriteLine("There are " + freePlaces + " free places on this section.");
                    break;
            }
        }
    }
}
