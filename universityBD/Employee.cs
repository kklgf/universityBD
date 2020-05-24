using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace universityBD
{
    class Employee
    {
        public int EmployeeID { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Country { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public int Salary { get; set; }
        [ForeignKey("Department")]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        public static void SeeAll()
        {
            Console.WriteLine("Showing all the EMPLOYEES in the database:");
            UniversityContext database = new UniversityContext();
            var query = database.Employees;
            print(query);
        }

        public static void print(IQueryable<Employee> query)
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("\nID".PadRight(5) + "| " + "Name".PadRight(13) + " " + "Surname".PadRight(15) +
                "| " + "Address".PadRight(27) + "| " + "City".PadRight(20) + "| " + "Country".PadRight(35) +
                "| " + "Phone".PadRight(22) + "| " + "Email".PadRight(39) + "| " + "Salary".PadRight(8) + "| " + "Department Name".PadRight(15));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------" +
                "----------------------------------------------------------------------------------------------------------------------------");
            foreach (var item in query)
            {
                var department = (Department)database.Departments.Where(e => e.DepartmentID == item.DepartmentID).FirstOrDefault();
                Console.WriteLine(item.EmployeeID.ToString().PadRight(4) + "| " + item.Name.PadRight(13) + " " + item.Surname.PadRight(15) + "| "
                    + item.Address.PadRight(27) + "| " + item.City.PadRight(20) + "| " + item.Country.PadRight(35) + "| " + item.Phone.ToString().PadRight(22)
                    + "| " + item.Email.PadRight(39) + "| " + item.Salary.ToString().PadRight(8) + "| " + department.Name.PadRight(15));
            }
        }

        public static Employee NewEmployee()
        {
            Console.WriteLine("\nAdding a new EMPLOYEE\nYou need to specify those values.");
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
            Console.Write("Salary: ");
            int Salary = int.Parse(Console.ReadLine());
            Console.WriteLine("Department (chose from existing):");
            int DepartmentID = Department.SearchToAdd().DepartmentID;
            Employee employee = new Employee
            {
                Name = Name,
                Surname = Surname,
                Address = Address,
                City = City,
                Country = Country,
                Phone = Phone,
                Email = Email,
                Salary = Salary,
                DepartmentID = DepartmentID
            };
            return employee;
        }

        public static void Search()
        {
            System.Linq.IQueryable<universityBD.Employee> query = null;
            UniversityContext database = new UniversityContext();
            bool run = true;
            while (run)
            {
                run = false;
                Console.WriteLine("\nBy which value you want to SEARCH?");
                Console.WriteLine("1. ID");
                Console.WriteLine("2. Name");
                Console.WriteLine("3. Surname");
                Console.WriteLine("4. Adress");
                Console.WriteLine("5. City");
                Console.WriteLine("6. Country");
                Console.WriteLine("7. Phone");
                Console.WriteLine("8. Email");
                Console.WriteLine("9. Salary");
                Console.WriteLine("10. Department");
                Console.WriteLine("O. Cancel");
                Console.WriteLine("###############################");
                Console.Write("Your choice: ");
                int action = int.Parse(Console.ReadLine());
                Console.WriteLine("###############################");
                switch (action)
                {
                    case 1:
                        Console.Write("Employee's ID: ");
                        int id = int.Parse(Console.ReadLine());
                        query = database.Employees.Where(e => e.EmployeeID == id);
                        break;
                    case 2:
                        Console.Write("Employee's Name: ");
                        String Name = Console.ReadLine();
                        query = database.Employees.Where(e => e.Name.Contains(Name));
                        break;
                    case 3:
                        Console.Write("Employee's Surname:");
                        String Surname = Console.ReadLine();
                        query = database.Employees.Where(e => e.Surname.Contains(Surname));
                        break;
                    case 4:
                        Console.Write("Employee's Address:");
                        String Address = Console.ReadLine();
                        query = database.Employees.Where(e => e.Address.Contains(Address));
                        break;
                    case 5:
                        Console.Write("Employee's City:");
                        String City = Console.ReadLine();
                        query = database.Employees.Where(e => e.City.Contains(City));
                        break;
                    case 6:
                        Console.Write("Employee's Country:");
                        String Country = Console.ReadLine();
                        query = database.Employees.Where(e => e.Country.Contains(Country));
                        break;
                    case 7:
                        Console.Write("Employee's Phone:");
                        String Phone = Console.ReadLine();
                        query = database.Employees.Where(e => e.Phone.Contains(Phone));
                        break;
                    case 8:
                        Console.Write("Employee's Email:");
                        String Email = Console.ReadLine();
                        query = database.Employees.Where(e => e.Email.Contains(Email));
                        break;
                    case 9:
                        Console.Write("Employee's Salary:");
                        int Salary = int.Parse(Console.ReadLine());
                        query = database.Employees.Where(e => e.Salary == Salary);
                        break;
                    case 10:
                        Console.Write("Employee's Department (chose from existing):");
                        int DepartmentID = Department.SearchToAdd().DepartmentID;
                        query = database.Employees.Where(e => e.DepartmentID == DepartmentID);
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
            print(query);
        }
        public static Employee SearchToAdd()
        {
            UniversityContext database = new UniversityContext();
            Employee result = null;
            bool run = true;
            while (run)
            {
                Search();
                Console.WriteLine("Now chose Employee by inserting it's ID. Write '0' to abort.");
                Console.Write("Your choice: ");
                int id = int.Parse(Console.ReadLine());
                switch (id)
                {
                    case 0:
                        result = null;
                        break;
                    default:
                        var query = database.Employees.Where(e => e.EmployeeID == id).FirstOrDefault(); ;
                        if (query != null)
                        {
                            run = false;
                            result = query;
                        }
                        else
                        {
                            Console.WriteLine("There is not Employee with ID = " + id);
                            Console.WriteLine("Try again");
                        }
                        break;
                }
            }
            return result;
        }

        public static void EmployeesCourses()
        {
            UniversityContext database = new UniversityContext();
            Console.WriteLine("First find the employee whose courses you'd like to see");
            Search();
            Console.WriteLine("Now choose the Employee by inserting it's ID. Write '0' to abort.");
            Console.Write("Your choice: ");
            int id = int.Parse(Console.ReadLine());
            var employee = database.Employees.Where(e => e.EmployeeID == id).FirstOrDefault();
            switch(id)
            {
                case 0:
                    break;
                default:
                    Console.WriteLine("");
                    var foundSection = from sections in database.Sections
                                        where sections.EmployeeID == employee.EmployeeID
                                        select sections;
                    foreach (var section in foundSection)
                    {
                       var course = (Course)database.Courses.Where(e => e.CourseID == section.CourseID).FirstOrDefault();
                       Console.WriteLine(employee.Name + " " + employee.Surname + ": " + course.Name);
                    }
                    break;
            }
        }
    }
}
