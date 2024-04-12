using Microsoft.AspNetCore.Mvc;

namespace FE.ManagerPoint.Controllers.Classes
{
	public class ClassController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
        public IActionResult Student_Class()
        {
            return View();
        }
    }
}
