using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CmsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessDBController(IAccessImportService access) : ControllerBase
    {
        // TODO: Create Access DB Upload Endpoint
        // Status: In Progress
        IAccessImportService _accessImportService = access;
        [HttpPost("upload-access-db")]
        public async Task<IActionResult> UploadAccessDb(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".accdb");
            using var stream = System.IO.File.Create(tempPath);
            await file.CopyToAsync(stream);
            stream.Close();

            await _accessImportService.Import(tempPath); // generic handler
            return Ok("Import started.");
        }
    }
}
