using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
