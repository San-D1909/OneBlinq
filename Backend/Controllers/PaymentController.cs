using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Backend.DTO.In;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System;
using Backend.Core.Logic;

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
        private MailClient _mail;
        public CheckoutApiController(IConfiguration config, MailClient mailClient)
        {
            _config = config;
            _mail = mailClient;
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

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    _config["STRIPE_WEBHOOK_SECRET"]
                );
                Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something failed {e}");
                return BadRequest();
            }

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                Console.WriteLine($"Session ID: {session.Id}");
                // Take some action based on session.
                var email = session.CustomerDetails.Email;
                await _mail.PurchaseConfirmationMail("oneblinq@stuur.men", email, "Purchase confirmation", "asdfkek");

            }

            return Ok();
        }
    }
}
