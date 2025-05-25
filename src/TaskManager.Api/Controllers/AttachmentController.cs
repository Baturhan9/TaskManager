using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/tasks/{taskId}/attachments")]
    [ApiController]
    [Authorize]
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
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetAttachments()
        {
            var attachments = _serviceManager.Attachment.GetAttachments(trackChanges: false);
            return Ok(attachments);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetAttachment(int id)
        {
            var attachment = _serviceManager.Attachment.GetAttachment(id, trackChanges: false);
            return Ok(attachment);
        }

        [HttpGet("task/{taskId}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetAttachmentsByTaskId(int taskId)
        {
            var attachments = _serviceManager.Attachment.GetAttachmentsByTaskId(
                taskId,
                trackChanges: false
            );
            return Ok(attachments);
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult CreateAttachment([FromBody] AttachmentForManipulationDTO attachment)
        {
            if (attachment == null)
            {
                return BadRequest("Attachment is null");
            }
            var attachmentDB = _serviceManager.Attachment.CreateAttachment(attachment);
            return CreatedAtAction(
                nameof(GetAttachment),
                new { id = attachmentDB.AttachmentId },
                attachmentDB
            );
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult UpdateAttachment(
            int id,
            [FromBody] AttachmentForManipulationDTO attachment
        )
        {
            if (attachment == null)
            {
                return BadRequest("Attachment is null");
            }
            _serviceManager.Attachment.UpdateAttachment(id, attachment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult DeleteAttachment(int id)
        {
            _serviceManager.Attachment.DeleteAttachment(id);
            return NoContent();
        }
    }
}
