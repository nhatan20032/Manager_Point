using BLL.Services.Interface;
using BLL.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CousresController : Controller
	{
		ICourseService _courseService;
        public CousresController(ICourseService courseService)
        {
            _courseService = courseService;
        }
		[HttpGet("/course/get_all")]
		public async Task<IActionResult> Get_All_Item(int page_number = 1, int page_size = 10, string search = "")
		{
			return Ok(await _courseService.Get_All_Async(page_number, page_size, search));
		}

		[HttpGet("/course/get_by_id")]
		public async Task<IActionResult> Get_By_Id(int id)
		{
			return Ok(await _courseService.Get_By_Id(id));
		}

		[HttpPost("/course/create")]
		public async Task<IActionResult> Create_Item([FromBody] vm_create_course request)
		{
			if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _courseService.Create_Item(request));
		}

		[HttpPost("/course/batch_create")]
		public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_create_course> requests)
		{
			if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _courseService.Batch_Create_Item(requests));
		}

		[HttpPut("/course/modified")]
		public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_update_course request)
		{
			if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _courseService.Modified_Item(id, request));
		}

		[HttpDelete("/course/remove")]
		public async Task<IActionResult> Remove_Item(int id)
		{
			return Ok(await _courseService.Remove_Item(id));
		}

		[HttpDelete("/course/batch_remove")]
		public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
		{
			return Ok(await _courseService.Batch_Remove_Item(ids));
		}
	}
}
