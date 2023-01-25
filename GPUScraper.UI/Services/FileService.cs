using GPUScraper.UI.Services.Contracts;

namespace GPUScraper.UI.Services
{
    public class FileService : IFileService
    {
        private readonly HttpClient _httpClient;

        public FileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async void SerializeToJson()
        {
            try
            {
                await _httpClient.GetAsync("/api/json/get");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
