using Microsoft.AspNetCore.Mvc;

namespace FE.ManagerPoint.Controllers.Course
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
