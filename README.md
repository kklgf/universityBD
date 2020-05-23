# University Database

## Projekt na laboratoria Bazy Danych.

##### Grupa:
  - Cyra Agata
  - ~~Nowakowski Karol~~ (urlop dziekański)
  - Siwek Patryk

##### Projekt:
  - Temat: Baza danych dla uczelni.
  - Serwer: lokalny serwer SQL
  - Technologie: .Net i Entity Framework
  - Link: https://github.com/kklgf/universityBD

## Current database schema
![Database schema](universityBD.jpg)

## Features
- Database schema implementation and creation [Patryk]
- Inteligent adding of new entities from terminal [Patryk]
- Inteligent searching of entities from terminal [Patryk]
- Viewing the grades of a particular student: function Student.StudentGrades(); [Agata]
- Viewing the courses of a particular employee: function Employee.EmployeesCourses(); [Agata]
- Viewing the ECTS points of a particular student: function Student.StudentsECTS(); [Agata]
- Viewing the "attendance list" (list of students during at the particular section): function Section.AttendanceList(); [Agata]
- Viewing free places on a particular section: function Section.FreePlaces(); [Agata]
- blocking the course overload (more students than capacity): while adding a new enrollment [Agata]
- blocking student from having two classes at the same time: using the HasClassesAtTheTime(studentID, section) while enrolling for classes [Agata]
- Random data generation for whole database [Patryk]

## Dependencies
- Faker.Net:
    dotnet add package Faker.Net --version 1.3.77
- NBuilder:
    dotnet add package NBuilder --version 6.1.0
