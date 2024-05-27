using BLL.Services.Implement;
using BLL.Services.Interface;
using BLL.ViewModels.GradePoint;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradePointsController : Controller
    {
        IGradePointServices _gradePointService;
        IClassServices _classService;
        public GradePointsController(IGradePointServices gradePointService, IClassServices classServices)
        {
            _gradePointService = gradePointService;
            _classService = classServices;
        }
        [HttpGet("/gradepoint/get_all")]
        public async Task<IActionResult> Get_All_Item(int id, int semester, int start = 0, int length = 10, string search = "")
        {
            return Ok(await _gradePointService.Get_All_Async(id, start, length, search, semester));
        }
        [HttpGet("/gradepoint/get_all_year")]
        public async Task<IActionResult> GetSumPointWholeyear_Async(int id, int classes, string search = "")
        {
            return Ok(await _gradePointService.GetSumPointWholeyear_Async(id, classes, search));
        }

        [HttpGet("/gradepoint/get_by_id")]
        public IActionResult Get_By_Id(int ClassId, int UserId, int SubjectId, int Semester)
        {
            return Ok(_gradePointService.Get_By_Id(ClassId, UserId, SubjectId, Semester));
        }
        [HttpGet("/gradepoint/GradePointByClass")]
        public async Task<IActionResult> GradePointByClass(int idClass, int? semester = null)
        {
            return Ok(await _classService.GradePointByClass(idClass, semester));
        }
        [HttpGet("/gradepoint/GradePointByClassAllYear")]
        public async Task<IActionResult> GradePointByClassAllYear(int idClass, int? semester = null)
        {
            return Ok(await _classService.GradePointByClassAllYear(idClass, semester));
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
        //[HttpPost("/gradepoint/ImportFile")]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> Import(IFormFile file)
        //{
        //	if (file == null || file.Length <= 0)
        //	{
        //		return BadRequest("File is required.");
        //	}

        //	try
        //	{
        //		using (var memoryStream = new MemoryStream())
        //		{
        //			await file.CopyToAsync(memoryStream);
        //			memoryStream.Position = 0;

        //			(int addedUsersCount, byte[] invalidExcelBytes) = await _gradePointService.ImportFromExcel(memoryStream);

        //			if (invalidExcelBytes != null)
        //			{
        //				// Trả về tệp Excel chứa các dòng không hợp lệ
        //				return File(invalidExcelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "InvalidRows.xlsx");
        //			}
        //			else
        //			{
        //				// Trả về kết quả thành công
        //				return Ok($"Successfully added {addedUsersCount} users from Excel.");
        //			}
        //		}
        //	}
        //	catch (Exception ex)
        //	{
        //		return StatusCode(500, $"Error importing users: {ex.Message}");
        //	}
        //}


        //[HttpGet("/gradepoint/GetClassByUser")]
        //public async Task<IActionResult> GetClassByUser(int id)
        //{
        //	return Ok(await _gradePointService.GetClassByUser(id));
        //}
        [HttpGet("/gradepoint/subject")]
        public async Task<IActionResult> DetailGradePointByClass(int idClass, int idUser, int? semester = null)
        {
            //int idUser = 13;
            return Ok(await _classService.GradePointSubjectByClass(idClass, idUser, semester));
        }
    }

}
