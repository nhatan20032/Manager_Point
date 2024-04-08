using BLL.Services.Implement;
using BLL.Services.Interface;
using BLL.ViewModels.Examination;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : Controller
    {
        IExaminationServices _examinationServices;
        public ExaminationController(IExaminationServices examinationServices)
        {
            _examinationServices = examinationServices;
        }
        [HttpGet("/examination/get_all")]
        public async Task<IActionResult> Get_All_Item(int page_number = 1, int page_size = 10, string search = "")
        {
            return Ok(await _examinationServices.Get_All_Async(page_number, page_size, search));
        }

        [HttpGet("/examination/get_by_id")]
        public async Task<IActionResult> Get_By_Id(int id)
        {
            return Ok(await _examinationServices.Get_By_Id(id));
        }

        [HttpPost("/examination/create")]
        public async Task<IActionResult> Create_Item([FromBody] vm_create_examination request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _examinationServices.Create_Item(request));
        }

        [HttpPost("/examination/batch_create")]
        public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_create_examination> requests)
        {
            if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _examinationServices.Batch_Create_Item(requests));
        }

        [HttpPut("/examination/modified")]
        public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_update_examination request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _examinationServices.Modified_Item(id, request));
        }

        [HttpDelete("/examination/remove")]
        public async Task<IActionResult> Remove_Item(int id)
        {
            return Ok(await _examinationServices.Remove_Item(id));
        }

        [HttpDelete("/examination/batch_remove")]
        public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
        {
            return Ok(await _examinationServices.Batch_Remove_Item(ids));
        }
    }
}
