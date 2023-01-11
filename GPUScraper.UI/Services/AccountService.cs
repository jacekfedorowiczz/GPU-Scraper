using GPUScraper.Models.Models;
using GPUScraper.UI.Services.Contracts;
using System.Net.Http.Json;

namespace GPUScraper.UI.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            throw new NotImplementedException();
            //_httpClient.PostAsJsonAsync("api/account/login", dto);
            //return;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
