using GPU_Scraper.Controllers;
using GPU_Scraper.Services;
using GPU_Scraper.Services.Contracts;
using Moq;

namespace GPUScraper.Tests
{
    public class ScraperControllerTests
    {
        private readonly ScraperController scraperController = new();

        [Fact]
        public async Task GetGPUs_()
        {
            //Arrange
            var serviceMock = new Mock<IGPUScraperService>();

            serviceMock.Setup(() =>
            {

            });

            var controller = new ScraperController(IGPUScraperService serviceMock);

            //Act



            //Assert
        }
    }
}