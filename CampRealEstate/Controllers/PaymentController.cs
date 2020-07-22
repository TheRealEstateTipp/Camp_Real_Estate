using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CampRealEstate.Controllers
{
    public class PaymentController : Controller
    {
        private int amount = 5000;

        public IActionResult Index()
        {
            ViewBag.PaymentAmount = amount;
            return View();
        }

        [HttpPost]
        public IActionResult Processing(string stripeToken, string stripeEmail)
        {
            Dictionary<string, string> MetaData = new Dictionary<string, string>();
            MetaData.Add("Product", "EnrollmentFee");
            MetaData.Add("Quantity", "1");
            var options = new ChargeCreateOptions
            {
                Amount = amount,
                Currency = "USD",
                Description = "One-Year Enrollment",
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
                Metadata = MetaData
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            return View();
        }


    }
}
