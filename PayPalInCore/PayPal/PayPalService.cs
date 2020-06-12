using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PayPalInCore.PayPal
{
    public class PayPalService
    {
        private readonly IConfiguration _conf;
        public static PayPalConfig GetPayPalConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            return new PayPalConfig()
            {
                AuthToken = configuration["PayPal:AuthToken"],
                Business = configuration["PayPal:Business"],
                PostUrl = configuration["PayPal:PostUrl"],
                ReturnUrl = configuration["PayPal:ReturnUrl"],
                IdentityToken = configuration["PayPal:IdentityToken"]
            };
        }
    }
}
