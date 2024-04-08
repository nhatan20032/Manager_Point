using BLL.Services.Implement;
using BLL.Services.Interface;
using BLL.ViewModels.Subject_Teacher;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Subject_TeachersController : ControllerBase
    {
        private readonly ISubject_TeacherServices _subject_TeacherServices;
        public Subject_TeachersController(ISubject_TeacherServices subject_TeacherServices)
        {
            _subject_TeacherServices = subject_TeacherServices;
        }

        [HttpPost("/subject_tecaher/create")]
        public async Task<IActionResult> Create_Item([FromBody] vm_subject_teacher request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _subject_TeacherServices.Create_Item(request));
        }

        [HttpPost("/subject_tecaher/batch_create")]
        public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_subject_teacher> requests)
        {
            if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _subject_TeacherServices.Batch_Create_Item(requests));
        }

        [HttpPut("/subject_tecaher/modified")]
        public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_subject_teacher request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _subject_TeacherServices.Modified_Item(id, request));
        }

        [HttpDelete("/subject_tecaher/remove")]
        public async Task<IActionResult> Remove_Item(int id)
        {
            return Ok(await _subject_TeacherServices.Remove_Item(id));
        }

        [HttpDelete("/subject_tecaher/batch_remove")]
        public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
        {
            return Ok(await _subject_TeacherServices.Batch_Remove_Item(ids));
        }
    }
}
