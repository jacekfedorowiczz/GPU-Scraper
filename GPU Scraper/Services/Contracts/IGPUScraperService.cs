using GPU_Scraper.Entities;
using GPUScraper.Models.Models;

namespace GPU_Scraper.Services.Contracts
{
    public interface IGPUScraperService
    {
        Task CrawlGPUs();
        Task<PageResult<GPUDto>> GetGPUs(GPUQuery query);
        void DeleteGPU(int GPUId);
    }
}
