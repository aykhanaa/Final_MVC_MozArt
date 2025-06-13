using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Controllers
{
    public class ShopController : Controller
    {
        private readonly ISettingService _settingService;



        public ShopController(ISettingService settingService)
        {
            _settingService = settingService;



        }
        public async Task<IActionResult> Index()
        {
            var setting = _settingService.GetSettings();


            ShopVM model = new ShopVM()
            {
                Setting = setting,


            };
            return View(model);
        }
    }
}
