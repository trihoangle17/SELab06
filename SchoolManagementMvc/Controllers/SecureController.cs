using System.Web.Mvc;
namespace SchoolManagementMvc.Controllers
{
    public abstract class SecureController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Username"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "Account");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
