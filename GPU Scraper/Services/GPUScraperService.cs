using AutoMapper;
using GPU_Scraper.Data;
using GPU_Scraper.Entities;
using GPU_Scraper.Middlewares.Exceptions;
using GPU_Scraper.Services.Contracts;
using GPUScraper.Maps;
using GPUScraper.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

namespace GPU_Scraper.Services
{
    public class GPUScraperService : IGPUScraperService
    {
        private readonly GPUScraperDbContext _dbContext;
        private readonly XkomCrawler _xkom;
        private readonly MoreleCrawler _morele;
        private readonly GPUCrawler _crawler;
        private readonly GPUUpdater _updater;
        private readonly IMapper _mapper;

        public GPUScraperService(GPUScraperDbContext dbContext, XkomCrawler xkom, MoreleCrawler morele, GPUCrawler crawler, GPUUpdater updater, IMapper mapper)
        {
            _dbContext = dbContext;
            _xkom = xkom;
            _morele = morele;
            _crawler = crawler;
            _updater = updater;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GPU>> CrawlGPUs()
        {
            var crawledGPUs = await GetGPUs();

            if (!crawledGPUs.Any())
            {
                throw new Exception();
            }

            _dbContext.GPUs.AddRange(crawledGPUs);
            _dbContext.SaveChanges();

            return crawledGPUs;
        }

        public IEnumerable<GPUDto> ScrapGPUs()
        {
            var GPUs = _dbContext.GPUs.ToList();
            var result = new List<GPUDto>();

            if (!GPUs.Any())
            {
                throw new NotFoundException("Nie znaleziono kart w bazie danych! W pierwszej kolejności scrawluj strony internetowe.");
            }

            foreach (var gpu in GPUs)
            {
                var dto = _mapper.Map<GPUDto>(gpu);
                result.Add(dto);
            }

            return result;
        }

        public async Task UpdateGPUs()
        {
            var GPUsFromDatabase = await _dbContext.GPUs.ToListAsync();
            var crawledGPUs = await GetGPUs();

            if (!GPUsFromDatabase.Any())
            {
                throw new NotFoundException("Nie znaleziono kart w bazie danych! W pierwszej kolejności scrawluj strony internetowe.");
            }
            else if (!crawledGPUs.Any())
            {
                throw new Exception();
            }

            var gpusToUpdate = await _updater.UpdateGPUs(crawledGPUs);

            foreach (var gpu in gpusToUpdate)
            {
                var GPU = _dbContext.GPUs.Where(x => x.Name == gpu.Name).FirstOrDefault();

                GPU.LowestPrice = gpu.LowestPrice;
                GPU.HighestPrice = gpu.HighestPrice;
            }

            await _dbContext.SaveChangesAsync();
        }

        public void DeleteGPU(int GPUId)
        {
            var GPU = _dbContext.GPUs.Where(x => x.Id == GPUId).FirstOrDefault();

            if (GPU is null)
            {
                throw new NotFoundException("Nie znaleziono w bazie danych karty graficznej o podanym Id!");
            }

            _dbContext.GPUs.Remove(GPU);
            _dbContext.SaveChanges();
        }

        private async Task<IEnumerable<GPU>> GetGPUs()
        {
            var xkom = await _xkom.CrawlProducts();
            var morele = await _morele.CrawlProducts();
            var crawledGPUs = await _crawler.CrawlNewGPUs(xkom, morele);

            return crawledGPUs;
        }
    } 
}

