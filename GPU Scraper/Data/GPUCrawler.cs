using GPU_Scraper.Entities;
using GPUScraper.Models.Models;

namespace GPU_Scraper.Data
{
    public class GPUCrawler
    {
        public async Task<IEnumerable<GPU>> CrawlNewGPUs(IEnumerable<Product> xkom, IEnumerable<Product> morele)
        {
            var gpuList = new List<GPU>();

            foreach (var gpu in morele)
            {
                var xkomGPU = xkom.Where(x => x.Name.ToLower() == gpu.Name.ToLower()).FirstOrDefault();
                var newGPU = new GPU();

                if (xkomGPU is null)
                {
                    newGPU.Name = gpu.Name;
                    newGPU.LowestPrice = gpu.Price;
                    newGPU.LowestPriceShop = "Morele";
                    gpuList.Add(newGPU);
                }
                else
                {
                    newGPU.Name = gpu.Name;

                    if (gpu.Price <= xkomGPU.Price)
                    {
                        newGPU.LowestPrice = gpu.Price;
                        newGPU.LowestPriceShop = "Morele";
                        newGPU.HighestPrice = xkomGPU.Price;
                        newGPU.HighestPriceShop = "X-Kom";
                    }
                    else if (gpu.Price > xkomGPU.Price)
                    {
                        newGPU.LowestPrice = xkomGPU.Price;
                        newGPU.LowestPriceShop = "X-Kom";
                        newGPU.HighestPrice = gpu.Price;
                        newGPU.HighestPriceShop = "Morele";
                    }
                    gpuList.Add(newGPU);
                }
            }

            return gpuList;
        }
    }
}
