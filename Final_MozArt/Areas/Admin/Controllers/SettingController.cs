using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Setting;
using Microsoft.AspNetCore.Mvc;
using Final_MozArt.Helpers.Extensions;
using Final_MozArt.Services;
using AutoMapper;

namespace Final_MozArt.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : MainController
    {
        private readonly ISettingService _settingService;
        private readonly IMapper _mapper;

        public SettingController(ISettingService settingService, IMapper mapper)
        {
            _settingService = settingService;
            _mapper = mapper;
        }

        // GET: Admin/Setting
        public async Task<IActionResult> Index()
        {
            var settings = await _settingService.GetAllAsync();
            return View(settings);
        }

        // GET: Admin/Setting/Detail/5
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Error");

            var setting = await _settingService.GetByIdAsync(id.Value);
            if (setting == null)
                return RedirectToAction("Index", "Error");

            var vm = _mapper.Map<SettingVM>(setting);
            return View(vm);
        }


        // GET: Admin/Setting/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Setting/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SettingCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _settingService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Setting/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var setting = await _settingService.GetByIdAsync(id.Value);
            if (setting == null) return NotFound();

            var vm = new SettingEditVM
            {
                Id = setting.Id,
                Key = setting.Key,
                Value = setting.Value
            };

            return View(vm);
        }

        // POST: Admin/Setting/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SettingEditVM vm)
        {
            if (id != vm.Id) return BadRequest();

            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _settingService.EditAsync(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Setting/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _settingService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
