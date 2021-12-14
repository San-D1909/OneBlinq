using System.Collections.Generic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
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

            var pluginId = Request.Form["pluginId"];
            var variantId = Request.Form["variantId"];

            var domain = _config["DOMAIN"];
            var options = new SessionCreateOptions
            {
                CustomerEmail = Request.Form["email"],
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // TODO: get price_id from variant
                    Price = "price_1K6C7WD3N0oRDjVt28k8Cxpl",
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "?success=true",
                CancelUrl = domain + "?canceled=true",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
