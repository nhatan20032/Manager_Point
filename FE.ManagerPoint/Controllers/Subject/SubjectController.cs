using Microsoft.AspNetCore.Mvc;

namespace FE.ManagerPoint.Controllers.Subject
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Subject_User()
        {
            return View();
        }
    }
}
