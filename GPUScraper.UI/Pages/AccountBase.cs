using GPUScraper.UI.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace GPUScraper.UI.Pages
{
    public class AccountBase : ComponentBase
    {
        [Inject]
        public IAccountService AccountService { get; set; }

    }
}
