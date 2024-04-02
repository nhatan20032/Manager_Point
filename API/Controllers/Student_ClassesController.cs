using BLL.Services.Interface;
using BLL.ViewModels.Student_Class;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Student_ClassesController : ControllerBase
    {
        private readonly IStudent_ClassServices _student_ClassServices;
        public Student_ClassesController(IStudent_ClassServices student_ClassServices)
        {
            _student_ClassServices = student_ClassServices;
        }
        [HttpPost("/student_class/create")]
        public async Task<IActionResult> Create_Item([FromBody] vm_student_class request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _student_ClassServices.Create_Item(request));
        }

        [HttpPost("/student_class/batch_create")]
        public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_student_class> requests)
        {
            if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _student_ClassServices.Batch_Create_Item(requests));
        }

        [HttpPut("/student_class/modified")]
        public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_student_class request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _student_ClassServices.Modified_Item(id, request));
        }

        [HttpDelete("/student_class/remove")]
        public async Task<IActionResult> Remove_Item(int id)
        {
            return Ok(await _student_ClassServices.Remove_Item(id));
        }

        [HttpDelete("/student_class/batch_remove")]
        public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
        {
            return Ok(await _student_ClassServices.Batch_Remove_Item(ids));
        }
    }
}
