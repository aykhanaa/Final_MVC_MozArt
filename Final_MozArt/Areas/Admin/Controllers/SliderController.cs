using AutoMapper;
using Final_MozArt.Helpers.Extensions;
using Final_MozArt.Models;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Slider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Final_MozArt.Areas.Admin.Controllers
{
    public class SliderController : MainController
    {
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;

        public SliderController(ISliderService sliderService, IMapper mapper)
        {
            _sliderService = sliderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            return View(sliders);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Error");

            var slider = await _sliderService.GetByIdAsync(id.Value);
            if (slider == null)
                return RedirectToAction("Index", "Error");

            return View(slider);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Error");

            var slider = await _sliderService.GetEntityByIdAsync(id.Value);
            if (slider == null)
                return NotFound();

            var model = new SliderEditVM
            {
                Id = slider.Id,
                Title = slider.Title,
                Image = slider.Image
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SliderEditVM request)
        {
            if (id != request.Id) return BadRequest();

            if (!ModelState.IsValid) return View(request);

            if (request.Photo != null)
            {
                if (!request.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Only image files are allowed");
                    return View(request);
                }

                if (!request.Photo.CheckFileSize(2048))
                {
                    ModelState.AddModelError("Photo", "Max file size is 2MB");
                    return View(request);
                }
            }

            await _sliderService.EditAsync(request);
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
                return View();

            if (!request.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Only image files are allowed");
                return View();
            }

            if (!request.Photo.CheckFileSize(2048))
            {
                ModelState.AddModelError("Photo", "Max file size is 2MB");
                return View();
            }

            await _sliderService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
    }
}
