using Final_MozArt.Models;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Product;
using Final_MozArt.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Final_MozArt.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IInstagramService _instagramService;
        private readonly IProductService _productService;


        public HomeController(ISliderService sliderService, 
                              ICategoryService categoryService,
                              IBrandService brandService,
                              IInstagramService instagramService,
                              IProductService productService)
        {
            _sliderService = sliderService;
            _categoryService = categoryService;
            _brandService = brandService;
            _instagramService = instagramService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var brands = await _brandService.GetAllAsync();
            var instagrams = await _instagramService.GetAllAsync();
            var products = await _productService.GetAllAsync();


            HomeVM model = new HomeVM()
            {
                Sliders = sliders,
                Categories = categories,
                Brands = brands,
                Instagrams = instagrams,
                Products = products,


            };

            return View(model);

        }
        public async Task<IActionResult> Search(string query)
        {
            var products = await _productService.SearchProductsAsync(query); // List<Product>

            var productVMs = products.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                CategoryName = p.Category?.Name
            }).ToList();

            var model = new HomeVM
            {
                Products = productVMs // IEnumerable<ProductVM>
                                      // dig?r sah?l?r
            };

            return View("Index", model);
        }



    }
}
