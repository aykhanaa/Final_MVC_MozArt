using Final_MozArt.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Controllers
{
    public class SubscribeController : Controller
    {
        private readonly ISubscribeService _subscribeService;

        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    TempData["Error"] = "Please enter a valid email address.";
                    return RedirectToAction("Index", "Contact");
                }

                if (!IsValidEmail(email))
                {
                    TempData["Error"] = "Please enter a valid email format.";
                    return RedirectToAction("Index", "Contact");
                }

                var emailExists = await _subscribeService.CheckEmailExistsAsync(email);
                if (emailExists)
                {
                    TempData["Error"] = "This email is already subscribed to our newsletter.";
                    return RedirectToAction("Index", "Contact");
                }

                await _subscribeService.AddAsync(email);
                TempData["Successs"] = "Thank you for subscribing to our newsletter!";
                return RedirectToAction("Index", "Contact");
            }
            catch
            {
                TempData["Error"] = "An error occurred while processing your subscription. Please try again.";
                return RedirectToAction("Index", "Contact");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}