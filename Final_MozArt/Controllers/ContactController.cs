using Final_MozArt.Models;
using Final_MozArt.Services.Interfaces;
using Final_MozArt.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;

namespace Final_MozArt.Controllers
{
    public class ContactController : Controller
    {

        private readonly IContactIntroService  _contactIntroService;


        public ContactController(IContactIntroService contactIntroService)
        {
            _contactIntroService = contactIntroService;
        }





        public async Task<IActionResult> Index()
        { 
            var contactintros = await _contactIntroService.GetAllAsync();


            ContactVM model = new ContactVM()
            {
                ContactIntros = contactintros

            };

            return View(model);
        }
    }
}
