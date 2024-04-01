using Microsoft.AspNetCore.Mvc;

namespace FE.ManagerPoint.Controllers.User
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
