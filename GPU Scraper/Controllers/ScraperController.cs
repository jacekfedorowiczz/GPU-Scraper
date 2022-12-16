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

        [HttpGet("getall")]
        public ActionResult Serialize()
        {
            _scraperService.SerializedToJson();

            return Ok();
        }

        
    }
}
