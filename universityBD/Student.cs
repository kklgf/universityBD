using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace universityBD
{
    class Student
    {
        public int StudentID { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public int GraduationYear { get; set; }


        public static Student NewStudent()
        {
            Console.WriteLine("\n###############################");
            Console.WriteLine("You need to specify those values.");
            Console.Write("Name: ");
            String Name = Console.ReadLine();
            Console.Write("Surname: ");
            String Surname = Console.ReadLine();
            Console.Write("Address: ");
            String Address = Console.ReadLine();
            Console.Write("City: ");
            String City = Console.ReadLine();
            Console.Write("Country: ");
            String Country = Console.ReadLine();
            Console.Write("Phone: ");
            String Phone = Console.ReadLine();
            Console.Write("Email: ");
            String Email = Console.ReadLine();
            Console.Write("GraduationYear: ");
            int GraduationYear = int.Parse(Console.ReadLine());
            Student student = new Student
            {
                Name = Name,
                Surname = Surname,
                Address = Address,
                City = City,
                Country = Country,
                Phone = Phone,
                Email = Email,
                GraduationYear = GraduationYear
            };
            return student;
        }
        public static void Search()
        {
            System.Linq.IQueryable<universityBD.Student> query = null;
            UniversityContext database = new UniversityContext();
            bool run = true;
            while (run)
            {
                run = false;
                Console.WriteLine("\n###############################");
                Console.WriteLine("By which value you want to search?");
                Console.WriteLine("1. ID");
                Console.WriteLine("2. Name");
                Console.WriteLine("3. Surname");
                Console.WriteLine("4. Address");
                Console.WriteLine("5. City");
                Console.WriteLine("6. Country");
                Console.WriteLine("7. Phone");
                Console.WriteLine("8. Email");
                Console.WriteLine("9. Graduation year");
                Console.WriteLine("0. Cancel");
                Console.WriteLine("###############################");
                Console.Write("Your choice: ");
                int action = int.Parse(Console.ReadLine());
                Console.WriteLine("###############################");
                switch (action)
                {
                    case 1:
                        Console.Write("ID: ");
                        int id = int.Parse(Console.ReadLine());
                        query = database.Students.Where(s => s.StudentID == id);
                        break;
                    case 2:
                        Console.Write("Name: ");
                        String Name = Console.ReadLine();
                        query = database.Students.Where(s => s.Name.Contains(Name));
                        break;
                    case 3:
                        Console.Write("Surname: ");
                        String Surname = Console.ReadLine();
                        query = database.Students.Where(s => s.Surname.Contains(Surname));
                        break;
                    case 4:
                        Console.Write("Address: ");
                        String Address = Console.ReadLine();
                        query = database.Students.Where(s => s.Address.Contains(Address));
                        break;
                    case 5:
                        Console.Write("City: ");
                        String City = Console.ReadLine();
                        query = database.Students.Where(s => s.City.Contains(City));
                        break;
                    case 6:
                        Console.Write("Country: ");
                        String Country = Console.ReadLine();
                        query = database.Students.Where(s => s.Country.Contains(Country));
                        break;
                    case 7:
                        Console.Write("Phone: ");
                        String Phone = Console.ReadLine();
                        query = database.Students.Where(s => s.Phone.Contains(Phone));
                        break;
                    case 8:
                        Console.Write("Email: ");
                        String Email = Console.ReadLine();
                        query = database.Students.Where(s => s.Email.Contains(Email));
                        break;
                    case 9:
                        Console.Write("Graduation year: ");
                        int GraduationYear = int.Parse(Console.ReadLine());
                        query = database.Students.Where(s => s.GraduationYear == GraduationYear);
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
            Console.WriteLine("###############################");
            print(query);
        }


        public static void print(IQueryable<Student> query)
        {
            Console.WriteLine("\nID".PadRight(5) + "| " + "Name".PadRight(15) + "| " + "Surname".PadRight(15) + "| " + "Address".PadRight(30) +
                "| " + "City".PadRight(20) + "| " + "Country".PadRight(45) + "| " + "Phone".PadRight(25) + "| " + "Email".PadRight(40) +
                "| " + "Graduation Year".PadRight(20));
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------" +
                "-----------------------------------------------------------------------------------------------------------------");
            foreach (var item in query)
            {
                Console.WriteLine(item.StudentID.ToString().PadRight(4) + "| " + item.Name.PadRight(15) + "| " + item.Surname.PadRight(15)
                     + "| " + item.Address.PadRight(30) + "| " + item.City.PadRight(20) + "| " + item.Country.PadRight(45) + "| " + item.Phone.PadRight(25)
                      + "| " + item.Email.PadRight(40) + "| " + item.GraduationYear.ToString().PadRight(20));
            }
        }

        public static void SeeAll()
        {
            Console.WriteLine("Showing all the STUDENTS in the database:");
            UniversityContext database = new UniversityContext();
            var query = database.Students;
            print(query);
        }

        public static Student SearchToAdd()
        {
            UniversityContext database = new UniversityContext();
            Student result = null;
            bool run = true;
            while (run)
            {
                Search();
                Console.Write("Now chose Student by inserting it's ID / Write '0' to abort: ");
                int id = int.Parse(Console.ReadLine());
                switch (id)
                {
                    case 0:
                        result = null;
                        break;
                    default:
                        var query = database.Students.Where(s => s.StudentID == id).FirstOrDefault(); ;
                        if (query != null)
                        {
                            run = false;
                            result = query;
                        }
                        else
                        {
                            Console.WriteLine("There is not Student with ID = " + id);
                            Console.WriteLine("Try again");
                        }
                        break;
                }
            }
            return result;
        }

        public static bool HasClassesAtTheTime(int studentID, Section section)
        {
            UniversityContext database = new UniversityContext();
            var foundSections = from sections in database.Sections
                                join enrollments in database.Enrollments
                                on sections.SectionID equals enrollments.SectionID
                                where enrollments.StudentID == studentID
                                select sections;
            foreach(var foundSection in foundSections)
            {
                if(foundSection.Day == section.Day && foundSection.StartTime == section.StartTime)
                { return true; }
            }
            return false;
        }

        public static void StudentsGrades()
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("\n###############################");
            Console.WriteLine("\nFirst find the student whose grades you'd like to see");
            Search();
            Console.WriteLine("\n###############################");
            Console.Write("Choose selected Student by inserting it's ID / Write '0' to abort: ");
            int id = int.Parse(Console.ReadLine());
            switch(id)
            {
                case 0:
                    break;
                default:
                    var student = (Student)database.Students.Where(e => e.StudentID == id).FirstOrDefault();
                    var query = from courses in database.Courses
                                join grades in database.Grades
                                on courses.CourseID equals grades.CourseID
                                join students in database.Students
                                on grades.StudentID equals students.StudentID
                                where students.StudentID == id
                                select grades;
                    Console.WriteLine("\nName".PadRight(16) + "| " + "Surname".PadRight(15) + "| " + "Course Name".PadRight(15) +
                                                "| " + "SCORE".PadRight(10));
                    Console.WriteLine("-----------------------------------------------------------------");
                    foreach (var item in query)
                    {
                        var foundCourse = from courses in database.Courses
                                          where courses.CourseID == item.CourseID
                                          select courses;
                        foreach (var course in foundCourse)
                        {
                            Console.WriteLine(student.Name.PadRight(15) + "| " + student.Surname.PadRight(15)
                                     + "| " + course.Name.PadRight(15) + "| " + item.Score.ToString().PadRight(10));
                        }
                    }
                    break;
            }
        }

        public static void StudentsECTS()
        {
            UniversityContext database = new UniversityContext();
            int result = 0;
            Console.WriteLine("First find the student whose ECTS points you'd like to see");
            Search();
            Console.Write("Choose selected Student by inserting it's ID / Write '0' to abort: ");
            int id = int.Parse(Console.ReadLine());
            switch (id)
            {
                case 0:
                    break;
                default:
                    Console.Write("Choose the semeester you are interested in: ");
                    int semester = int.Parse(Console.ReadLine());
                    var query = from courses in database.Courses
                                join grades in database.Grades
                                on courses.CourseID equals grades.CourseID
                                join students in database.Students
                                on grades.StudentID equals students.StudentID
                                where students.StudentID == id
                                select grades;
                    foreach (var item in query)
                    {
                        if (item.Score > 2)
                        {
                            var passedCourse = (Course)database.Courses.Where(e => e.CourseID == item.CourseID).FirstOrDefault();
                            result += passedCourse.ECTS;
                        }
                    }
                    Console.WriteLine("\nCollected ECTS points: " + result);
                    break;
            }
        }
    }
}
