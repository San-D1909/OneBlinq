using System.Collections.Generic;
using Backend.DTO.In;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;

public class StripeOptions
{
    public string option { get; set; }
}

namespace Backend.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CheckoutApiController : Controller
    {
        private IConfiguration _config;
        public CheckoutApiController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("create-checkout-session")]
        public ActionResult Create()
        {

            var priceId = Request.Form["priceId"];

            var domain = _config["DOMAIN"];
            var options = new SessionCreateOptions
            {
                //CustomerEmail = Request.Form["email"],
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    Price = priceId,
                    Quantity = 1,
                  }
                },
                Mode = "payment",
                SuccessUrl = domain + "?success=true",
                CancelUrl = domain + "?canceled=true",
                AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
