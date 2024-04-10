using Microsoft.AspNetCore.Mvc;

namespace FE.ManagerPoint.Controllers.Gradepoint
{
    public class GradepointController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
