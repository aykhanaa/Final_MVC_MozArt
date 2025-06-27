using Final_MozArt.Data;
using Final_MozArt.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stripe.Checkout;

namespace Final_MozArt.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AppDbContext _context;

        public PaymentController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Checkout()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            var basketCookie = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(basketCookie))
            {
                
                return RedirectToAction("Index", "Basket");
            }

            var basketItems = JsonConvert.DeserializeObject<List<BasketDetailVM>>(basketCookie);
            var data = await _context.Products.FirstOrDefaultAsync(m=>m.Id==basketItems.FirstOrDefault().Id);
            var domain = "https://localhost:7286/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "home/index",
                CancelUrl = domain + "account/login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
            };

            foreach (var item in basketItems)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(data.Price* basketItems.FirstOrDefault().Count * 100),  
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = data.Name,
                        },
                    },
                    Quantity = basketItems.Count,
                };

                options.LineItems.Add(sessionListItem);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

    }
}
