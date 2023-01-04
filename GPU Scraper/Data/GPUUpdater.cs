using GPU_Scraper.Entities;
using Microsoft.EntityFrameworkCore;

namespace GPU_Scraper.Data
{
    public class GPUUpdater
    {
        private readonly GPUScraperDbContext _dbContext;

        public GPUUpdater(GPUScraperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GPU>> UpdateGPUs(IEnumerable<GPU> crawledGPUs)
        {
            var GPUsFromDatabase = _dbContext.GPUs.ToList();
            var updatedGPUs = new List<GPU>();

            if (!GPUsFromDatabase.Any())
            {
                return null;
            }

            foreach (var GPU in GPUsFromDatabase)
            {
                var crawledGPU = crawledGPUs.Where(x => x.Name.ToLower() == GPU.Name.ToLower()).FirstOrDefault();

                if (crawledGPU is null)
                {
                    continue;
                }
                
                var result = await UpdateGPUPrices(GPU, crawledGPU);
                updatedGPUs.Add(result);
            }

            if (!updatedGPUs.Any())
            {
                return null;
            }

            return updatedGPUs;
        }

        private async Task<GPU> UpdateGPUPrices(GPU gpu, GPU crawledGPU)
        {
            if (gpu.LowestPrice == crawledGPU.LowestPrice && gpu.HighestPrice == crawledGPU.HighestPrice)
            {
                return gpu;
            }

            if (gpu.LowestPrice > crawledGPU.LowestPrice)
            {
                gpu.LowestPrice = crawledGPU.LowestPrice;
                
            }
            else if (gpu.HighestPrice < crawledGPU.HighestPrice)
            {
                gpu.HighestPrice = crawledGPU.HighestPrice;

            }

            return gpu;
        }
    }
}
