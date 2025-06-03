using Final_MozArt.Models;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Final_MozArt.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;
        private readonly IInstagramService _instagramService;






        public HomeController(ISliderService sliderService, 
                              ICategoryService categoryService,
                              IBrandService brandService,
                              IInstagramService instagramService)
        {
            _sliderService = sliderService;
            _categoryService = categoryService;
            _brandService = brandService;
            _instagramService = instagramService;
        }
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var brands = await _brandService.GetAllAsync();
            var instagrams = await _instagramService.GetAllAsync();



            HomeVM model = new HomeVM()
            {
                Sliders = sliders,
                Categories = categories,
                Brands = brands,
                Instagrams = instagrams
                
                
                
               
            };

            return View(model);

        }

    }
}
