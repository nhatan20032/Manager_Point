using Microsoft.AspNetCore.Mvc;

namespace FE.ManagerPoint.Controllers.User
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult User_Role()
        {
            return View();
        }
        public IActionResult Analystic_User()
        {
            return View();
        }
        [HttpPost("/user/upload")]
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
