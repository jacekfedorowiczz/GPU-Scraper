using GPUScraper.Models.Models;
using GPUScraper.UI.Services.Contracts;

namespace GPUScraper.UI.Services
{
    public class GPUScraperService : IGPUScraperService
    {
        public bool IsCrawled { get; set; } = false;
        private readonly HttpClient _httpClient;

        public GPUScraperService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CrawlGPUs()
        {
            try
            {
                await _httpClient.GetAsync("api/scraper/crawl");
                IsCrawled = true;
                return IsCrawled;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PageResult<GPUDto>> GetGPUs(GPUQuery query)
        {
            throw new NotImplementedException();
        }

        public void DeleteGPU(int GPUId)
        {
            throw new NotImplementedException();
        }
    }
}
