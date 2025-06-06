using Final_MozArt.Helpers.Extensions;
using Final_MozArt.Models;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Areas.Admin.Controllers
{
    public class SettingController : MainController
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _settingService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return RedirectToAction("Index", "Error");

            var setting = await _settingService.GetByIdAsync(id.Value);
            if (setting == null) return RedirectToAction("Index", "Error");

            var model = new SettingEditVM
            {
                Id = setting.Id,
                Key = setting.Key,
                Value = setting.Value
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SettingEditVM setting)
        {
            if (id is null || id != setting.Id) return RedirectToAction("Index", "Error");

            var dbSetting = await _settingService.GetByIdAsync(id.Value);
            if (dbSetting == null) return RedirectToAction("Index", "Error");

            setting.Key = dbSetting.Key;
            setting.Value = dbSetting.Value;

            if (setting.Photo != null)
            {
                if (!setting.Photo.CheckFileType("image/"))
                    ModelState.AddModelError("Photo", "Only image files allowed");

                if (!setting.Photo.CheckFileSize(2048))
                    ModelState.AddModelError("Photo", "Max file size is 2MB");
            }

            if (!ModelState.IsValid)
                return View(setting);

            await _settingService.EditAsync(setting);
            return RedirectToAction(nameof(Index));
        }
    }
}
