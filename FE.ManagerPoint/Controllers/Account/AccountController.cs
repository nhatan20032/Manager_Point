using Microsoft.AspNetCore.Mvc;

namespace FE.ManagerPoint.Controllers.Account
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    } 
}
