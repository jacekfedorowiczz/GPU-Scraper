using GPU_Scraper.Entities;
using GPU_Scraper.Middlewares.Exceptions;
using GPUScraper.Authentication;
using GPUScraper.Entities;
using GPUScraper.Models.Models;
using GPUScraper.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GPUScraper.Services
{
    public class AccountService : IAccountService
    {
        private readonly GPUScraperDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(GPUScraperDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            if (dto == null)
            {
                throw new Exception();
            }

            var newUser = new User()
            {
                Name = dto.Name,
                Email = dto.Email,
            };

            newUser.HashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            var user = _dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
            {
                throw new BadRequestException("Podano nieprawidłowy adres email lub hasło.");
            }

            var passwordCheck = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, dto.Password);

            if (passwordCheck == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Podano nieprawidłowy adres email lub hasło.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred
                );

            var tokenHandler = new JwtSecurityTokenHandler();
            if (tokenHandler.CanWriteToken == true)
            {
                return tokenHandler.WriteToken(token);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
