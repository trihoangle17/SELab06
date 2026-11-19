using System.Data.Entity;
using System.Linq;
using SchoolManagementMvc.Models;
namespace SchoolManagementMvc.Controllers
{
    public class StudentsController : CrudController<Student> { protected override DbSet<Student> Set => Db.Students; protected override int IdOf(Student x) => x.StudentID; }
    public class CoursesController : CrudController<Course> { protected override DbSet<Course> Set => Db.Courses; protected override int IdOf(Course x) => x.CourseID; protected override IQueryable<Course> Query() => Db.Courses.Include(x => x.Department); }
    public class InstructorsController : CrudController<Instructor> { protected override DbSet<Instructor> Set => Db.Instructors; protected override int IdOf(Instructor x) => x.InstructorID; }
    public class DepartmentsController : CrudController<Department> { protected override DbSet<Department> Set => Db.Departments; protected override int IdOf(Department x) => x.DepartmentID; }
    public class EnrollmentsController : CrudController<Enrollment> { protected override DbSet<Enrollment> Set => Db.Enrollments; protected override int IdOf(Enrollment x) => x.EnrollmentID; protected override IQueryable<Enrollment> Query() => Db.Enrollments.Include(x => x.Student).Include(x => x.Course); }
}
