using BLL.Services.Interface;
using BLL.ViewModels.AcademicPerformance;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class AcademicPerformanceController : Controller
	{
		IAcademicPerformanceServices _academicPerformanceServices;
		public AcademicPerformanceController(IAcademicPerformanceServices academicPerformanceServices) 
		{ 
			_academicPerformanceServices = academicPerformanceServices;
		}
		[HttpGet("/academicperformance/get_all")]
		public async Task<IActionResult> Get_All_Item(int page_number = 1, int page_size = 10, string search = "")
		{
			return Ok(await _academicPerformanceServices.Get_All_Async(page_number, page_size, search));
		}

		[HttpGet("/academicperformance/get_by_id")]
		public async Task<IActionResult> Get_By_Id(int id)
		{
			return Ok(await _academicPerformanceServices.Get_By_Id(id));
		}

		[HttpPost("/academicperformance/create")]
		public async Task<IActionResult> Create_Item([FromBody] vm_create_academicperformance request)
		{
			if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _academicPerformanceServices.Create_Item(request));
		}

		[HttpPost("/academicperformance/batch_create")]
		public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_create_academicperformance> requests)
		{
			if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _academicPerformanceServices.Batch_Create_Item(requests));
		}

		[HttpPut("/academicperformance/modified")]
		public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_update_academicperformance request)
		{
			if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
			return Ok(await _academicPerformanceServices.Modified_Item(id, request));
		}

		[HttpDelete("/academicperformance/remove")]
		public async Task<IActionResult> Remove_Item(int id)
		{
			return Ok(await _academicPerformanceServices.Remove_Item(id));
		}

		[HttpDelete("/academicperformance/batch_remove")]
		public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
		{
			return Ok(await _academicPerformanceServices.Batch_Remove_Item(ids));
		}
	}
}
