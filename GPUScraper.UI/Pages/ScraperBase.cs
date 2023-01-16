using GPUScraper.Models.Models;
using GPUScraper.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace GPUScraper.UI.Pages
{
    public class ScraperBase : ComponentBase
    {
        [Inject]
        public IGPUScraperService ScraperService { get; set; }

        public IEnumerable<GPUDto> GPUDtos { get; set; }
        public bool IsCrawled { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var isCrawled = await ScraperService.CrawlGPUs();
            return;
        }
    }
}
