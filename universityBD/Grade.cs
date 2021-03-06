﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace universityBD
{
    class Grade
    {
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public Student Student { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
        public int Score { get; set; }

        public static Grade NewGrade()
        {
            Console.WriteLine("\nYou need to specify those values.");
            Console.WriteLine("CourseID (chose from existing):");
            int CourseID = Course.SearchToAdd().CourseID;
            Console.WriteLine("StudentID (chose from existing):");
            int StudentID = Student.SearchToAdd().StudentID;
            Console.WriteLine("Year:");
            int Year = int.Parse(Console.ReadLine());
            Console.WriteLine("Semester:");
            int Semester = int.Parse(Console.ReadLine());
            Console.WriteLine("Score:");
            int Score = int.Parse(Console.ReadLine());
            Grade grade = new Grade
            {
                CourseID = CourseID,
                StudentID = StudentID,
                Year = Year,
                Semester = Semester,
                Score = Score
            };
            return grade;
        }

        public static void SeeAll()
        {
            Console.WriteLine("Showing all the GRADES in the database:");
            UniversityContext database = new UniversityContext();
            var query = database.Grades;
            print(query);
        }

        public static void print(IQueryable<Grade> query)
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("");
            Console.WriteLine("Course".PadRight(50) + "| " + "Student".PadRight(30) + "| " + "Year".PadRight(6) + "| " + "Semester".PadRight(10) + "| " + "Score".PadRight(7));
            Console.WriteLine("---------------------------------------------------------------------" +
                "---------------------------------------------");
            foreach (var item in query)
            {
                var student = (Student)database.Students.Where(e => e.StudentID == item.StudentID).FirstOrDefault();
                var course = (Course)database.Courses.Where(e => e.CourseID == item.CourseID).FirstOrDefault();
                Console.WriteLine(course.Name.PadRight(50) + "| " + student.Name.PadRight(14) + " " + student.Surname.PadRight(15) + "| "
                              + item.Year.ToString().PadRight(6) + "| " + item.Semester.ToString().PadRight(10) + "| " + item.Score.ToString().PadRight(7));
            }
        }

        public static void Search()
        {
            System.Linq.IQueryable<universityBD.Grade> query = null;
            UniversityContext database = new UniversityContext();
            bool run = true;
            while (run)
            {
                run = false;
                Console.WriteLine("\nBy which value you want to search?");
                Console.WriteLine("1. Course");
                Console.WriteLine("2. Student");
                Console.WriteLine("3. Year");
                Console.WriteLine("4. Semester");
                Console.WriteLine("5. Score");
                int action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        Console.WriteLine("Course (chose from existing):");
                        int CourseID = Course.SearchToAdd().CourseID;
                        query = database.Grades.Where(w => w.CourseID == CourseID);
                        break;
                    case 2:
                        Console.WriteLine("Student (chose from existing):");
                        int StudentID = Student.SearchToAdd().StudentID;
                        query = database.Grades.Where(w => w.StudentID == StudentID);
                        break;
                    case 3:
                        Console.WriteLine("Year:");
                        int Year = int.Parse(Console.ReadLine());
                        query = database.Grades.Where(w => w.Year == Year);
                        break;
                    case 4:
                        Console.WriteLine("Semester:");
                        int Semester = int.Parse(Console.ReadLine());
                        query = database.Grades.Where(w => w.Semester == Semester);
                        break;
                    case 5:
                        Console.WriteLine("Score:");
                        int Score = int.Parse(Console.ReadLine());
                        query = database.Grades.Where(w => w.Score == Score);
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
        public static Grade SearchToAdd()
        {
            UniversityContext database = new UniversityContext();
            Grade result = null;
            bool run = true;
            while (run)
            {
                Search();
                Console.WriteLine("Now chose Enrollment by inserting Course's and Student's ID. Write '0' to abort.");
                int idc = int.Parse(Console.ReadLine());
                int ids = int.Parse(Console.ReadLine());
                if (idc == 0 || ids == 0)
                {
                    result = null;
                    break;
                }
                else
                {
                    var query = database.Grades.Where(e => e.CourseID == idc && e.StudentID == ids).FirstOrDefault(); ;
                    if (query != null)
                    {
                        run = false;
                        result = query;
                    }
                    else
                    {
                        Console.WriteLine("There is not Grade with specified ID's");
                        Console.WriteLine("Try again");
                    }
                    break;
                }
            }
            return result;
        }
    }
}
