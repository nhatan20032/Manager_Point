using BLL.Services.Interface;
using BLL.ViewModels.Role_User;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Role_UsersController : ControllerBase
    {
        private readonly IRole_UserServices _role_UserServices;
        public Role_UsersController(IRole_UserServices role_UserServices)
        {
            _role_UserServices = role_UserServices;
        }
        [HttpPost("/role_user/create")]
        public async Task<IActionResult> Create_Item([FromBody] vm_role_user request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _role_UserServices.Create_Item(request));
        }

        [HttpPost("/role_user/batch_create")]
        public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_role_user> requests)
        {
            if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _role_UserServices.Batch_Create_Item(requests));
        }

        [HttpPut("/role_user/modified")]
        public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_role_user request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _role_UserServices.Modified_Item(id, request));
        }

        [HttpDelete("/role_user/remove")]
        public async Task<IActionResult> Remove_Item(int id)
        {
            return Ok(await _role_UserServices.Remove_Item(id));
        }

        [HttpDelete("/role_user/batch_remove")]
        public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
        {
            return Ok(await _role_UserServices.Batch_Remove_Item(ids));
        }
    }
}
