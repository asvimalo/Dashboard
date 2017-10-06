using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dashboard.Web.Services;
using Dashboard.Web.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;


namespace Dashboard.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            // Add framework services.
            services.AddMvc();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //Auth: adds the authentication services to DI
            services.AddAuthentication(options =>
            {
                //1 using a cookie as the primary means to authenticate a user via "Cookies"
                options.DefaultScheme = "Cookies";
                //2 when we need the user to login, we will be using the OpenID Connect scheme.
                options.DefaultChallengeScheme = "oidc";
            })
                //3 add the handler that can process cookies
                .AddCookie("Cookies")
                //4 configure the handler that perform the OpenID Connect protocol
                .AddOpenIdConnect("oidc", options =>
                {
                    //5 is used to issue a cookie using the cookie handler 
                    // once the OpenID Connect protocol is complete
                    options.SignInScheme = "Cookies";

                    //6 indicates that we are trusting IdentityServer
                    options.Authority = "https://localhost:44394/";

                    // ssl - https
                    options.RequireHttpsMetadata = true;

                    //7  identity this client via ClientId
                    options.ClientId = "dashboard";

                    options.ClientSecret = "secret";
                    options.ResponseType = "code id_token";

                    //8 Used to persist the tokens from IdentityServer in the cookie
                    // The OpenID Connect middleware saves the tokens (identity, access and refresh in our case) automatically for you. 
                    // That’s what the SaveTokens setting does.
                    options.SaveTokens = true;

                    options.GetClaimsFromUserInfoEndpoint = true;

                    //options.GetClaimsFromUserInfoEndpoint = true;
                    options.Scope.Add("offline_access");
                    //options.Scope.Add("openId");
                    //options.Scope.Add("profile");
                });
            // HttpContext injected through services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // register  IHttpClientDashboard
            services.AddScoped<IHttpClientDashboard, HttpClientDashboard>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
            }


            // ensure the authentication services execute on each request
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
