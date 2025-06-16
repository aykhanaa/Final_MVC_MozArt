using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Product;
using Final_MozArt.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Controllers
{
    public class ShopController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IProductService _productService;



        public ShopController(ISettingService settingService,
                              IProductService productService)
        {
            _settingService = settingService;
            _productService = productService;



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

        public async Task<IActionResult> Detail(int id)
        {
            var model = await _productService.GetByIdWithIncludesWithoutTrackingAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }
    }
}
