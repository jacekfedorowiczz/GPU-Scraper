using GPU_Scraper.Entities;
using GPUScraper.Models.Models;

namespace GPU_Scraper.Services.Contracts
{
    public interface IGPUScraperService
    {
        Task CrawlGPUs();
        Task<IEnumerable<GPUDto>> GetGPUs(string searchPhrase);
        void DeleteGPU(int GPUId);
    }
}
