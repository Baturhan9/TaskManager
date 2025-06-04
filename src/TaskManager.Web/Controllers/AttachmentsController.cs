using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using TaskManager.Web.Interfaces;

namespace TaskManager.Web.Controllers;

public class AttachmentsController : Controller
{
    private ITaskManagerClient _taskManagerClient;
    public AttachmentsController(ITaskManagerClient taskManagerClient)
    {
        _taskManagerClient = taskManagerClient;
    }

    [HttpGet]
    public async Task<IActionResult> Download(int taskId, int attachmentId, string fileName)
    {
        var fileResponse = await _taskManagerClient.GetAttachmentFile(taskId, attachmentId);

        if (!fileResponse.Success)
        {
            return NotFound();
        }

        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out var contentType))
        {
            contentType = "application/octet-stream"; 
        }
        var stream = fileResponse.Data;
        return File(stream, contentType, fileName);
    }


}