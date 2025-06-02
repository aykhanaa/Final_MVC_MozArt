using Final_MozArt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Final_MozArt.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
