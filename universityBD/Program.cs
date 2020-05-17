using System;

namespace universityBD
{
    class Program
    {
        static void Main(string[] args)
        {
            double version = 0.1;
            Console.WriteLine("###############################");
            Console.WriteLine("UniversityDB version " + version);
            Console.WriteLine("###############################");
            UniversityContext database = new UniversityContext();
            // Student.StudentsGrades();
            // Employee.EmployeesCourses();
            // Student.StudentsECTS();
            // Section.AttendanceList();
            Section.FreePlaces();
            bool run = true;
            while (run)
            {
                Console.WriteLine("###############################");
                Console.WriteLine("\nWhat you want to do?");
                Console.WriteLine("1. Search in database");
                Console.WriteLine("2. Add to database");
                Console.WriteLine("O. Close");
                Console.WriteLine("###############################");
                int action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        Search(database);
                        break;
                    case 2:
                        database.Add(Add());
                        database.SaveChanges();
                        break;
                    case 0:
                        run = false;
                        break;
                    default:
                        WrongAction();
                        break;
                }
            }
        }

        static void Search(UniversityContext database)
        {
            throw new NotImplementedException();
            Console.WriteLine("###############################");
            Console.WriteLine("\nIn which table you want to search?");
            Console.WriteLine("1. Courses");
            Console.WriteLine("2. Departments");
            Console.WriteLine("3. Employees");
            Console.WriteLine("4. Enrollments");
            Console.WriteLine("5. Grades");
            Console.WriteLine("6. Sections");
            Console.WriteLine("7. Students");
            Console.WriteLine("O. Cancel");
            Console.WriteLine("###############################");
            int action = int.Parse(Console.ReadLine());
            switch (action)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 7:
                    Student.Search();
                    break;
                case 0:
                    break;
                default:
                    WrongAction();
                    break;
            }
        }
        static object Add()
        {
            Console.WriteLine("###############################");
            Console.WriteLine("\nTo which table you want to add row?");
            Console.WriteLine("1. Courses");
            Console.WriteLine("2. Departments");
            Console.WriteLine("3. Employees");
            Console.WriteLine("4. Enrollments");
            Console.WriteLine("5. Grades");
            Console.WriteLine("6. Sections");
            Console.WriteLine("7. Students");
            Console.WriteLine("O. Cancel");
            Console.WriteLine("###############################");
            int action = int.Parse(Console.ReadLine());
            switch (action)
            {
                case 1:
                    return Course.NewCourse();
                case 2:
                    return Department.NewDepartment();
                case 3:
                    return Employee.NewEmployee();
                case 4:
                    return Enrollment.NewEnrollment();
                case 5:
                    return Grade.NewGrade();
                case 6:
                    return Section.NewSection();
                case 7:
                    return Student.NewStudent();
                case 0:
                    break;
                default:
                    WrongAction();
                    break;
            }
            return null;

        }
        static void WrongAction()
        {
            Console.WriteLine("###############################");
            Console.WriteLine("ERROR: CHOSEN INCORRECT ACTION");
            Console.WriteLine("###############################");
        }

    }
}
