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
            var classInfo = _services.Get_By_Id_vm_class(id);
            return View(classInfo);
        }
        public async Task<IActionResult> ClassOnBoard(int idUser)
        {
            var classInfo = await _services.GetClassOnBoard(idUser);
            ViewBag.ClassInfoJson = classInfo;
            return View();
        }
		public IActionResult GetInClassOnBoard(int idClass)
		{
			ViewBag.idClassbySubject = idClass;
			return View();
		}
		public async Task<IActionResult> ClassHomeRoom(int idUser)
        {
            var classInfor = await _services.GetHomeRoomOnBoard(idUser);
            ViewBag.ClassInfoJson = classInfor;
            return View();
        }
        public async Task<IActionResult> GetInClassHomeRoom(int idClass)
        {
            var classInfor = await _services.Get_By_Id(idClass);
            ViewBag.ClassInfor = classInfor;
            ViewBag.idCLass = idClass;
            return View();
        }
        [HttpPost("/class/upload")]
        public string Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                var filePath = Path.Combine(uploadDirectory, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                var relativePath = "/uploads/" + fileName;
                return relativePath;
            }
            return null!;
        }
    }
}
