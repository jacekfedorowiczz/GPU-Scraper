using GPUScraper.Models.Models;

namespace GPUScraper.UI.Services.Contracts
{
    public interface IGPUScraperService
    {
        Task<bool> CrawlGPUs();
        Task<PageResult<GPUDto>> GetGPUs(GPUQuery query);
        void DeleteGPU(int GPUId);
    }
}
