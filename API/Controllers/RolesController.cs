using BLL.Services.Interface;
using BLL.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleServices _roleServices;
        public RolesController(IRoleServices role)
        {
            _roleServices = role;
        }

        [HttpGet("/role/get_all")]
        public async Task<IActionResult> Get_All_Item(int start = 0, int length = 10, string search = "")
        {
            return Ok(await _roleServices.Get_All_Async(start, length, search));
        }

        [HttpGet("/role/get_by_id")]
        public async Task<IActionResult> Get_By_Id(int id)
        {
            return Ok(await _roleServices.Get_By_Id(id));
        }

        [HttpPost("/role/create")]
        public async Task<IActionResult> Create_Item([FromBody] vm_create_role request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _roleServices.Create_Item(request));
        }

        [HttpPost("/role/batch_create")]
        public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_create_role> requests)
        {
            if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _roleServices.Batch_Create_Item(requests));
        }

        [HttpPut("/role/modified")]
        public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_update_role request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _roleServices.Modified_Item(id, request));
        }

        [HttpDelete("/role/remove")]
        public async Task<IActionResult> Remove_Item(int id)
        {
            return Ok(await _roleServices.Remove_Item(id));
        }

        [HttpDelete("/role/batch_remove")]
        public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
        {
            return Ok(await _roleServices.Batch_Remove_Item(ids));
        }
    }
}
