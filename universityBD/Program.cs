using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

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
            bool seedUsed = false;
            while (run)
            {
                Console.WriteLine("\n###############################");
                Console.WriteLine("What you want to do?");
                Console.WriteLine("1. Search in database");
                Console.WriteLine("2. Add to database");
                Console.WriteLine("3. See the whole table");
                Console.WriteLine("4. See a specific view");
                if (!seedUsed) {
                    Console.WriteLine("9. Generate data");
                }
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
                    case 9:
                        if (!seedUsed)
                        {
                            Seed(database);
                        }
                        seedUsed = true;
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

        static void Seed(UniversityContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            int quantity = 100;
            // Generate departments
            var depNames = DepartmentNames.GetListOfNames();
            int depQ = Math.Min(quantity, depNames.Count());
            var selectedD = Pick<String>.UniqueRandomList(With.Exactly(depQ).Elements).From(depNames);
            var iterD = new Stack<String>(selectedD); 
            var departments = Builder<Department>.CreateListOfSize(depQ)
                .All()
                    .With(d => d.Name = iterD.Pop())
                .Build();
            foreach (var department in departments)
            {
                context.Add(department);
            }
            context.SaveChanges();

            // Generate courses
            /*int coursesPerDepartment = 10;
            var allCourses = new List<Course>();
            foreach (var dep in departments)
            {
                var courses = Builder<Course>.CreateListOfSize(Faker.RandomNumber.Next((int)(coursesPerDepartment*0.8), (int)(coursesPerDepartment *1.2)))
                .All()
                    .With(d => d.Department = dep)
                    .With(d => d.Name = Faker.Company.CatchPhrase())
                    .With(d => d.ECTS = Faker.RandomNumber.Next(1, 8))
                .Build();
                allCourses.AddRange(courses);
            }

            foreach (var course in allCourses)
            {
                context.Add(course);
            }
            context.SaveChanges();*/

            int coursesPerDepartment = 10;
            var courses = Builder<Course>.CreateListOfSize(Faker.RandomNumber.Next((int)(coursesPerDepartment * 0.8), (int)(coursesPerDepartment * 1.2)))
                .All()
                    .With(d => d.Department = Pick<Department>.RandomItemFrom(departments))
                    .With(d => d.Name = Faker.Company.CatchPhrase())
                    .With(d => d.ECTS = Faker.RandomNumber.Next(1,8))
                .Build();
            foreach (var course in courses)
            {
                context.Add(course);
            }
            context.SaveChanges();

            // Generate employees
            /*int employeesPerDepartment = 10;
            var allEmployees = new List<Employee>();
            foreach (var dep in departments)
            {
                var employees = Builder<Employee>.CreateListOfSize(employeesPerDepartment)
                .All()
                    .With(d => d.Name = Faker.Name.First())
                    .With(d => d.Surname = Faker.Name.Last())
                    .With(d => d.Address = Faker.Address.StreetAddress())
                    .With(d => d.City = Faker.Address.City())
                    .With(d => d.Country = Faker.Address.Country())
                    .With(d => d.Phone = Faker.Phone.Number())
                    .With(d => d.Email = Faker.Internet.Email())
                    .With(d => d.Salary = Faker.RandomNumber.Next(2000, 6000))
                    .With(d => d.Department = dep)
                .Build();
                allEmployees.AddRange(employees);
            }

            foreach (var employee in allEmployees)
            {
                context.Add(employee);
            }
            context.SaveChanges();*/

            int employeesPerDepartment = 10;
            var employees = Builder<Employee>.CreateListOfSize(Faker.RandomNumber.Next((int)(employeesPerDepartment * 0.8), (int)(employeesPerDepartment * 1.2)))
                .All()
                    .With(d => d.Name = Faker.Name.First())
                    .With(d => d.Surname = Faker.Name.Last())
                    .With(d => d.Address = Faker.Address.StreetAddress())
                    .With(d => d.City = Faker.Address.City())
                    .With(d => d.Country = Faker.Address.Country())
                    .With(d => d.Phone = Faker.Phone.Number())
                    .With(d => d.Email = Faker.Internet.Email())
                    .With(d => d.Salary = Faker.RandomNumber.Next(2000, 6000))
                    .With(d => d.Department = Pick<Department>.RandomItemFrom(departments))
                .Build();
            foreach (var employee in employees)
            {
                context.Add(employee);
            }
            context.SaveChanges();

            // Generate students
            var students = Builder<Student>.CreateListOfSize(quantity)
                .All()
                    .With(d => d.Name = Faker.Name.First())
                    .With(d => d.Surname = Faker.Name.Last())
                    .With(d => d.Address = Faker.Address.StreetAddress())
                    .With(d => d.City = Faker.Address.City())
                    .With(d => d.Country = Faker.Address.Country())
                    .With(d => d.Phone = Faker.Phone.Number())
                    .With(d => d.Email = Faker.Internet.Email())
                    .With(d => d.GraduationYear = Faker.RandomNumber.Next(2010, 2025))
                .Build();
            foreach (var student in students)
            {
                context.Add(student);
            }
            context.SaveChanges();

            // Generate sections
           /* var allSections = new List<Section>();
            foreach (var cou in courses)
            {
                int sectionsPerCourse = Faker.RandomNumber.Next(3, 10);
                int employeesPerCourse = Faker.RandomNumber.Next(1, 4);
                var selectedE = Pick<Employee>.UniqueRandomList(With.Exactly(employeesPerCourse).Elements).From(employees);
                var sections = Builder<Section>.CreateListOfSize(sectionsPerCourse)
                .All()
                    .With(d => d.Course = cou)
                    .With(d => d.Employee = selectedE[Faker.RandomNumber.Next(0, selectedE.Count())])
                    .With(d => d.Day = Faker.RandomNumber.Next(1, 5))
                    .With(d => d.StartTime = Faker.RandomNumber.Next(8, 19).ToString()
                        + ":" + (Faker.RandomNumber.Next(0, 3) * 15).ToString())
                    .With(d => d.Length = Faker.RandomNumber.Next(1, 4) * 45)
                    .With(d => d.Capacity = Faker.RandomNumber.Next(1, 4) * 10)
                .Build();
                allSections.AddRange(sections);
            }

            foreach (var section in allSections)
            {
                context.Add(section);
            }
            context.SaveChanges();*/

            var sections = Builder<Section>.CreateListOfSize(quantity)
                .All()
                    .With(d => d.Course = Pick<Course>.RandomItemFrom(courses))
                    .With(d => d.Employee = Pick<Employee>.RandomItemFrom(employees))
                    .With(d => d.Day = Faker.RandomNumber.Next(1, 5))
                    .With(d => d.StartTime = Faker.RandomNumber.Next(8, 19).ToString()
                        + ":" + (Faker.RandomNumber.Next(0, 3)*15).ToString())
                    .With(d => d.Length = Faker.RandomNumber.Next(1, 4) * 45)
                    .With(d => d.Capacity = Faker.RandomNumber.Next(1, 4) * 10)
                .Build();
            foreach (var section in sections)
            {
                context.Add(section);
            }
            context.SaveChanges();

            // Generate students grades
            List<Student> oldStuds = students.Where(s => DateTime.Now.Year - s.GraduationYear > -4).ToList();
            var allGrades = new List<Grade>();
            foreach (var s in oldStuds)
            {
                var studYearEnded = Math.Min(DateTime.Now.Year - s.GraduationYear + 5, 6);
                var coursesEnded = Math.Min(10*studYearEnded, courses.Count());
                var selectedC = Pick<Course>.UniqueRandomList(With.Exactly(coursesEnded).Elements).From(courses);
                var iter = new Stack<Course>(selectedC);
                var grades = Builder<Grade>.CreateListOfSize(Math.Min(Faker.RandomNumber.Next(8 * studYearEnded, 10 * studYearEnded), selectedC.Count()))
                    .All()
                        .With(d => d.StudentID = s.StudentID)
                        .With(d => d.CourseID = iter.Pop().CourseID)
                        .With(d => d.Year = Faker.RandomNumber.Next(1, studYearEnded))
                        .With(d => d.Semester = d.Year * 2 + Faker.RandomNumber.Next(0, 1))
                        .With(d => d.Score = Faker.RandomNumber.Next(2, 5))
                    .Build();
                allGrades.AddRange(grades);
            }

            foreach (var grade in allGrades)
            {
                context.Add(grade);
            }
            context.SaveChanges();

            // Generate students enrolments
            var allEnrolments = new List<Enrollment>();
            foreach (var s in students)
            {
                var selectedS = Pick<Section>.UniqueRandomList(With.Exactly(15).Elements).From(sections);
                var iter = new Stack<Section>(selectedS);
                var enrolments = Builder<Enrollment>.CreateListOfSize(Faker.RandomNumber.Next(5, 15))
                .All()
                    .With(d => d.SectionID = iter.Pop().SectionID)
                    .With(d => d.StudentID = s.StudentID)
                .Build();
                allEnrolments.AddRange(enrolments);
            }

            foreach (var enrolment in allEnrolments)
            {
                var section = (Section)context.Sections.Where(e => e.SectionID == enrolment.SectionID).FirstOrDefault();
                int freePlaces = section.Capacity - Section.CountStudsOnTmpDB(section, context);
                if (freePlaces > 0)
                {
                    context.Add(enrolment);
                    context.SaveChanges();
                }
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
