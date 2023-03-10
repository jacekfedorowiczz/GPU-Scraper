using GPUScraper.Models.Models;
using GPUScraper.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GPUScraper.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("/register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("/login")]
        public ActionResult<string> Login([FromBody]LoginUserDto dto)
        {
            var token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
