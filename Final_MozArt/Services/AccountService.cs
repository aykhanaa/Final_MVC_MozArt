using Final_MozArt.Helpers.Enums;
using Final_MozArt.Models;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Final_MozArt.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;

        public AccountService(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelperFactory urlHelperFactory,
            IActionContextAccessor actionContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;

            // Create UrlHelper for generating URLs
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        public async Task<IdentityResult> RegisterAsync(RegisterVM request)
        {
            AppUser user = new()
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded) return result;

            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string url = _urlHelper.Action("ConfirmEmail", "Account", new
            {
                userId = user.Id,
                token
            }, _httpContextAccessor.HttpContext.Request.Scheme);

            string subject = "Welcome to MozArt";
            string emailHtml = File.ReadAllText("wwwroot/templates/register-confirm.html")
                                   .Replace("{{link}}", url)
                                   .Replace("{{fullName}}", user.FullName);

            _emailService.Send(user.Email, subject, emailHtml);

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginVM request)
        {
            AppUser dbUser = await _userManager.FindByEmailAsync(request.EmailOrUsername);

            if (dbUser is null)
            {
                dbUser = await _userManager.FindByNameAsync(request.EmailOrUsername);
            }

            if (dbUser is null)
            {
                return SignInResult.Failed;
            }

            var result = await _signInManager.PasswordSignInAsync(dbUser, request.Password, request.IsRememberMe, false);

            return result;
        }

    }
}
