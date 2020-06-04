# University Database

## Projekt na laboratoria Bazy Danych.

##### Grupa:
  - Cyra Agata
  - ~~Nowakowski Karol~~ (urlop dziekański)
  - Siwek Patryk

##### Projekt:
  - Temat: Baza danych dla uczelni.
  - Serwer: wykorzystanie SQLite -- baza danych jest zintegrowana z aplikacją, która ma do niej bezpośredni dostęp (serwer nie jest potrzebny)
  - Technologie: .Net i Entity Framework
  - Link: https://github.com/kklgf/universityBD

## Current database schema
![Database schema](universityBD.jpg)

## User guide
![Available user options](user_options.png)

## Features
- Database schema implementation and creation [Patryk]
- Inteligent adding of new entities from terminal [Patryk]
- Inteligent searching of entities from terminal [Patryk]
- Viewing the grades of a particular student: function Student.StudentGrades(); [Agata]
- Viewing the courses of a particular employee: function Employee.EmployeesCourses(); [Agata]
- Viewing the ECTS points of a particular student: function Student.StudentsECTS(); [Agata]
- Viewing the "attendance list" (list of students at the particular section): function Section.AttendanceList(); [Agata]
- Viewing free places on a particular section: function Section.FreePlaces(); [Agata]
- Random data generation for whole database [Patryk]

## Data corectness insurance
- blocking student from having two classes at the same time: using the HasClassesAtTheTime(studentID, section) while enrolling for classes
- blocking the course overload (more students than capacity): while adding a new enrollment
- ensuring there is an existing row (in another table) while adding an attribute -- you need to choose from existing

## Dependencies
- Faker.Net:
    dotnet add package Faker.Net --version 1.3.77
- NBuilder:
    dotnet add package NBuilder --version 6.1.0

## Entire code breakdown
### File: Program.cs
#### Class: Program
> class responsible for interaction with user  
> catches the answers and switches appropriate functions depending on what user decides to do
##### static void Main(string[] args)
> displaying main menu right after the start of an application
##### static void Search(UniversityContext database)
> choice of a table and redirection into the matching function in the chosen class [example: Student.Search()]
##### static void SeeTable(UniversityContext database)
> choice of a table and redirection
##### static object Add()
> choice of a table and redirection
##### static void SpecificViews()
> choice of a view (out of 5 available) and redirection into the appropriate class with the function implementation
##### static void Seed(UniversityContext context)
> data generation using Faker
##### static void WrongAction()
> informing user about chosing incorrect value while deciding about actions
---------------------------------------------------
### File: Course.cs
#### Class: Course
> object class being mapped into the database table  

[Key]\
public int CourseID { get; set; }\
[ForeignKey("Department")]\
public int DepartmentID { get; set; }\
public Department Department { get; set; }\
public String Name { get; set; }\
public int ECTS { get; set; }\
##### public static Course NewCourse()
> constructor: to construct a new course, the existence of any Department row is required and specification of other course properties
##### public static void SeeAll()
> being called from program main function prepares the view of the whole courses table in the database
##### public static void print(IQueryable\<Course> query)
> being called from SeeAll() or Search() function displays the result of the query
##### public static void Search()
> responsible for searching in the courses table --- requires specification of a value by which user decides to search in the database
##### public static Course SearchToAdd()
> used for searching while adding a new row into a different table

---------------------------------------------------
### File: Department.cs
#### Class: Department
> object class being mapped into the database table

public int DepartmentID { get; set; }\
public String Name { get; set; }\
##### public static Department NewDepartment()
> constructor: requires specification of the new department name
##### public static void SeeAll()
> being called from program main function prepares the view of the whole departments table in the database
##### public static void print(IQueryable\<Department> query)
> being called from SeeAll() or Search() function displays the result of the query
##### public static void Search()
> responsible for searching in the department table --- requires specification of department name or ID value
##### public static Department SearchToAdd()
> used for searching while adding a new row into a different table
---------------------------------------------------
### File: Employee.cs
#### Class: Employee
> object class being mapped into the database table

public int EmployeeID { get; set; }\
public String Name { get; set; }\
public String Surname { get; set; }\
public String Address { get; set; }\
public String City { get; set; }\
public String Country { get; set; }\
public String Phone { get; set; }\
public String Email { get; set; }\
public int Salary { get; set; }\
\[ForeignKey("Department")]\
public int DepartmentID { get; set; }\
public Department Department { get; set; }\
##### public static void SeeAll()
> being called from program main function prepares the view of the whole employees table in the database
##### public static void print(IQueryable\<Employee> query)
> displays the result of the query from SeeAll() or Search()
##### public static Employee NewEmployee()
> constructor: to construct a new Employee an existence of any Department row is required (choice from already existing ones) and specification of other class properties
##### public static void Search()
> responsible for searching in the employee table --- requires specification of a known value
##### public static Employee SearchToAdd()
> used for seaching while adding a new row into a different table
##### public static void EmployeesCourses()
> prepares the view of a courses being teached by the chosen employee
> being called directly from the progam main function, first asks for the employee specification
> when the chosen employee is found, database query is being prepared taking advantage of the sections table which connects employees with courses:
> from all the sections where the chosen employee teaches courseID value is selected and then with the use of this ID, course name is displayed (selection from the courses table with the known courseID)
---------------------------------------------------
### File: Enrollment.cs
#### Class: Enrollment
> object class being mapped into the database table
> enrollment is a connection between student and a section (existence of both are required to create a new enrollment)
> the class does not have a key itself, each object of this class is identified by a combination of a SectionID and a StudentID as foreign keys

\[ForeignKey("Section")]\
public int SectionID { get; set; }\
public Section Section { get; set; }\
\[ForeignKey("Student")]\
public int StudentID { get; set; }\
public Student Student { get; set; }\
##### public static Enrollment NewEnrollment()
> creating a new enrollment: specifying the class properties by choice from already existing ones
> 1: choice of a section: for a successful enrollment any free places at the section is required --- comparing section capacity with Section.CountStudentsOnSection(section)
> 2. choice of a student: student can't have any other classes at the time of the new section (use of Student.HasClassesAtTheTime(studentID, section))
##### public static void SeeAll()
> prepares the view of the whole enrollments table in the database
##### public static void print(IQueryable\<Enrollment> query)
> displays the result of the query
##### public static void Search()
> used for searching in the enrollment table -- requires specification of a known value
##### public static Enrollment SearchToAdd()
> used for searching while adding a new row into a different table
---------------------------------------------------
### File: Grade.cs
#### Class: Grade
> object class being mapped into the database table
> to create a new grade, the existence of any row in Students and Courses table is required

\[ForeignKey("Course")]\
public int CourseID { get; set; }\
public Course Course { get; set; }\
\[ForeignKey("Student")]\
public int StudentID { get; set; }\
public Student Student { get; set; }\
public int Year { get; set; }\
public int Semester { get; set; }\
public int Score { get; set; }\
##### public static Grade NewGrade()
##### public static void SeeAll()
##### public static void print(IQueryable\<Grade> query)
##### public static void Search()
##### public static Grade SearchToAdd()
---------------------------------------------------
### File: Section.cs
#### Class: Section
> object class being mapped into the database table\
> section is a class given by a particular professor\
> constructing a section creates a connection between an employee (professor) and a course, so both of these must have existed before to create a new section

public int SectionID { get; set; }\
\[ForeignKey("Course")]\
public int CourseID { get; set; }\
public Course Course { get; set; }\
\[ForeignKey("Employees")]\
public int EmployeeID { get; set; }\
public Employee Employee { get; set; }\
public int Day { get; set; }\
public String StartTime { get; set; }\
public int Length { get; set; }\
public int Capacity { get; set; }\
##### public static Section NewSection()
##### public static void SeeAll()
##### public static void print(IQueryable\<Section> query)
##### public static void Search()
##### public static Section SearchToAdd()
##### public static int CountStudsOnTmpDB(Section section, UniversityContext context)
##### public static int CountStudentsOnSection(Section section)
##### public static void AttendanceList()
##### public static void FreePlaces()
> displays the number of free places available for section
> used while enrolling a student (freePlaces>0 is a required condition to make an enrollment)

---------------------------------------------------
### File: Student.cs
#### Class: Student
> object class being mapped into the database table

public int StudentID { get; set; }\
public String Name { get; set; }\
public String Surname { get; set; }\
public String Address { get; set; }\
public String City { get; set; }\
public String Country { get; set; }\
public String Phone { get; set; }\
public String Email { get; set; }\
public int GraduationYear { get; set; }
##### public static Student NewStudent()
##### public static void Search()
##### public static void print(IQueryable\<Student> query)
##### public static void SeeAll()
##### public static Student SearchToAdd()
##### public static bool HasClassesAtTheTime(int studentID, Section section)
> gives the answer whether student can enroll for a Section section or has another classes at the time: then enrollment is not available
##### public static void StudentsGrades()
> resonsible for creating a view with grades of a student
##### public static void StudentsECTS()
---------------------------------------------------
### File: UniversityContext.cs
#### Class: UniversityContext : DbContext
> class inheritating from DbContext responsible for creating a connection with a database and mapping classes into entities

public DbSet\<Course> Courses { get; set; }\
public DbSet\<Department> Departments { get; set; }\
public DbSet\<Employee> Employees { get; set; }\
public DbSet\<Enrollment> Enrollments { get; set; }\
public DbSet\<Grade> Grades { get; set; }\
public DbSet\<Section> Sections { get; set; }\
public DbSet\<Student> Students { get; set; }\

---------------------------------------------------

### File: DepartmentNames.cs
#### Class: DepartmentNames
> file used for data generation to make department names sound less awkward than the ones generated automatically
##### public static List\<String> GetListOfNames()
---------------------------------------------------
### File: WeekDays.cs
#### Class: WeekDays
> class used for parsing numbers stored in the database into the string values like 'Monday' to be displayed for the user
##### public static String Parse(int number)
