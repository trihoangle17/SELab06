IF DB_ID(N'StudentsDB') IS NULL CREATE DATABASE StudentsDB;
GO
USE StudentsDB;
GO
IF OBJECT_ID('dbo.tblStudents','U') IS NULL
CREATE TABLE tblStudents(StudentID INT IDENTITY(1,1) PRIMARY KEY,FirstName NVARCHAR(50) NOT NULL,LastName NVARCHAR(50) NOT NULL,Email NVARCHAR(100) NOT NULL,EnrollmentDate DATE NOT NULL);
GO
IF NOT EXISTS(SELECT 1 FROM tblStudents)
INSERT tblStudents(FirstName,LastName,Email,EnrollmentDate) VALUES
('Trung','Pham','trung.pham@seuni.com','2024-01-15'),('Michael','Pham','michael.pham@seuni.com','2024-02-20');
GO
