using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
