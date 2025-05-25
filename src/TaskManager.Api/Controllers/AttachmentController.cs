using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Classes;
using TaskManager.Consts;
using TaskManager.Interfaces.Services;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Api.Controllers
{
    [Route("api/tasks/{taskId}/attachments")]
    [ApiController]
    [Authorize]
    public class AttachmentController : ApiControllerBase
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
        public IActionResult GetAttachments(int taskId)
        {
            var userId = GetCurrentUserId();
            var attachments = _serviceManager.Attachment.GetAttachments(
                taskId,
                userId,
                trackChanges: false
            );
            return Ok(attachments);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult GetAttachment(int taskId, int id)
        {
            var userId = GetCurrentUserId();
            var attachment = _serviceManager.Attachment.GetAttachment(
                taskId,
                id,
                userId,
                trackChanges: false
            );
            return Ok(attachment);
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult CreateAttachment(
            int taskId,
            [FromBody] AttachmentForManipulationDTO attachment
        )
        {
            var userId = GetCurrentUserId();
            if (attachment is null)
            {
                return BadRequest("Attachment is null");
            }
            var attachmentDB = _serviceManager.Attachment.CreateAttachment(
                taskId,
                userId,
                attachment
            );
            return CreatedAtAction(
                nameof(GetAttachment),
                new
                {
                    taskId,
                    id = attachmentDB.AttachmentId,
                    userId,
                },
                attachmentDB
            );
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult UpdateAttachment(
            int taskId,
            int id,
            [FromBody] AttachmentForManipulationDTO attachment
        )
        {
            var userId = GetCurrentUserId();
            if (attachment is null)
            {
                return BadRequest("Attachment is null");
            }
            _serviceManager.Attachment.UpdateAttachment(taskId, id, userId, attachment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult DeleteAttachment(int taskId, int id)
        {
            var userId = GetCurrentUserId();
            _serviceManager.Attachment.DeleteAttachment(taskId, id, userId);
            return NoContent();
        }
    }
}
