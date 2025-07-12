using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CmsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessDBController : ControllerBase
    {
        [HttpPost("upload-access-db")]
        public async Task<IActionResult> UploadAccessDb(IFormFile file)
        {
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".accdb");
            using var stream = System.IO.File.Create(tempPath);
            await file.CopyToAsync(stream);
            stream.Close();

            await _accessImportService.Import(tempPath); // generic handler
            return Ok("Import started.");
        }

    }
}
