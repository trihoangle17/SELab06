/* SE Lab 6 - School Management Database
   Student: Lê Hoàng Trí - 524H0199 */
IF DB_ID(N'SchoolDB') IS NULL CREATE DATABASE SchoolDB;
GO
USE SchoolDB;
GO
IF OBJECT_ID('dbo.Enrollments','U') IS NOT NULL DROP TABLE dbo.Enrollments;
IF OBJECT_ID('dbo.Courses','U') IS NOT NULL DROP TABLE dbo.Courses;
IF OBJECT_ID('dbo.Instructors','U') IS NOT NULL DROP TABLE dbo.Instructors;
IF OBJECT_ID('dbo.Students','U') IS NOT NULL DROP TABLE dbo.Students;
IF OBJECT_ID('dbo.Departments','U') IS NOT NULL DROP TABLE dbo.Departments;
IF OBJECT_ID('dbo.tblUser','U') IS NOT NULL DROP TABLE dbo.tblUser;
GO
CREATE TABLE Departments(
 DepartmentID INT IDENTITY(1,1) PRIMARY KEY,
 Name NVARCHAR(100) NOT NULL,
 Budget DECIMAL(18,2) NOT NULL CHECK(Budget>=0),
 StartDate DATE NOT NULL
);
CREATE TABLE Students(
 StudentID INT IDENTITY(1,1) PRIMARY KEY,
 FirstName NVARCHAR(50) NOT NULL,
 LastName NVARCHAR(50) NOT NULL,
 EnrollmentDate DATE NOT NULL
);
CREATE TABLE Instructors(
 InstructorID INT IDENTITY(1,1) PRIMARY KEY,
 FirstName NVARCHAR(50) NOT NULL,
 LastName NVARCHAR(50) NOT NULL,
 HireDate DATE NOT NULL
);
CREATE TABLE Courses(
 CourseID INT IDENTITY(1,1) PRIMARY KEY,
 Title NVARCHAR(100) NOT NULL,
 Credits INT NOT NULL CHECK(Credits BETWEEN 1 AND 10),
 DepartmentID INT NOT NULL REFERENCES Departments(DepartmentID)
);
CREATE TABLE Enrollments(
 EnrollmentID INT IDENTITY(1,1) PRIMARY KEY,
 CourseID INT NOT NULL REFERENCES Courses(CourseID),
 StudentID INT NOT NULL REFERENCES Students(StudentID),
 Grade DECIMAL(3,2) NULL CHECK(Grade BETWEEN 0 AND 4),
 CONSTRAINT UQ_Enrollment UNIQUE(CourseID,StudentID)
);
CREATE TABLE tblUser(
 UserID INT IDENTITY(1,1) PRIMARY KEY,
 Username NVARCHAR(50) NOT NULL UNIQUE,
 PasswordHash CHAR(64) NOT NULL,
 Email NVARCHAR(100) NOT NULL,
 Role NVARCHAR(20) NOT NULL DEFAULT 'Staff'
);
GO
INSERT Departments(Name,Budget,StartDate) VALUES
('Computer Science',120000,'2023-01-01'),('Business Administration',95000,'2023-02-01');
INSERT Students(FirstName,LastName,EnrollmentDate) VALUES
('Jane','Smith','2023-09-01'),('Michael','Pham','2024-02-20'),('Trung','Le','2024-01-15');
INSERT Instructors(FirstName,LastName,HireDate) VALUES
('John','Doe','2020-08-15'),('Anna','Nguyen','2021-06-10');
INSERT Courses(Title,Credits,DepartmentID) VALUES
('Introduction to Programming',3,1),('Database Systems',3,1),('Principles of Management',2,2);
INSERT Enrollments(CourseID,StudentID,Grade) VALUES (1,1,3.50),(1,2,3.75),(2,3,3.20),(3,1,3.80);
INSERT tblUser(Username,PasswordHash,Email,Role)
VALUES('admin',CONVERT(VARCHAR(64),HASHBYTES('SHA2_256','Lab6@123'),2),'524H0199@student.tdtu.edu.vn','Admin');
GO
SELECT * FROM Students; SELECT * FROM Courses; SELECT * FROM Enrollments;
