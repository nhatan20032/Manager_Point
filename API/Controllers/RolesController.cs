using BLL.Services.RoleServices.Interface;
using BLL.ViewModels.Role;
using Manager_Point.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleServices? _roleServices;
        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpPost("/role/Create")]
        public async Task<IActionResult> CreateRole([FromForm] VM_Create_Roles role)
        {
            var result = await _roleServices!.AddItem(role);
            return Ok(result);
        }

        [HttpPost("/role/Create_Multiple")]
        public async Task<IActionResult> CreateMultipleRoles(List<Role> roles)
        {
            var result = await _roleServices!.CreateMultipleAsync(roles);
            return Ok(result);
        }

        [HttpPut("/role/Modified")]
        public async Task<IActionResult> ModifiedRole(int id, Role role)
        {
            var result = await _roleServices!.ModifiedItem(id, role);
            return Ok(result);
        }

        [HttpDelete("/role/Delete")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _roleServices!.RemoveItem(id);
            return Ok(result);
        }

        [HttpDelete("/role/Delete_Multiple")]
        public async Task<IActionResult> DeleteRole(List<int> ids)
        {
            var result = await _roleServices!.RemoveMultipleAsync(ids);
            return Ok(result);
        }

        [HttpGet("/role/Get_All")]
        public async Task<IActionResult> GetAllRole(int page_number = 1, int page_size = 10, string search = "")
        {
            var result = await _roleServices!.GetAllAsync(page_number, page_size, search);
            return Ok(result);
        }

        [HttpGet("/role/Get_By_Id")]
        public async Task<IActionResult> GetByIdRoles(int id)
        {
            var result = await _roleServices!.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
