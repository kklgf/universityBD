﻿using Microsoft.EntityFrameworkCore.Storage;
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
        [ForeignKey("Section")]
        public int SectionID { get; set; }
        public Section Section { get; set; }
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public Student Student { get; set; }

        public static Enrollment NewEnrollment()
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("\nAdding a new ENROLLMENT\nYou need to specify those values.");
            Console.WriteLine("Course (chose from existing):");
            int CourseID = Course.SearchToAdd().CourseID;
            Console.WriteLine("SectionID (chose from existing):");
            var query = from sections in database.Sections where sections.CourseID == CourseID select sections;
            Section.print(query);
            bool SectionAvailable = false;
            bool run = true;
            int SectionID = 0;
            while(run)
            {
                Console.Write("Put section ID here: ");
                SectionID = int.Parse(Console.ReadLine());
                query = from sections in database.Sections
                            where sections.SectionID == SectionID
                            select sections;
                foreach(var item in query)
                { SectionAvailable = (item.Capacity > Section.CountStudentsOnSection(item, database)); }
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
                        SectionID = SectionID,
                        StudentID = StudentID
                    };
                    Console.WriteLine("This enrollment has been successful!");
                    return enrollment;
                }
            }
            return null;
        }

        public static void SeeAll()
        {
            Console.WriteLine("Showing all the ENROLLMENTS in the database:");
            UniversityContext database = new UniversityContext();
            var query = database.Enrollments;
            print(query);
        }

        public static void print(IQueryable<Enrollment> query)
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("");
            Console.WriteLine("Course Name".PadRight(50) + "| " + "Profesor".PadRight(30) + "| " + "Student".PadRight(30));
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            foreach (var item in query)
            {
                var student = (Student)database.Students.Where(e => e.StudentID == item.StudentID).FirstOrDefault();
                var section = (Section)database.Sections.Where(e => e.SectionID == item.SectionID).FirstOrDefault();
                var course = (Course)database.Courses.Where(e => e.CourseID == section.CourseID).FirstOrDefault();
                var employee = (Employee)database.Employees.Where(e => e.EmployeeID == section.EmployeeID).FirstOrDefault();
                Console.WriteLine(course.Name.PadRight(50) + "| " + employee.Name.PadRight(14)
                    + " " + employee.Surname.PadRight(15) + "| " + student.Name.PadRight(14) + " " + student.Surname.PadRight(15));
            }
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
                Console.WriteLine("1. Section");
                Console.WriteLine("2. Student");
                int action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        Console.WriteLine("Section (chose from existing):");
                        int SectionID = Section.SearchToAdd().SectionID;
                        query = database.Enrollments.Where(e => e.SectionID == SectionID);
                        break;
                    case 2:
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
            print(query);
        }
        public static Enrollment SearchToAdd()
        {
            UniversityContext database = new UniversityContext();
            Enrollment result = null;
            bool run = true;
            while (run)
            {
                Search();
                Console.WriteLine("Now chose Enrollment by inserting Section's and Student's ID. Write '0' to abort.");
                int idse = int.Parse(Console.ReadLine());
                int ids = int.Parse(Console.ReadLine());
                if (idse == 0 || ids == 0)
                {
                    result = null;
                    break;
                }
                else 
                {
                    var query = database.Enrollments.Where(e => e.SectionID == idse && e.StudentID == ids).FirstOrDefault(); ;
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
