using GPU_Scraper.Entities;
using GPUScraper.Models.Models;

namespace GPU_Scraper.Services.Contracts
{
    public interface IGPUScraperService
    {
        Task<IEnumerable<GPU>> CrawlGPUs();
        Task UpdateGPUs();
        void DeleteGPU(int GPUId);
        IEnumerable<GPUDto> ScrapGPUs();
    }
}
