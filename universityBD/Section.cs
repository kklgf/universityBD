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
            var course = database.Courses.Where(c => c.CourseID == CourseID).FirstOrDefault();
            Console.WriteLine("ProfesorID (chose from existing):");
            var potentialProfessors = database.Employees.Where(e => e.DepartmentID == course.DepartmentID);
            Employee.print(potentialProfessors);
            Console.WriteLine("\nEnter chosen professor ID: ");
            int ProfesorID = int.Parse(Console.ReadLine());
            bool professorFound = false;
            while (!professorFound)
            {
                var profesor = database.Employees.Where(e => e.EmployeeID == ProfesorID).FirstOrDefault();
                if (profesor.DepartmentID != course.DepartmentID)
                {
                    Console.WriteLine("THIS PROFESSOR DOES NOT TEACH IN THE DEPARTMENT YOU CHOSE!");
                    Console.WriteLine("Please choose another professor from the ones below:\n");
                    Employee.print(potentialProfessors);
                    ProfesorID = int.Parse(Console.ReadLine());
                }
                else professorFound = true;
            }
            Console.Write("Day [number]: ");
            int Day = int.Parse(Console.ReadLine());
            Console.Write("StartTime: ");
            String StartTime = Console.ReadLine();
            Console.Write("Length: ");
            int Length = int.Parse(Console.ReadLine());
            Console.Write("Capacity: ");
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

        public static void SeeAll()
        {
            Console.WriteLine("Showing all the SECTIONS in the database:");
            UniversityContext database = new UniversityContext();
            var query = database.Sections;
            print(query);
        }

        public static void print(IQueryable<Section> query)
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("\nID".PadRight(5) + "| " + "Course Name".PadRight(20) + "| " + "Profesor".PadRight(30) + "| " + "Day".PadRight(10)
                + "| " + "Start Time".PadRight(11) + "| " + "Length".PadRight(7) + "| " + "Capacity".PadRight(10) + "| " + "Free Places".PadRight(5));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            foreach (var item in query)
            {
                int freePlaces = item.Capacity - CountStudentsOnSection(item);
                var course = (Course)database.Courses.Where(e => e.CourseID == item.CourseID).FirstOrDefault();
                var employee = (Employee)database.Employees.Where(e => e.EmployeeID == item.ProfesorID).FirstOrDefault();
                Console.WriteLine(item.SectionID.ToString().PadRight(4) + "| " + course.Name.PadRight(20) + "| " + employee.Name.PadRight(14) + " " +
                    employee.Surname.PadRight(15) + "| " + WeekDays.Parse(item.Day).PadRight(10) + "| " + item.StartTime.PadRight(11) + "| "
                    + item.Length.ToString().PadRight(7) + "| " + item.Capacity.ToString().PadRight(10) + "| " + freePlaces.ToString().PadRight(5));
            }
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
                        Console.Write("ID: ");
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
                        Console.Write("Day: ");
                        int Day = int.Parse(Console.ReadLine());
                        query = database.Sections.Where(s => s.Day == Day);
                        break;
                    case 5:
                        Console.Write("StartTime: ");
                        String StartTime = Console.ReadLine();
                        query = database.Sections.Where(s => s.StartTime.Contains(StartTime));
                        break;
                    case 6:
                        Console.Write("Length: ");
                        int Length = int.Parse(Console.ReadLine());
                        query = database.Sections.Where(s => s.Length == Length);
                        break;
                    case 7:
                        Console.Write("Capacity: ");
                        int Capacity = int.Parse(Console.ReadLine());
                        query = database.Sections.Where(s => s.Capacity == Capacity);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("\n###############################");
                        Console.WriteLine("ERROR: CHOSEN INCORRECT VALUE");
                        Console.WriteLine("###############################\n");
                        run = true;
                        break;
                }
            }
            print(query);
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
                Console.Write("Your choice: ");
                int id = int.Parse(Console.ReadLine());
                switch (id)
                {
                    case 0:
                        result = null;
                        run = false;
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
            Console.Write("Your choice: ");
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
