using BLL.Services.Implement;
using BLL.Services.Interface;
using BLL.ViewModels.Teacher_Class;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class Teacher_ClassesController : ControllerBase
    {
        private readonly ITeacher_ClassServices _teacher_ClassServices;
        public Teacher_ClassesController(ITeacher_ClassServices teacher_ClassServices)
        {
            _teacher_ClassServices = teacher_ClassServices;
        }
        [HttpPost("/teacher_class/create")]
        public async Task<IActionResult> Create_Item([FromBody] vm_teacher_class request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _teacher_ClassServices.Create_Item(request));
        }

        [HttpPost("/teacher_class/batch_create_subject")]
        public async Task<IActionResult> Batch_Create_Item_Subject([FromBody] List<vm_teacher_class_subject> requests)
        {
            if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _teacher_ClassServices.Batch_Create_Item_Subject(requests));
        }

        [HttpPost("/teacher_class/batch_create_homeroom")]
        public async Task<IActionResult> Batch_Create_Item_HomeRoom([FromBody] List<vm_teacher_class> requests)
        {
            if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _teacher_ClassServices.Batch_Create_Item_HomeRoom(requests));
        }

        [HttpPut("/teacher_class/modified")]
        public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_teacher_class request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _teacher_ClassServices.Modified_Item(id, request));
        }

        [HttpDelete("/teacher_class/remove")]
        public async Task<IActionResult> Remove_Item(int id)
        {
            return Ok(await _teacher_ClassServices.Remove_Item(id));
        }

        [HttpDelete("/teacher_class/batch_remove")]
        public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
        {
            return Ok(await _teacher_ClassServices.Batch_Remove_Item(ids));
        }
        [HttpDelete("/teacher_class/Batch_Remove_Item_HomeRoom/{idClass}")]
        public async Task<IActionResult> Batch_Remove_Item_HomeRoom([FromRoute] int idClass)
        {
            return Ok(await _teacher_ClassServices.Batch_Remove_Item_HomeRoom(idClass));
        }        
        [HttpDelete("/teacher_class/Batch_Remove_Item_Subject/{UserId}")]
        public async Task<IActionResult> Batch_Remove_Item_Subject([FromRoute] int UserId)
        {
            return Ok(await _teacher_ClassServices.Batch_Remove_Item_Subject(UserId));
        }
    }
}
