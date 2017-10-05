using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dashboard.IDP.Configuration;

namespace Dashboard.IDP
{
    public class Startup
    {       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(InMemoryConfiguration.Users().ToList())
                .AddInMemoryClients(InMemoryConfiguration.Clients())
                .AddInMemoryApiResources(InMemoryConfiguration.ApiResources());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseDeveloperExceptionPage();

            app.UseIdentityServer();
        }
    }
}
