using BLL.Author;
using BLL.Services.Implement;
using BLL.Services.Interface;
using BLL.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet("/user/get_all")]
        public async Task<IActionResult> Get_All_Item(int start = 0, int length = 10, string search = "")
        {
            return Ok(await _userServices.Get_All_Async(start, length, search));
        }
        [HttpGet("/user/get_all_user_no_role")]
        public async Task<IActionResult> Get_All_Teacher(int start = 0, int length = 10, string search = "")
        {
            return Ok(await _userServices.Get_User_No_Role(start, length, search));
        }
        [HttpGet("/user/get_all_teacher")]
        public async Task<IActionResult> Get_All_Teacher(int start = 0, int length = 10, string search = "", int subject = 0, int classes = 0, int check_subject = 0)
        {
            return Ok(await _userServices.Get_All_Teacher(start, length, search, subject, classes, check_subject));
        }
        [HttpGet("/user/Get_All_Teacher_No_HomeRoom")]
        public async Task<IActionResult> Get_All_Teacher_No_HomeRoom(int start = 0, int length = 10, string search = "", int subject = 0, int classes = 0)
        {
            return Ok(await _userServices.Get_All_Teacher_No_HomeRoom(start, length, search, subject, classes));
        }
        [HttpGet("/user/count_all_teacher_student")]
        public async Task<IActionResult> Count_All_Teacher_Student()
        {
            return Ok(await _userServices.Count_All_Teacher_Student());
        }
        [HttpGet("/user/Count_Teachers_By_Subject")]
        public async Task<IActionResult> Count_Teachers_By_Subject()
        {
            return Ok(await _userServices.Count_Teachers_By_Subject());
        }
        [HttpGet("/user/Count_Students_By_Course")]
        public async Task<IActionResult> Count_Students_By_Course()
        {
            return Ok(await _userServices.Count_Students_By_Course());
        }
        [HttpGet("/user/get_all_student")]
        public async Task<IActionResult> Get_All_Student(int start = 0, int length = 10, string search = "",  int classes = 0, int check_class = 0)
        {
            return Ok(await _userServices.Get_All_Student(start, length, search, classes, check_class));
        }

        [HttpGet("/user/get_by_id")]
        public async Task<IActionResult> Get_By_Id(int id)
        {
            return Ok(await _userServices.Get_By_Id(id));
        }
        [HttpGet("/user/Get_By_HomeRoom_Id")]
        public async Task<IActionResult> Get_By_HomeRoom_Id(int idClass)
        {
            var result = await _userServices.Get_By_HomeRoom_Id(idClass);

            if (result != null)
            {
                return Ok(result); // Trả về Ok (200) nếu có dữ liệu
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost("/user/authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userServices.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("/user/import_excel")]
        public async Task<IActionResult> ImportUsersFromExcel(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("File is required.");
            }

            try
            {
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                int addedUsersCount = await _userServices.AddUsersFromExcel(memoryStream);

                return Ok($"Successfully added {addedUsersCount} users from Excel.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error importing users: {ex.Message}");
            }
        }

        [HttpPost("/user/create")]
        public async Task<IActionResult> Create_Item([FromBody] vm_create_user request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _userServices.Create_Item(request));
        }

        [HttpPost("/user/batch_create")]
        public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_create_user> requests)
        {
            if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _userServices.Batch_Create_Item(requests));
        }

        [HttpPut("/user/modified")]
        public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_update_user request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _userServices.Modified_Item(id, request));
        }

        [HttpDelete("/user/remove")]
        public async Task<IActionResult> Remove_Item(int id)
        {
            return Ok(await _userServices.Remove_Item(id));
        }

        [HttpDelete("/user/batch_remove")]
        public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
        {
            return Ok(await _userServices.Batch_Remove_Item(ids));
        }
    }
}
