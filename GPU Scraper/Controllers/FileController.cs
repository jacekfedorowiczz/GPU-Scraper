using GPUScraper.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Owin.StaticFiles.ContentTypes;
using System.IO;

namespace GPUScraper.Controllers
{
    [ApiController]
    [Route("api/json")]
    public class FileController : ControllerBase
    {
        private const string filePath = @"..\GPU Scraper\JSON\GPUs.json";
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet("get")]
        [Authorize]
        [ResponseCache(Duration = 300)]
        public ActionResult GetJson()
        {
            _fileService.SerializeToJson();
            var isFileExists = System.IO.File.Exists(filePath);

            if (!isFileExists) 
            { 
                return NotFound();
            }
            else
            {
                var contentProvider = new FileExtensionContentTypeProvider();
                var fileContents = System.IO.File.ReadAllBytes(filePath);

                contentProvider.TryGetContentType(filePath, out var contentType);

                return File(fileContents, contentType, "GPUs.json");
            }
        }
    }
}
