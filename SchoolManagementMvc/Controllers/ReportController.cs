using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SchoolManagementMvc.Models;

namespace SchoolManagementMvc.Controllers
{
    public class ReportController : SecureController
    {
        private readonly SchoolDbContext db = new SchoolDbContext();

        public ActionResult Index(int? courseId)
        {
            var model = BuildModel(courseId);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Generate(int selectedCourseID, string action)
        {
            var model = BuildModel(selectedCourseID);
            if (action == "export")
            {
                var lines = new List<string> { "Student Report", "Course: " + model.SelectedCourseTitle, "" };
                lines.AddRange(model.Students.Select(x => string.Format("{0}  {1} {2}  Grade: {3}", x.StudentID, x.FirstName, x.LastName, x.Grade.HasValue ? x.Grade.Value.ToString("0.00") : "N/A")));
                return File(SimplePdfBuilder.Create(lines), "application/pdf", "StudentReport.pdf");
            }
            return View("Index", model);
        }

        private CourseReportViewModel BuildModel(int? courseId)
        {
            var courses = db.Courses.OrderBy(x => x.Title).ToList();
            var selected = courseId ?? courses.Select(x => (int?)x.CourseID).FirstOrDefault() ?? 0;
            var model = new CourseReportViewModel
            {
                SelectedCourseID = selected,
                SelectedCourseTitle = courses.Where(x => x.CourseID == selected).Select(x => x.Title).FirstOrDefault() ?? "No course selected",
                Courses = courses.Select(x => new SelectListItem { Value = x.CourseID.ToString(), Text = x.Title, Selected = x.CourseID == selected }).ToList()
            };
            model.Students = db.Enrollments.Where(x => x.CourseID == selected).Select(x => new StudentReportRow
            {
                StudentID = x.StudentID,
                FirstName = x.Student.FirstName,
                LastName = x.Student.LastName,
                Grade = x.Grade
            }).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
            return model;
        }

        protected override void Dispose(bool disposing) { if (disposing) db.Dispose(); base.Dispose(disposing); }
    }
}
