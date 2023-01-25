using GPUScraper.Models.Models;
using GPUScraper.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace GPUScraper.UI.Pages
{
    public class FileBase : ComponentBase
    {
        [Inject]
        public IFileService FileService { get; set; }
        public IEnumerable<GPUDto> GPUDtos { get; set; }
        public bool _isDatabaseEmpty { get; set; }

        protected override async Task OnInitializedAsync()
        {
            
        }

    }
}
