using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Dashboard.Data.EF.Db;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.Data
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DashboardContext>();
                    DashboardContextSeedData.SeedData(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder()
                                .UseKestrel()
                                .ConfigureAppConfiguration((ctx, cfg) =>
                                {
                                    

                                })
                                .ConfigureLogging((ctx, logging) => {
                                    logging.AddConsole();
                                })
                                .UseIISIntegration()
                                .UseStartup<Startup>()
                                .UseSetting("DesignTime", "true")
                                .UseDefaultServiceProvider((context, options) =>
                                 {
                                     options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                                 })
                                .Build();
        }
    }
}
