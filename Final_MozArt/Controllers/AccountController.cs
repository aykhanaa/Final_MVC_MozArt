
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

namespace Final_MozArt.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {

            if (!ModelState.IsValid) return View(request);

            var result = await _accountService.RegisterAsync(request);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(request);
            }

            return RedirectToAction(nameof(VerifyEmail));
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _accountService.LoginAsync(request);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login informations is wrong");
                return View(request);
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
