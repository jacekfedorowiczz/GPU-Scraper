using GPUScraper.Models.Models;

namespace GPU_Scraper.Data.Contracts
{
    public interface ICrawler
    {
        Task<List<Product>> CrawlProducts();
    }
}
