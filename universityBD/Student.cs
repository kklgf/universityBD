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
        public int Phone { get; set; }
        public String Email { get; set; }
        public int GraduationYear { get; set; }


        public static Student NewStudent()
        {
            Console.WriteLine("\nYou need to specify those values.");
            Console.WriteLine("Name:");
            String Name = Console.ReadLine();
            Console.WriteLine("Surname:");
            String Surname = Console.ReadLine();
            Console.WriteLine("Address:");
            String Address = Console.ReadLine();
            Console.WriteLine("City:");
            String City = Console.ReadLine();
            Console.WriteLine("Country:");
            String Country = Console.ReadLine();
            Console.WriteLine("Phone:");
            int Phone = int.Parse(Console.ReadLine());
            Console.WriteLine("Email:");
            String Email = Console.ReadLine();
            Console.WriteLine("GraduationYear:");
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
                Console.WriteLine("\nBy which value you want to search?");
                Console.WriteLine("1. ID");
                Console.WriteLine("2. Name");
                Console.WriteLine("3. Surname");
                Console.WriteLine("4. Address");
                Console.WriteLine("5. City");
                Console.WriteLine("6. Country");
                Console.WriteLine("7. Phone");
                Console.WriteLine("8. Email");
                Console.WriteLine("9. Graduation year");
                Console.WriteLine("O. Cancel");
                int action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        Console.WriteLine("ID:");
                        int id = int.Parse(Console.ReadLine());
                        query = database.Students.Where(s => s.StudentID == id);
                        break;
                    case 2:
                        Console.WriteLine("Name:");
                        String Name = Console.ReadLine();
                        query = database.Students.Where(s => s.Name.Contains(Name));
                        break;
                    case 3:
                        Console.WriteLine("Surname:");
                        String Surname = Console.ReadLine();
                        query = database.Students.Where(s => s.Surname.Contains(Surname));
                        break;
                    case 4:
                        Console.WriteLine("Address:");
                        String Address = Console.ReadLine();
                        query = database.Students.Where(s => s.Address.Contains(Address));
                        break;
                    case 5:
                        Console.WriteLine("City:");
                        String City = Console.ReadLine();
                        query = database.Students.Where(s => s.City.Contains(City));
                        break;
                    case 6:
                        Console.WriteLine("Country:");
                        String Country = Console.ReadLine();
                        query = database.Students.Where(s => s.Country.Contains(Country));
                        break;
                    case 7:
                        Console.WriteLine("Phone:");
                        int Phone = int.Parse(Console.ReadLine());
                        query = database.Students.Where(s => s.Phone == Phone);
                        break;
                    case 8:
                        Console.WriteLine("Email:");
                        String Email = Console.ReadLine();
                        query = database.Students.Where(s => s.Email.Contains(Email));
                        break;
                    case 9:
                        Console.WriteLine("Graduation year:");
                        int GraduationYear = int.Parse(Console.ReadLine());
                        query = database.Students.Where(s => s.GraduationYear == GraduationYear);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("ERROR: CHOSEN INCORRECT VALUE");
                        run = true;
                        break;
                }
            }
            Console.WriteLine("ID, Name, Surname, Address, City, Country, Phone, Email, Graduation year");
            foreach (var item in query)
            {
                Console.WriteLine(item.StudentID + ", " + item.Name + ", " + item.Surname
                     + ", " + item.Address + ", " + item.City + ", " + item.Country + ", " + item.Phone
                      + ", " + item.Email + ", " + item.GraduationYear);
            }
        }

        public static void SeeAll()
        {
            Console.WriteLine("Showing all the STUDENTS in the database:");
            UniversityContext database = new UniversityContext();
            var query = database.Students;
            Console.WriteLine("ID | Name | Surname | Address | City | Country | Phone | Email | Graduation Year");
            foreach (var item in query)
            {
                Console.WriteLine(item.StudentID + ", " + item.Name + ", " + item.Surname
                     + ", " + item.Address + ", " + item.City + ", " + item.Country + ", " + item.Phone
                      + ", " + item.Email + ", " + item.GraduationYear);
            }
        }

        public static Student SearchToAdd()
        {
            UniversityContext database = new UniversityContext();
            Student result = null;
            bool run = true;
            while (run)
            {
                Search();
                Console.WriteLine("Now chose Student by inserting it's ID. Write '0' to abort.");
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
            Console.WriteLine("First find the student whose grades you'd like to see");
            Search();
            Console.WriteLine("Choose selected Student by inserting it's ID. Write '0' to abort.");
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
                    foreach(var item in query)
                    {
                        var foundCourse = from courses in database.Courses
                                          where courses.CourseID == item.CourseID
                                          select courses;
                        foreach (var course in foundCourse)
                        { Console.WriteLine(student.Name + " " + student.Surname + ", Course Name: " + course.Name + ", SCORE: " + item.Score); }
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
            Console.WriteLine("Choose selected Student by inserting it's ID. Write '0' to abort.");
            int id = int.Parse(Console.ReadLine());
            switch (id)
            {
                case 0:
                    break;
                default:
                    Console.WriteLine("choose the semeester you are interested in: ");
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
                    Console.WriteLine("Collected ECTS points: " + result);
                    break;
            }
        }
    }
}
