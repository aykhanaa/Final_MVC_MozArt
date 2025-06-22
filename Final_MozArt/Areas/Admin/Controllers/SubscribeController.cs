using Final_MozArt.Services;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Subscribe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Areas.Admin.Controllers
{
    public class SubscribeController : MainController
    {
        private readonly ISubscribeService _subscribeService;

        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }

        public async Task<IActionResult> Index()
        {
            var newsletters = await _subscribeService.GetAllAsync();

            var viewModel = new SubscribeListVM
            {
                Items = newsletters.Select(n => new SubscribeVM
                {
                    Id = n.Id,
                    Email = n.Email,
                    CreateDate = n.CreateDate
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _subscribeService.DeleteAsync(id);
            return Json(new { success = true });
        }
    }
}

