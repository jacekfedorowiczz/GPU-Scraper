using GPU_Scraper.Entities;
using GPU_Scraper.Middlewares.Exceptions;
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

        [HttpGet("scrap")]
        public ActionResult<IEnumerable<GPUDto>> GetGPUs([FromQuery]string searchPhrase)
        {
            var result = _scraperService.GetGPUs(searchPhrase);
            return Ok(result);
        }

        [HttpGet("crawl")]
        public async Task<ActionResult> CrawlGPUs()
        {
            await _scraperService.CrawlGPUs();
            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteGPU([FromQuery]int GPUId)
        {
            _scraperService.DeleteGPU(GPUId);
            return NoContent();
        }
    }
}
