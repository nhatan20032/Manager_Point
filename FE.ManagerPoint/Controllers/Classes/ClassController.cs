using BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FE.ManagerPoint.Controllers.Classes
{
    public class ClassController : Controller
    {
        private readonly IClassServices _services;
        public ClassController(IClassServices services)
        {
            _services = services;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Student_Class()
        {
            return View();
        }
        public IActionResult Teacher_Class()
        {
            return View();
        }
        public IActionResult Add_Teacher_To_Class(int id)
        {
            var classInfo = _services.Get_By_Id(id);
            return View(classInfo);
        }
    }
}
