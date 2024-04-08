using BLL.Services.Implement;
using BLL.Services.Interface;
using BLL.ViewModels.Message;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        IMessageServices _messageService;
        public MessageController(IMessageServices messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("/message/get_all")]
        public async Task<IActionResult> Get_All_Item(int page_number = 1, int page_size = 10, string search = "")
        {
            return Ok(await _messageService.Get_All_Async(page_number, page_size, search));
        }

        [HttpGet("/message/get_by_id")]
        public async Task<IActionResult> Get_By_Id(int id)
        {
            return Ok(await _messageService.Get_By_Id(id));
        }

        [HttpPost("/message/create")]
        public async Task<IActionResult> Create_Item([FromBody] vm_create_message request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _messageService.Create_Item(request));
        }

        [HttpPost("/message/batch_create")]
        public async Task<IActionResult> Batch_Create_Item([FromBody] List<vm_create_message> requests)
        {
            if (requests == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _messageService.Batch_Create_Item(requests));
        }

        [HttpPut("/message/modified")]
        public async Task<IActionResult> Modified_Item(int id, [FromBody] vm_update_message request)
        {
            if (request == null) { return BadRequest("request null check object again, make sure request have a value"); }
            return Ok(await _messageService.Modified_Item(id, request));
        }

        [HttpDelete("/message/remove")]
        public async Task<IActionResult> Remove_Item(int id)
        {
            return Ok(await _messageService.Remove_Item(id));
        }

        [HttpDelete("/message/batch_remove")]
        public async Task<IActionResult> Batch_Remove_Item(List<int> ids)
        {
            return Ok(await _messageService.Batch_Remove_Item(ids));
        }
    }
}
