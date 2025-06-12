using Final_MozArt.Models;
using Final_MozArt.ViewModels.Account;
using Microsoft.AspNetCore.Identity;

namespace Final_MozArt.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterVM request);
        Task<SignInResult> LoginAsync(LoginVM request);
        
    }
}
