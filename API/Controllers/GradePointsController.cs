using BLL.Services.Interface;
using BLL.ViewModels.GradePoint;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GradePointsController : Controller
	{
        IGradePointService _gradePointService;
        public GradePointsController(IGradePointService gradePointService)
        {
            _gradePointService = gradePointService;
        }
		[HttpGet("/gradepoint/get_all")]
		public async Task<IActionResult> Get_All_Item(int page_number = 1, int page_size = 10, string search = "")
		{
			return Ok(await _gradePointService.Get_All_Async(page_number, page_size, search));
		}

		[HttpGet("/gradepoint/get_by_id")]
		public async Task<IActionResult> Get_By_Id(int id)
		{
			return Ok(await _gradePointService.Get_By_Id(id));
		}

		[HttpPost("/gradepoint/create")]
		public async Task<IActionResult> Create_Item([FromBody] vm_create_gradepoint request)
		{
			if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _gradePointService.Create_Item(request));
		}

		[HttpPost("/gradepoint/batch_create")]
		public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_create_gradepoint> requests)
		{
			if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _gradePointService.Batch_Create_Item(requests));
		}

		[HttpPut("/gradepoint/modified")]
		public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_update_gradepoint request)
		{
			if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _gradePointService.Modified_Item(id, request));
		}

		[HttpDelete("/gradepoint/remove")]
		public async Task<IActionResult> Remove_Item(int id)
		{
			return Ok(await _gradePointService.Remove_Item(id));
		}

		[HttpDelete("/gradepoint/batch_remove")]
		public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
		{
			return Ok(await _gradePointService.Batch_Remove_Item(ids));
		}
	}
}
