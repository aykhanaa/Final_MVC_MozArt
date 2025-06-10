using Final_MozArt.Areas.Admin.Controllers;
using Final_MozArt.Services;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Final_MozArt.Controllers
{
    public class ProductController : MainController
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        private readonly IColorService _colorService;
        private readonly ITagService _tagService;

        public ProductController(
            IProductService productService,
            IBrandService brandService,
            ICategoryService categoryService,
            IColorService colorService,
            ITagService tagService)
        {
            _productService = productService;
            _brandService = brandService;
            _categoryService = categoryService;
            _colorService = colorService;
            _tagService = tagService;
        }

        // GET: /Product
        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            var products = await _productService.GetPaginatedDatasAsync(page, take);
            return View(products);
        }

        // GET: /Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdWithIncludesWithoutTrackingAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: /Product/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = new SelectList(await _brandService.GetAllAsync(), "Id", "Name");
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
            ViewBag.Colors = new MultiSelectList(await _colorService.GetAllAsync(), "Id", "Name");
            ViewBag.Tags = new MultiSelectList(await _tagService.GetAllAsync(), "Id", "Name");

            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = new SelectList(await _brandService.GetAllAsync(), "Id", "Name");
                ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
                ViewBag.Colors = new MultiSelectList(await _colorService.GetAllAsync(), "Id", "Name");
                ViewBag.Tags = new MultiSelectList(await _tagService.GetAllAsync(), "Id", "Name");
                return View(vm);
            }

            await _productService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        // GET: /Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdWithIncludesWithoutTrackingAsync(id);
            if (product == null) return NotFound();

            var editVM = new ProductEditVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                ColorIds = product.ColorIds,
                TagIds = product.TagIds,
                Images = product.Images,
            };

            ViewBag.Brands = new SelectList(await _brandService.GetAllAsync(), "Id", "Name", editVM.BrandId);
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", editVM.CategoryId);
            ViewBag.Colors = new MultiSelectList(await _colorService.GetAllAsync(), "Id", "Name", editVM.ColorIds);
            ViewBag.Tags = new MultiSelectList(await _tagService.GetAllAsync(), "Id", "Name", editVM.TagIds);

            return View(editVM);
        }

        // POST: /Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = new SelectList(await _brandService.GetAllAsync(), "Id", "Name", vm.BrandId);
                ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", vm.CategoryId);
                ViewBag.Colors = new MultiSelectList(await _colorService.GetAllAsync(), "Id", "Name", vm.ColorIds);
                ViewBag.Tags = new MultiSelectList(await _tagService.GetAllAsync(), "Id", "Name", vm.TagIds);
                return View(vm);
            }

            await _productService.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Product/Search
        public async Task<IActionResult> Search(string query, int page = 1, int take = 10)
        {
            var products = await _productService.SearchAsync(query, page, take);
            return View("Index", products);
        }

        // GET: /Product/OrderByNameAsc
        public async Task<IActionResult> OrderByNameAsc(int page = 1, int take = 10)
        {
            var products = await _productService.OrderByNameAsc(page, take);
            return View("Index", products);
        }

        // GET: /Product/OrderByPriceDesc
        public async Task<IActionResult> OrderByPriceDesc(int page = 1, int take = 10)
        {
            var products = await _productService.OrderByPriceDesc(page, take);
            return View("Index", products);
        }

        // GET: /Product/Filter
        public async Task<IActionResult> Filter(int minPrice, int maxPrice)
        {
            var products = await _productService.FilterAsync(minPrice, maxPrice);
            return View("Index", products);
        }

        // POST: /Product/SetMainImage
        [HttpPost]
        public async Task<IActionResult> SetMainImage(SetIsMainVM model)
        {
            await _productService.SetMainImageAsync(model);
            return RedirectToAction("Edit", new { id = model.ProductId });
        }

        // POST: /Product/DeleteImage
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id, int productId)
        {
            await _productService.DeleteProductImageAsync(id);
            return RedirectToAction("Edit", new { id = productId });
        }
    }
}
