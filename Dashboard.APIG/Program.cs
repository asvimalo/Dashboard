﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Dashboard.DataG.Db;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;

namespace Dashboard.APIG
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
                    var context = services.GetRequiredService<DashboardGenericContext>();
                    DashboardContextSeedData.SeedData(context).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>

             WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSetting("DesignTime", "true")
                .UseDefaultServiceProvider((context, options) =>
                {
                     options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })
                .Build();
                                
                                
       
    }
}
