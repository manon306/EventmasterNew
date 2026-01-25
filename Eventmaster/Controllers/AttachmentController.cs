namespace Eventmaster.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentServices _attachmentService;
        public AttachmentController(IAttachmentServices attachmentService)
        {
            _attachmentService = attachmentService;
        }
        [Authorize(Roles = "Organizer,Admin")]
        [HttpPost("Upload/{eventId}")]
        public async Task<IActionResult> UploadAttachment(int eventId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            var result = await _attachmentService.UploadAsync(eventId, file);

            if (!result.success)
                return BadRequest(result.message);

            return Ok(result.message);
        }
        [Authorize(Roles = "Registered,Participant,Organizer,Admin")]
        [HttpGet("Download/{attachmentId}")]
        public IActionResult Download(int attachmentId)
        {
            var result = _attachmentService.GetFile(attachmentId, out string filePath, out string fileName);

            if (!result.success)
                return BadRequest(result.message);

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", filePath);

            var contentType = "application/octet-stream";
            return PhysicalFile(fullPath, contentType, fileName);
        }


    }

}
