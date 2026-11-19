using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using SchoolManagementMvc.Models;

namespace SchoolManagementMvc.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login() { if (Session["Username"] != null) return RedirectToAction("Index", "Students"); return View(new LoginViewModel()); }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            string hash = Hash(model.Password);
            using (var db = new SchoolDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Username == model.Username && x.PasswordHash == hash);
                if (user == null) { ModelState.AddModelError("", "Invalid username or password."); return View(model); }
                Session["Username"] = user.Username; Session["Role"] = user.Role;
                return RedirectToAction("Index", "Students");
            }
        }

        public ActionResult Logout() { Session.Clear(); return RedirectToAction("Login"); }

        private static string Hash(string input)
        {
            using (var sha = SHA256.Create())
                return string.Concat(sha.ComputeHash(Encoding.UTF8.GetBytes(input)).Select(b => b.ToString("X2")));
        }
    }
}
