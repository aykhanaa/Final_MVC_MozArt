using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.Shop;
using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _wishlistService.GetWishlistDatasAsync());

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _wishlistService.DeleteItem(id);
            List<WishlistVM> wishlist = _wishlistService.GetDatasFromCookies();

            return Ok(wishlist.Count);
        }
    }
}