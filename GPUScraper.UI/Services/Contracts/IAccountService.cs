using GPUScraper.Models.Models;

namespace GPUScraper.UI.Services.Contracts
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginUserDto dto);
    }
}
