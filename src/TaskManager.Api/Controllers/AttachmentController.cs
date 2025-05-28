using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Classes;
using TaskManager.Api.Classes.Utilities;
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
        private readonly FilesHandler _fileHandler;

        public AttachmentController(
            ILogger<AttachmentController> logger,
            IServiceManager serviceManager,
            FilesHandler filesHandler
        )
        {
            _logger = logger;
            _serviceManager = serviceManager;
            _fileHandler = filesHandler;
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
            var file = _fileHandler.GetFileByPath(attachment.FilePath);
            return file;
        }

        [HttpPost]
        [Authorize(Policy = UserRoles.Developer)]
        public IActionResult CreateAttachment(int taskId, IFormFile file)
        {
            var userId = GetCurrentUserId();
            if (file is null)
            {
                return BadRequest("Attachment is null");
            }

            var newFilePath = _fileHandler.UploadFile(file);
            var attachment = new AttachmentForManipulationDTO()
            {
                TaskId = taskId,
                FilePath = newFilePath,
            };

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
            var filePath = _serviceManager.Attachment.DeleteAttachment(taskId, id, userId);
            _fileHandler.DeleteFileAsync(filePath).GetAwaiter().GetResult();
            return NoContent();
        }
    }
}
