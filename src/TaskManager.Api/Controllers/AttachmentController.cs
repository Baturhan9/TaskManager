using Microsoft.AspNetCore.Mvc;
using TaskManager.Interfaces.Services;
using TaskManager.Models.CreateModelObjects;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly ILogger<AttachmentController> _logger;
        private readonly IServiceManager _serviceManager;

        public AttachmentController(
            ILogger<AttachmentController> logger,
            IServiceManager serviceManager
        )
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public IActionResult GetAttachments()
        {
            var attachments = _serviceManager.Attachment.GetAttachments(trackChanges: false);
            return Ok(attachments);
        }

        [HttpGet("{id}")]
        public IActionResult GetAttachment(int id)
        {
            var attachment = _serviceManager.Attachment.GetAttachment(id, trackChanges: false);
            return Ok(attachment);
        }

        [HttpGet("task/{taskId}")]
        public IActionResult GetAttachmentsByTaskId(int taskId)
        {
            var attachments = _serviceManager.Attachment.GetAttachmentsByTaskId(
                taskId,
                trackChanges: false
            );
            return Ok(attachments);
        }

        [HttpPost]
        public IActionResult CreateAttachment([FromBody] AttachmentCreateDTO attachment)
        {
            if (attachment == null)
            {
                return BadRequest("Attachment is null");
            }
            _serviceManager.Attachment.CreateAttachment(attachment);
            return CreatedAtAction(nameof(GetAttachment), attachment);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAttachment(int id, [FromBody] AttachmentCreateDTO attachment)
        {
            if (attachment == null)
            {
                return BadRequest("Attachment is null");
            }
            _serviceManager.Attachment.UpdateAttachment(id, attachment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAttachment(int id)
        {
            _serviceManager.Attachment.DeleteAttachment(id);
            return NoContent();
        }
    }
}
