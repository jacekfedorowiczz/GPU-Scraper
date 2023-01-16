using AutoMapper;
using GPU_Scraper.Data;
using GPU_Scraper.Entities;
using GPU_Scraper.Middlewares.Exceptions;
using GPU_Scraper.Services.Contracts;
using GPUScraper.Maps;
using GPUScraper.Models.Models;
using GPUScraper.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public GPUScraperService(GPUScraperDbContext dbContext, XkomCrawler xkom, MoreleCrawler morele, GPUCrawler crawler, GPUUpdater updater, IMapper mapper, IFileService fileService)
        {
            _dbContext = dbContext;
            _xkom = xkom;
            _morele = morele;
            _crawler = crawler;
            _updater = updater;
            _mapper = mapper;
        }

        public async Task CrawlGPUs()
        {
            var gpusFromDatabase = await _dbContext.GPUs.ToListAsync();

            if (!gpusFromDatabase.Any())
            {
                var crawledGPUs = await GetGPUsFromWebsites();

                if (!crawledGPUs.Any())
                {
                    throw new Exception();
                }

                await _dbContext.GPUs.AddRangeAsync(crawledGPUs);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                await UpdatePrices();
            }
        }

        public async Task<PageResult<GPUDto>> GetGPUs(GPUQuery query)
        {
            var baseQuery = _dbContext.GPUs
                                    .Where(x => query.searchPhrase == null || (x.Name.ToLower().Contains(query.searchPhrase.ToLower())));

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<GPU, object>>>()
                {
                    {nameof(GPU.Name), x => x.Name },
                    {nameof(GPU.LowestPrice), x => x.LowestPrice },
                    {nameof(GPU.HighestPrice), x => x.HighestPrice }
                };
                var selectedColumn = columnSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC 
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var GPUs = baseQuery
                            .Skip(query.pageSize * (query.pageNumber - 1))
                            .Take(query.pageSize)
                            .ToList();

            var totalGPUsCount = GPUs.Count;

            if (!GPUs.Any())
            {
                throw new NotFoundException("Nie znaleziono kart w bazie danych! W pierwszej kolejności scrawluj strony internetowe.");
            }

            var dtos = _mapper.Map<List<GPUDto>>(GPUs);
            var result = new PageResult<GPUDto>(dtos, totalGPUsCount, query.pageSize, query.pageNumber);

            return result;
        }

        private async Task UpdatePrices()
        {
            var crawledGPUs = await GetGPUsFromWebsites();

            if (!crawledGPUs.Any())
            {
                throw new Exception();
            }

            var gpusToUpdate = await _updater.UpdateGPUs(crawledGPUs);

            if (!gpusToUpdate.Any())
            {
                return;
            }

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

        private async Task<IEnumerable<GPU>> GetGPUsFromWebsites()
        {
            var xkom = await _xkom.CrawlProducts();
            var morele = await _morele.CrawlProducts();
            var crawledGPUs = await _crawler.CrawlNewGPUs(xkom, morele);

            return crawledGPUs;
        }

    } 
}

