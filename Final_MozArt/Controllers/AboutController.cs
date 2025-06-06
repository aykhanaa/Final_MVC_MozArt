using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.About;
using Final_MozArt.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly ISupportService _supportService;
        private readonly IInstagramService _instagramService;
        private readonly ISettingService _settingService;
        private readonly IVideoService _videoService;







        public AboutController(IAboutService aboutService,
                               ISupportService supportService,
                               IInstagramService ınstagramService,
                               ISettingService settingService,
                               IVideoService videoService)

        {
            _aboutService = aboutService;
            _supportService = supportService;
            _instagramService = ınstagramService;
            _settingService = settingService;
            _videoService = videoService;



        }
        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutService.GetAllAsync();
            var supports = await _supportService.GetAllAsync();
            var instagrams = await _instagramService.GetAllAsync();
            var setting = _settingService.GetSettings();
            var videos = await _videoService.GetAllAsync();




            AboutUIVM model = new AboutUIVM()
            {
                Abouts = abouts,
                Supports = supports,
                Instagrams= instagrams,
                Setting = setting,
                Videos= videos


            };



            return View(model);
        }
    }
}
