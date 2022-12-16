using GPU_Scraper.Data;
using GPU_Scraper.Entities;
using GPU_Scraper.Middlewares.Exceptions;
using GPU_Scraper.Services.Contracts;
using GPUScraper.Models.Models;
using Newtonsoft.Json;

namespace GPU_Scraper.Services
{
    public class GPUScraperService : IGPUScraperService
    {
        private readonly GPUScraperDbContext _dbContext;
        private readonly XkomScraper _xkom;
        private readonly MoreleScraper _morele;

        public GPUScraperService(GPUScraperDbContext dbContext, XkomScraper xkom, MoreleScraper morele)
        {
            _dbContext = dbContext;
            _xkom = xkom;
            _morele= morele;
        }

        public IEnumerable<GPU> CrawlGPUs()
        {
            var xkom = ScrapXkom();
            var morele = ScrapMorele();
            var gpuList = new List<GPU>();

            throw new NotImplementedException();

        }

        public IEnumerable<GPUDto> ScrapGPUs()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Product> ScrapXkom()
        {
            var resultXkom = _xkom.ScrapProducts();

            if (!resultXkom.Any())
            {
                throw new Exception();
            }

            return resultXkom;
        }

        private IEnumerable<Product> ScrapMorele()
        {
            var resultMorele = _morele.ScrapProducts();

            if (!resultMorele.Any())
            {
                throw new Exception();
            }

            return resultMorele;
        }


        public void SerializedToJson()
        {
            var xkomGPUs = ScrapXkom();
            var moreleGPUs = ScrapMorele();
            var xkom = new List<Product>();
            var morele = new List<Product>();
            

            foreach (var gpu in xkomGPUs)
            {
                xkom.Add(gpu);
            }
            string jsonXkom = JsonConvert.SerializeObject(xkom);
            File.WriteAllText(@"C:\Users\jacek\Desktop\GPU Scraper\GPU Scraper\GPU Scraper\JSON\xkomSerialized.json", jsonXkom);

            foreach (var gpu in moreleGPUs)
            {
                morele.Add(gpu);
            }
            string jsonMorele = JsonConvert.SerializeObject(morele);
            File.WriteAllText(@"C:\Users\jacek\Desktop\GPU Scraper\GPU Scraper\GPU Scraper\JSON\moreleSerialized.json", jsonMorele);
        }
    }
}
