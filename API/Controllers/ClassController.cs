using BLL.Services.Implement;
using BLL.Services.Interface;
using BLL.ViewModels.Class;
using BLL.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class ClassController : Controller
	{
		IClassService _classService;
        public ClassController(IClassService classService)
        {
			_classService = classService;
        }
		[HttpGet("/class/get_all")]
		public async Task<IActionResult> Get_All_Item(int page_number = 1, int page_size = 10, string search = "")
		{
			return Ok(await _classService.Get_All_Async(page_number, page_size, search));
		}

		[HttpGet("/class/get_by_id")]
		public async Task<IActionResult> Get_By_Id(int id)
		{
			return Ok(await _classService.Get_By_Id(id));
		}

		[HttpPost("/class/create")]
		public async Task<IActionResult> Create_Item([FromBody] vm_create_class request)
		{
			if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _classService.Create_Item(request));
		}

		[HttpPost("/class/batch_create")]
		public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_create_class> requests)
		{
			if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _classService.Batch_Create_Item(requests));
		}

		[HttpPut("/class/modified")]
		public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_update_class request)
		{
			if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _classService.Modified_Item(id, request));
		}

		[HttpDelete("/class/remove")]
		public async Task<IActionResult> Remove_Item(int id)
		{
			return Ok(await _classService.Remove_Item(id));
		}

		[HttpDelete("/class/batch_remove")]
		public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
		{
			return Ok(await _classService.Batch_Remove_Item(ids));
		}
	}
}
