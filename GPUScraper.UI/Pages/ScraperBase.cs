using GPUScraper.Models.Models;
using GPUScraper.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace GPUScraper.UI.Pages
{
    public class ScraperBase : ComponentBase
    {
        [Inject]
        public IScraperService ScraperService { get; set; }

        public IEnumerable<GPUDto> GPUDtos { get; set; }
        protected bool _isCrawled { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var isCrawled = await ScraperService.CrawlGPUs();
                isCrawled = true;
                return;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
