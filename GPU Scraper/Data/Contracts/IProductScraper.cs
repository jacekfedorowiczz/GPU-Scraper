using GPU_Scraper.Entities;
using GPUScraper.Models.Models;

namespace GPU_Scraper.Data.Contracts
{
    public interface IProductScraper
    {
        IEnumerable<Product> ScrapProducts();
    }
}
