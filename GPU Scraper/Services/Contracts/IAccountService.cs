using GPUScraper.Models.Models;

namespace GPUScraper.Services.Contracts
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginUserDto dto);
    }
}
