using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;

namespace SchoolManagementMvc.Models
{
    [Table("Students")]
    public class Student
    {
        public int StudentID { get; set; }
        [Required, StringLength(50), Display(Name = "First name")] public string FirstName { get; set; }
        [Required, StringLength(50), Display(Name = "Last name")] public string LastName { get; set; }
        [DataType(DataType.Date), Display(Name = "Enrollment date")] public DateTime EnrollmentDate { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
    }

    [Table("Departments")]
    public class Department
    {
        public int DepartmentID { get; set; }
        [Required, StringLength(100)] public string Name { get; set; }
        [Range(0, 999999999), DataType(DataType.Currency)] public decimal Budget { get; set; }
        [DataType(DataType.Date), Display(Name = "Start date")] public DateTime StartDate { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }

    [Table("Courses")]
    public class Course
    {
        public int CourseID { get; set; }
        [Required, StringLength(100)] public string Title { get; set; }
        [Range(1, 10)] public int Credits { get; set; }
        [Display(Name = "Department ID")] public int DepartmentID { get; set; }
        [ForeignKey("DepartmentID")] public virtual Department Department { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
    }

    [Table("Instructors")]
    public class Instructor
    {
        public int InstructorID { get; set; }
        [Required, StringLength(50), Display(Name = "First name")] public string FirstName { get; set; }
        [Required, StringLength(50), Display(Name = "Last name")] public string LastName { get; set; }
        [DataType(DataType.Date), Display(Name = "Hire date")] public DateTime HireDate { get; set; }
    }

    [Table("Enrollments")]
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        [Display(Name = "Course ID")] public int CourseID { get; set; }
        [Display(Name = "Student ID")] public int StudentID { get; set; }
        [Range(0, 4)] public decimal? Grade { get; set; }
        [ForeignKey("CourseID")] public virtual Course Course { get; set; }
        [ForeignKey("StudentID")] public virtual Student Student { get; set; }
    }

    [Table("tblUser")]
    public class AppUser
    {
        public int UserID { get; set; }
        [Required, StringLength(50)] public string Username { get; set; }
        [Required, StringLength(64)] public string PasswordHash { get; set; }
        [Required, EmailAddress, StringLength(100)] public string Email { get; set; }
        [Required, StringLength(20)] public string Role { get; set; }
    }

    public class SchoolDbContext : DbContext
    {
        static SchoolDbContext() { Database.SetInitializer<SchoolDbContext>(null); }
        public SchoolDbContext() : base("name=SchoolDbContext") { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<AppUser> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasMany(x => x.Enrollments).WithRequired(x => x.Student).HasForeignKey(x => x.StudentID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Course>().HasMany(x => x.Enrollments).WithRequired(x => x.Course).HasForeignKey(x => x.CourseID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Department>().HasMany(x => x.Courses).WithRequired(x => x.Department).HasForeignKey(x => x.DepartmentID).WillCascadeOnDelete(false);
            modelBuilder.Entity<Department>().Property(x => x.Budget).HasPrecision(18, 2);
            modelBuilder.Entity<Enrollment>().Property(x => x.Grade).HasPrecision(3, 2);
            base.OnModelCreating(modelBuilder);
        }
    }

    public class LoginViewModel
    {
        [Required] public string Username { get; set; }
        [Required, DataType(DataType.Password)] public string Password { get; set; }
    }

    public class StudentReportRow
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? Grade { get; set; }
    }

    public class CourseReportViewModel
    {
        [Display(Name = "Course")] public int SelectedCourseID { get; set; }
        public IEnumerable<SelectListItem> Courses { get; set; }
        public string SelectedCourseTitle { get; set; }
        public IList<StudentReportRow> Students { get; set; } = new List<StudentReportRow>();
    }
}
