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

        public async Task<IActionResult> ClassOnBoard(int idUser)
        {
            var classInfo = await _services.GetClassOnBoard(idUser);
            ViewBag.ClassInfoJson = classInfo;
            return View();
        }
        public async Task<IActionResult> ClassHomeRoom(int idUser)
        {
            var classInfor = await _services.GetHomeRoomOnBoard(idUser);
            ViewBag.ClassInfoJson = classInfor;
            return View();
        }
        public async Task<IActionResult> ClassDetail(int idClass)
        {
            var classInfor = await _services.GradePointByClass(idClass);
            ViewBag.ClassInfoJson = classInfor;
            return View();
        }

    }
}
