using Fakturownia.Functions.Models;
using Fakturownia.Functions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;

[assembly: FunctionsStartup(typeof(Fakturownia.Startup))]

namespace Fakturownia
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: false, reloadOnChange: true)
                .Build();

            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IInvoiceService, InvoiceService>();
            builder.Services.AddTransient<IOrderService, OrderService>();

            var servicesSettings = configuration.GetSection("Services").Get<Dictionary<string, ServicesSettings>>();
            builder.Services.AddSingleton(servicesSettings);

            var accountSettings = configuration.GetSection("Account").Get<AccountSettings>();
            builder.Services.AddSingleton(accountSettings);

            var sellerSettings = configuration.GetSection("Seller").Get<SellerSettings>();
            builder.Services.AddSingleton(sellerSettings);

            var invoiceServiceSettings = configuration.GetSection("InvoiceService").Get<InvoiceServiceSettings>();
            builder.Services.AddSingleton(invoiceServiceSettings);
        }
    }
}