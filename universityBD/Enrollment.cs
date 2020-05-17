using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace universityBD
{
    class Enrollment
    {
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }
        [ForeignKey("Section")]
        public int SectionID { get; set; }
        public Section Section { get; set; }
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public Student Student { get; set; }

        public static Enrollment NewEnrollment()
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("\nYou need to specify those values.");
            Console.WriteLine("Course (chose from existing):");
            int CourseID = Course.SearchToAdd().CourseID;
            Console.WriteLine("SectionID (chose from existing):");
            bool SectionAvailable = false;
            bool run = true;
            int SectionID = 0;
            var query = from sections in database.Sections select sections;
            while(run)
            {
                SectionID = Section.SearchToAdd().SectionID;
                query = from sections in database.Sections
                            where sections.SectionID == SectionID
                            select sections;
                foreach(var item in query)
                { SectionAvailable = (item.Capacity > Section.CountStudentsOnSection(item)); }
                if (SectionAvailable)
                {
                    Console.WriteLine("Congratulations! This section is available!");
                    run = false;
                }
                else
                {
                    Console.WriteLine("This section does not have enough free places for you. Try another one!");
                    Console.WriteLine("Press 1 to continue, 0 to quit");
                    int tryAgain = int.Parse(Console.ReadLine());
                    if (tryAgain == 0) { run = false; }
                }
            }
            if(SectionAvailable)
            {
                Console.WriteLine("StudentID (chose from existing):");
                int StudentID = Student.SearchToAdd().StudentID;
                bool hasOtherClasses = false;
                foreach(var item in query)
                { hasOtherClasses = Student.HasClassesAtTheTime(StudentID, item);}
                if (hasOtherClasses) { Console.WriteLine("Another section at the time!"); }
                else
                { 
                    Enrollment enrollment = new Enrollment
                    {
                        CourseID = CourseID,
                        SectionID = SectionID,
                        StudentID = StudentID
                    };
                    Console.WriteLine("This enrollment has been successful!");
                    return enrollment;
                }
            }
            return null;
        }

        public static void Search()
        {
            System.Linq.IQueryable<universityBD.Enrollment> query = null;
            UniversityContext database = new UniversityContext();
            bool run = true;
            while (run)
            {
                run = false;
                Console.WriteLine("\nBy which value you want to search?");
                Console.WriteLine("1. Course");
                Console.WriteLine("2. Section");
                Console.WriteLine("3. Student");
                int action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        Console.WriteLine("Course (chose from existing):");
                        int CourseID = Course.SearchToAdd().CourseID;
                        query = database.Enrollments.Where(e => e.CourseID == CourseID);
                        break;
                    case 2:
                        Console.WriteLine("Section (chose from existing):");
                        int SectionID = Section.SearchToAdd().SectionID;
                        query = database.Enrollments.Where(e => e.SectionID == SectionID);
                        break;
                    case 3:
                        Console.WriteLine("Student (chose from existing):");
                        int StudentID = Student.SearchToAdd().StudentID;
                        query = database.Enrollments.Where(e => e.StudentID == StudentID);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("ERROR: CHOSEN INCORRECT VALUE");
                        run = true;
                        break;
                }
            }
            Console.WriteLine("Course, Profesor, Student");
            foreach (var item in query)
            {
                Console.WriteLine(item.Course.Name + ", " + item.Section.Employee.Name
                    + " " + item.Section.Employee.Surname + ", "
                     + item.Student.Name + " " + item.Student.Surname);
            }
        }
        public static Enrollment SearchToAdd()
        {
            UniversityContext database = new UniversityContext();
            Enrollment result = null;
            bool run = true;
            while (run)
            {
                Search();
                Console.WriteLine("Now chose Enrollment by inserting Course's, Section's and Student's ID. Write '0' to abort.");
                int idc = int.Parse(Console.ReadLine());
                int idse = int.Parse(Console.ReadLine());
                int ids = int.Parse(Console.ReadLine());
                if (idc == 0 || idse == 0 || ids == 0)
                {
                    result = null;
                    break;
                }
                else 
                {
                    var query = database.Enrollments.Where(e => e.CourseID == idc && e.SectionID == idse && e.StudentID == ids).FirstOrDefault(); ;
                    if (query != null)
                    {
                        run = false;
                        result = query;
                    }
                    else
                    {
                        Console.WriteLine("There is not Enrollment with specified ID's");
                        Console.WriteLine("Try again");
                    }
                    break;
                }
            }
            return result;
        }
    }
}
