using GPU_Scraper.Data.Contracts;
using GPU_Scraper.Services.Contracts;
using GPUScraper.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace GPU_Scraper.Controllers
{
    [Route("api/scraper")]
    public class ScraperController : ControllerBase
    {
        private readonly IGPUScraperService _scraperService;

        public ScraperController(IGPUScraperService scraperService )
        {
            _scraperService = scraperService;
        }

        [HttpGet("getall")]
        public IEnumerable<GPUDto> GetGPUs()
        {
            var result = _scraperService.ScrapGPUs();

            if (!result.Any())
            {
                throw new NotFoundException();
            }

            return result;
        }
    }
}
