using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Areas.Admin.Controllers
{
    [Area("Admin")]
   //[Authorize(Roles = "SuperAdmin, Admin")]
    public class MainController : Controller
    {

    }
}
