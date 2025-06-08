using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Final_MozArt.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /Product
        public async Task<IActionResult> Index(int page = 1, int take = 10)
        {
            var products = await _productService.GetPaginatedDatasAsync(page, take);
            return View(products);
        }

        // GET: /Product/Detail/5
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productService.GetByIdWithIncludesWithoutTrackingAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _productService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdWithIncludesAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        // POST: /Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            await _productService.EditAsync(vm);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: /Product/DeleteImage/5
        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            await _productService.DeleteProductImageAsync(id);
            return Json(new { success = true });
        }

        // POST: /Product/SetMainImage
        [HttpPost]
        public async Task<IActionResult> SetMainImage(SetIsMainVM vm)
        {
            await _productService.SetMainImageAsync(vm);
            return Json(new { success = true });
        }
    }
}
