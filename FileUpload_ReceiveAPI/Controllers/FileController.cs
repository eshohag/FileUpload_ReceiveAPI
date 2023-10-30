using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUpload_ReceiveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public FileController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost]
        [Route("api/FileReceive/ReceiveFileChunk")]
        public async Task<IActionResult> ReceiveFileChunk([FromForm] IFormFile fileChunk)
        {
            // Save the file chunk to disk
            var fileSavePath = Path.Combine(_environment.ContentRootPath, "UploadedFiles", fileChunk.FileName);
            using (var fileStream = new FileStream(fileSavePath, FileMode.Append, FileAccess.Write))
            {
                await fileChunk.CopyToAsync(fileStream);
            }
            return Ok();
        }
    }
}
