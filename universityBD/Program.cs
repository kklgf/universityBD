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
            bool run = true;
            while (run)
            {
                Console.WriteLine("\n###############################");
                Console.WriteLine("What you want to do?");
                Console.WriteLine("1. Search in database");
                Console.WriteLine("2. Add to database");
                Console.WriteLine("3. See the whole table");
                Console.WriteLine("4. See a specific view");
                Console.WriteLine("0. Close");
                Console.WriteLine("###############################");
                Console.Write("Your choice: ");
                int action = int.Parse(Console.ReadLine());
                Console.WriteLine("###############################");
                switch (action)
                {
                    case 1:
                        Search(database);
                        break;
                    case 2:
                        object toBeAdded = Add();
                        if (toBeAdded != null)
                        {
                            database.Add(toBeAdded);
                            database.SaveChanges();
                        }
                        break;
                    case 3:
                        SeeTable(database);
                        break;
                    case 4:
                        SpecificViews();
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
            Console.WriteLine("\n###############################");
            Console.WriteLine("In which table you want to search?");
            Console.WriteLine("1. Courses");
            Console.WriteLine("2. Departments");
            Console.WriteLine("3. Employees");
            Console.WriteLine("4. Enrollments");
            Console.WriteLine("5. Grades");
            Console.WriteLine("6. Sections");
            Console.WriteLine("7. Students");
            Console.WriteLine("0. Cancel");
            Console.WriteLine("###############################");
            Console.Write("Your choice: ");
            int action = int.Parse(Console.ReadLine());
            Console.WriteLine("###############################");
            switch (action)
            {
                case 1:
                    Course.Search();
                    break;
                case 2:
                    Department.Search();
                    break;
                case 3:
                    Employee.Search();
                    break;
                case 4:
                    Enrollment.Search();
                    break;
                case 5:
                    Grade.Search();
                    break;
                case 6:
                    Section.Search();
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

        static void SeeTable(UniversityContext database)
        {
            Console.WriteLine("\n###############################");
            Console.WriteLine("Which table are you interested in?");
            Console.WriteLine("1. Courses");
            Console.WriteLine("2. Departments");
            Console.WriteLine("3. Employees");
            Console.WriteLine("4. Enrollments");
            Console.WriteLine("5. Grades");
            Console.WriteLine("6. Sections");
            Console.WriteLine("7. Students");
            Console.WriteLine("0. Cancel");
            Console.WriteLine("###############################");
            Console.Write("Your choice: ");
            int action = int.Parse(Console.ReadLine());
            Console.WriteLine("###############################");
            switch (action)
            {
                case 1:
                    Course.SeeAll();
                    break;
                case 2:
                    Department.SeeAll();
                    break;
                case 3:
                    Employee.SeeAll();
                    break;
                case 4:
                    Enrollment.SeeAll();
                    break;
                case 5:
                    Grade.SeeAll();
                    break;
                case 6:
                    Section.SeeAll();
                    break;
                case 7:
                    Student.SeeAll();
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
            Console.WriteLine("\n###############################");
            Console.WriteLine("To which table do you want to add row?");
            Console.WriteLine("1. Courses");
            Console.WriteLine("2. Departments");
            Console.WriteLine("3. Employees");
            Console.WriteLine("4. Enrollments");
            Console.WriteLine("5. Grades");
            Console.WriteLine("6. Sections");
            Console.WriteLine("7. Students");
            Console.WriteLine("O. Cancel");
            Console.WriteLine("###############################");
            Console.Write("Your choice: ");
            int action = int.Parse(Console.ReadLine());
            Console.WriteLine("###############################");
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

        static void SpecificViews()
        {
            Console.WriteLine("\n###############################");
            Console.WriteLine("What are you interested in?");
            Console.WriteLine("1. Grades of a particular student (search for student)");
            Console.WriteLine("2. ECTS points of a particular student (search for student)");
            Console.WriteLine("3. Courses of a particular employee (search for employee)");
            Console.WriteLine("4. Attendance list on a particular section (search for section)");
            Console.WriteLine("5. Free places on a particular section (search for section)");
            Console.WriteLine("0. Cancel");
            Console.WriteLine("###############################");
            Console.Write("Your choice: ");
            int action = int.Parse(Console.ReadLine());
            Console.WriteLine("###############################");
            switch (action)
            {
                case 1:
                    Student.StudentsGrades();
                    break;
                case 2:
                    Student.StudentsECTS();
                    break;
                case 3:
                    Employee.EmployeesCourses();
                    break;
                case 4:
                    Section.AttendanceList();
                    break;
                case 5:
                    Section.FreePlaces();
                    break;
                case 0:
                    break;
                default:
                    WrongAction();
                    break;
            }
        }

        static void WrongAction()
        {
            Console.WriteLine("\n###############################");
            Console.WriteLine("ERROR: CHOSEN INCORRECT ACTION");
            Console.WriteLine("###############################");
        }

    }
}
