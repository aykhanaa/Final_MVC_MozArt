using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Controllers
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
