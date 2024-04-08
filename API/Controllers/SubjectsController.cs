using BLL.Services.Implement;
using BLL.Services.Interface;
using BLL.ViewModels.Subject;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectServices _subjectServices;
        public SubjectsController(ISubjectServices services)
        {
            _subjectServices = services;
        }

        [HttpGet("/subject/get_all")]
        public async Task<IActionResult> Get_All_Item(int start = 0, int length = 10, string search = "")
        {
            return Ok(await _subjectServices.Get_All_Async(start, length, search));
        }
        [HttpGet("/subject/get_list")]
        public IActionResult Get_List()
        {
            return Ok(_subjectServices.Get_List());
        }

        [HttpGet("/subject/get_by_id")]
        public async Task<IActionResult> Get_By_Id(int id)
        {
            return Ok(await _subjectServices.Get_By_Id(id));
        }

        [HttpPost("/subject/create")]
        public async Task<IActionResult> Create_Item([FromBody] vm_create_subject request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _subjectServices.Create_Item(request));
        }

        [HttpPost("/subject/batch_create")]
        public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_create_subject> requests)
        {
            if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _subjectServices.Batch_Create_Item(requests));
        }

        [HttpPut("/subject/modified")]
        public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_update_subject request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _subjectServices.Modified_Item(id, request));
        }

        [HttpDelete("/subject/remove")]
        public async Task<IActionResult> Remove_Item(int id)
        {
            return Ok(await _subjectServices.Remove_Item(id));
        }

        [HttpDelete("/subject/batch_remove")]
        public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
        {
            return Ok(await _subjectServices.Batch_Remove_Item(ids));
        }
    }
}
