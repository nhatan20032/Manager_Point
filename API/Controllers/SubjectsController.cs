using BLL.Services.SubjectServices.Interface;
using Manager_Point.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectServices? _subjectServices;
        public SubjectsController(ISubjectServices subjectServices)
        {
            _subjectServices = subjectServices;
        }
        [HttpPost("/subject/Create")]
        public async Task<IActionResult> CreateSubject(Subject Subject)
        {
            var result = await _subjectServices!.AddItem(Subject);
            return Ok(result);
        }

        [HttpPost("/subject/Create_Multiple")]
        public async Task<IActionResult> CreateMultipleSubjects(List<Subject> Subjects)
        {
            var result = await _subjectServices!.CreateMultipleAsync(Subjects);
            return Ok(result);
        }

        [HttpPut("/subject/Modified")]
        public async Task<IActionResult> ModifiedSubject(int id, Subject Subject)
        {
            var result = await _subjectServices!.ModifiedItem(id, Subject);
            return Ok(result);
        }

        [HttpDelete("/subject/Delete")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var result = await _subjectServices!.RemoveItem(id);
            return Ok(result);
        }

        [HttpDelete("/subject/Delete_Multiple")]
        public async Task<IActionResult> DeleteSubject(List<int> ids)
        {
            var result = await _subjectServices!.RemoveMultipleAsync(ids);
            return Ok(result);
        }

        [HttpGet("/subject/Get_All")]
        public async Task<IActionResult> GetAllSubject(int page_number, int page_size, string search)
        {
            var result = await _subjectServices!.GetAllAsync(page_number, page_size, search);
            return Ok(result);
        }

        [HttpGet("/subject/Get_By_Id")]
        public async Task<IActionResult> GetByIdSubjects(int id)
        {
            var result = await _subjectServices!.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
