using GPUScraper.Models.Models;

namespace GPU_Scraper.Services.Contracts
{
    public interface IGPUScraperService
    {
        IEnumerable<ProductToGPU> ScrapGPUs();
    }
}
