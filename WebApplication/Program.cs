using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var settings = config.Build();

                        if (!hostingContext.HostingEnvironment.IsDevelopment())
                        {
                            var keyVaultUrl = settings["KeyVaultUrl"];
                            config.AddAzureKeyVault(new Uri(keyVaultUrl), new DefaultAzureCredential());
                        }
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
