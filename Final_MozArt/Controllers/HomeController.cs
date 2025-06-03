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




        public HomeController(ISliderService sliderService, 
                              ICategoryService categoryService)
        {
            _sliderService = sliderService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();



            HomeVM model = new HomeVM()
            {
                Sliders = sliders,
                Categories = categories
            };

            return View(model);

        }

    }
}
