using GPU_Scraper.Entities;
using GPUScraper.Models.Models;

namespace GPU_Scraper.Services.Contracts
{
    public interface IGPUScraperService
    {
        IEnumerable<GPU> CrawlGPUs();
        IEnumerable<GPUDto> ScrapGPUs();
        void SerializedToJson();
    }
}
