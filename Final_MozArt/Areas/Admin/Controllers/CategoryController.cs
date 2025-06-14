using AutoMapper;
using Final_MozArt.Helpers.Extensions;
using Final_MozArt.Services;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Areas.Admin.Controllers
{
    public class CategoryController : MainController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Error");

            var category = await _categoryService.GetByIdAsync(id.Value);
            if (category == null)
                return RedirectToAction("Index", "Error");

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Error");

            var category = await _categoryService.GetByIdAsync(id.Value);
            if (category == null)
                return NotFound();

            var model = new CategoryEditVM
            {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryEditVM request)
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

            await _categoryService.EditAsync(request);
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            //return RedirectToAction(nameof(Index));
            return Ok();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
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

            await _categoryService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }
    }
}
