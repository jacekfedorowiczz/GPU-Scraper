using GPU_Scraper.Entities;
using GPUScraper.Models.Models;

namespace GPU_Scraper.Services.Contracts
{
    public interface IGPUScraperService
    {
        Task<IEnumerable<GPU>> CrawlGPUs();
        Task<IEnumerable<GPUDto>> ScrapGPUs();
        Task UpdatePrices();
        void DeleteGPU(int GPUId);
    }
}
