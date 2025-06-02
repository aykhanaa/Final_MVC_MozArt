using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
