using System;using System.ComponentModel.DataAnnotations;using System.ComponentModel.DataAnnotations.Schema;using System.Data.Entity;
namespace ClassActivityStudentsApp.Models{
[Table("tblStudents")]public class Student{public int StudentID{get;set;}[Required,StringLength(50)]public string FirstName{get;set;}[Required,StringLength(50)]public string LastName{get;set;}[Required,EmailAddress,StringLength(100)]public string Email{get;set;}[DataType(DataType.Date)]public DateTime EnrollmentDate{get;set;}}
public class StudentsDbContext:DbContext{static StudentsDbContext(){Database.SetInitializer<StudentsDbContext>(null);}public StudentsDbContext():base("name=StudentsDbContext"){}public DbSet<Student> Students{get;set;}}
}
