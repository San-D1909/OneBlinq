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
using Backend.Infrastructure.Data.Repositories;
using Backend.Models;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Infrastructure.Data;
using Backend.Core.Logic.EmailClients;

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
        private LicenseGenerator _licenseGenerator;
        private ILicenceRepository _licenceRepository;
        private IUserRepository _userRepository;
        private readonly IPluginBundleVariantRepository _pluginBundleVariantRepository;
        private readonly IPluginVariantRepository _pluginVariantRepository;
        private readonly IPluginLicenseRepository _pluginLicenseRepository;
        private readonly IPluginBundlesRepository _pluginBundlesRepository;
        private ApplicationDbContext _context;
        private ResetPasswordHelper _resetPasswordHelper;

        public CheckoutApiController(IConfiguration config, MailClient mailClient, LicenseGenerator licenseGenerator, ILicenceRepository licenseRepo, IUserRepository userRepo, IPluginBundleVariantRepository pluginBundleVariantRepository, IPluginVariantRepository pluginVariantRepository, IPluginLicenseRepository pluginLicenseRepository, IPluginBundlesRepository pluginBundlesRepository, ApplicationDbContext context)
        {
            _config = config;
            _mail = mailClient;
            _licenseGenerator = licenseGenerator;
            _licenceRepository = licenseRepo;
            _pluginBundleVariantRepository = pluginBundleVariantRepository;
            _pluginVariantRepository = pluginVariantRepository;
            _pluginLicenseRepository = pluginLicenseRepository;
            _pluginBundlesRepository = pluginBundlesRepository;
            _userRepository = userRepo;
            _context = context;
            _resetPasswordHelper = new ResetPasswordHelper(config, mailClient);
        }

        [HttpPost("create-checkout-session")]
        public ActionResult Create()
        {
            var isSubscription = Request.Form["isSubscription"];
            var priceId = Request.Form["priceId"];
            var variantId = Request.Form["variantId"];
            var description = Request.Form["description"];
            var maxActivations = Request.Form["maxActivations"];

            var mode = isSubscription == "true" ? "subscription" : "payment";

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
                Mode = mode,
                SuccessUrl = domain + "/order?success=true&session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "?canceled=true",
                AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
            };

            if (mode == "subscription")
            {
                options.SubscriptionData = new SessionSubscriptionDataOptions
                {
                    Metadata = new Dictionary<string, string>
                        {
                            { "maxActivations", maxActivations },
                            { "description", description },
                            { "variantId", variantId }
                        },
                };
            }
            else
            {
                options.PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    Metadata = new Dictionary<string, string>
                    {
                        { "maxActivations", maxActivations },
                        { "description", description },
                        { "variantId", variantId }
                    }, 
                };
            }


            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _config["STRIPE_WEBHOOK_SECRET"]
            );
            
            Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");

            if(stripeEvent.Type == Events.SubscriptionScheduleCanceled)
            {
                var session = stripeEvent.Data.Object as Session;
                var paymentIntentId = session.PaymentIntentId;

                LicenseModel license = await _licenceRepository.GetLicenseByIntent(paymentIntentId);
                license.IsActive = false;
                _licenceRepository.Update(license);
                await _licenceRepository.SaveAsync();
            }

            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var session = stripeEvent.Data.Object as PaymentIntent;

                var service = new CustomerService();
                var customer = service.Get(session.CustomerId);

                Console.WriteLine($"Session ID: {session.Id}");
                // Take some action based on session.
                var email = customer.Email;

                var key = _licenseGenerator.CreateLicenseKey(email, session.Metadata["description"], session.Metadata["variantId"]);
                // TODO: plugin + variant ids

                var emailCheck = await _userRepository.GetUserByEmail(email);

                if (emailCheck == null)
                {
                    UserModel user = new UserModel
                    {
                        Company = null,
                        Email = email,
                        FullName = customer.Name,
                        IsAdmin = false,
                        IsVerified = true,
                        Password = "",
                        Salt = new byte[0]
                    };

                    _userRepository.Add(user);
                    await _userRepository.SaveAsync();

                    _resetPasswordHelper.SendResetLink(email);
                }

                var license = new LicenseModel
                {
                    IsActive = true,
                    LicenseKey = key,
                    UserId = _userRepository.GetUserByEmail(email).Result.Id,
                    TimesActivated = 0,
                    VariantId = Convert.ToInt32(session.Metadata["variantId"]),
                    ExpirationTime = DateTime.UtcNow.AddYears(1),
                    PaymentIntentId = session.Id
                };

                _licenceRepository.Add(license);
                await _licenceRepository.SaveAsync();


                var plugin = await this._pluginLicenseRepository.GetPluginByStripePriceId(session.Metadata["variantId"]);
                var pluginBundle = await this._pluginLicenseRepository.GetPluginBundleByStripePriceId(session.Metadata["variantId"]);

                if (plugin != null)
                {
                    this._pluginLicenseRepository.Add(new PluginLicenseModel()
                    {
                        License = license,
                        LicenseId = license.Id,
                        Plugin = plugin,
                        PluginId = plugin.Id,
                        TimesActivated = 0
                    });
                    await this._pluginLicenseRepository.SaveAsync();
                }
                else if(pluginBundle != null)
                {
                    var plugins = await this._pluginBundlesRepository.GetPluginsFromBundle(pluginBundle.Id);
                    foreach (PluginModel p in plugins)
                    {
                        this._pluginLicenseRepository.Add(new PluginLicenseModel()
                        {
                            License = license,
                            LicenseId = license.Id,
                            Plugin = p,
                            PluginId = p.Id,
                            TimesActivated = 0,
                            PluginBundle = pluginBundle,
                            PluginBundleId = pluginBundle.Id
                        });
                    }
                    await this._pluginLicenseRepository.SaveAsync();
                }

                await _mail.PurchaseConfirmationMail("oneblinq@stuur.men", email, "Purchase confirmation", key);

            }

            return Ok();
        }
    }
}
