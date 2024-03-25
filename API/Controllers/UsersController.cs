using BLL.Services.Interface;
using BLL.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet("/user/get_all")]
        public async Task<IActionResult> Get_All_Item(int page_number = 1, int page_size = 10, string search = "")
        {
            return Ok(await _userServices.Get_All_Async(page_number, page_size, search));
        }

        [HttpGet("/user/get_by_id")]
        public async Task<IActionResult> Get_By_Id(int id)
        {
            return Ok(await _userServices.Get_By_Id(id));
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
