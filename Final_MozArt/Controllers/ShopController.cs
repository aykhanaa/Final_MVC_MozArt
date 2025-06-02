using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
