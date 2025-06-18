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
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private  readonly ITagService _tagService;



        public ShopController(ISettingService settingService,
                              IProductService productService,
                              ICategoryService categoryService,
                              IBrandService brandService,
                              ITagService tagService)
        {
            _settingService = settingService;
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
            _tagService = tagService;
        }
        public async Task<IActionResult> Index()
        {
            var setting = _settingService.GetSettings();
            var product = await _productService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var categoryiesWithProductCount = await _categoryService.GetProductCountByCategoryNameAsync();
            var brandWithProductCount = await _brandService.GetProductCountByBrandNameAsync();
            var brands = await _brandService.GetAllAsync();
            var tags = await _tagService.GetAllAsync();

            ShopVM model = new ShopVM()
            {
                Setting = setting,
                Products = product,
                Categories = categories,
                ProductCount = categoryiesWithProductCount,
                Brands = brands,
                Tags = tags,
                BrandsProductCount = brandWithProductCount
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
