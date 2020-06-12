using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PayPalInCore.Models;
using PayPal.Api;
using PayPalInCore.PayPal;

namespace PayPalInCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Pay()
        {
            PayPalConfig payPalConfig = PayPalService.GetPayPalConfig();
            ViewBag.payPalConfig = payPalConfig;
            return View();
        }
        public IActionResult Success()
        {
            var result = PDTHolder.Success(Request.Query["tx"].ToString());

            return View();
        }

    }
}
