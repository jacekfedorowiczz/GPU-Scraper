using GPUScraper.Models.Models;
using GPUScraper.UI.Services.Contracts;

namespace GPUScraper.UI.Services
{
    public class ScraperService : IScraperService
    {
        public bool IsCrawled { get; set; } = false;
        private readonly HttpClient _httpClient;

        public ScraperService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CrawlGPUs()
        {
            if (IsCrawled == true)
            {
                return IsCrawled;
            }

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
