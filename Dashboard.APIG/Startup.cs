using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dashboard.DataG.Db;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Dashboard.DataG.Contracts;
using Dashboard.DataG.Repository;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Dashboard.APIG
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
            services.AddDbContext<DashboardGenericContext>(option => {
                option.UseSqlServer(Configuration["ConnectionStrings:DashboardlocalDbContextConnection"]);
                option.ConfigureWarnings(warnings => warnings.Throw(CoreEventId.IncludeIgnoredWarning));

            });
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DashboardGenericContext>();

            services.AddScoped(typeof(IRepoCommitment), typeof(RepoCommitment));
            services.AddScoped(typeof(IRepoAssignment), typeof(RepoAssignment));
            services.AddScoped(typeof(IRepoEmployee), typeof(RepoEmployee));
            services.AddScoped(typeof(IRepoKnowledge), typeof(RepoKnowledge));
            services.AddScoped(typeof(IRepoLocation), typeof(RepoLocation));
            services.AddScoped(typeof(IRepoPhase), typeof(RepoPhase));
            services.AddScoped(typeof(IRepoTask), typeof(RepoTask));
            services.AddScoped(typeof(IRepoClient), typeof(RepoClient));
            services.AddScoped(typeof(IRepoAcquiredKnowledge), typeof(RepoAcquiredKnowledge));
            services.AddScoped(typeof(IRepoProject), typeof(RepoProject));
            services.AddScoped(typeof(IRepoJobTitle), typeof(RepoJobTitle));
            services.AddScoped(typeof(IRepoJobTitleAssignment), typeof(RepoJobTitleAssignment));

            services.AddScoped(typeof(IDesignTimeDbContextFactory<DashboardGenericContext>), typeof(TempCtxDashboardGeneric));

            services.AddCors(options =>
            {
                options.AddPolicy("access", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // It was already camelcasing before this config
                    config.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            
                   
                });

            services.AddLogging();
            services.AddSwaggerGen(c =>
            {
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (apiDesc.HttpMethod == null) return false;
                    return true;
                });
            });
            services.AddSwaggerGen(options =>
            {
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (apiDesc.HttpMethod == null) return false;
                    return true;
                });
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SIGMA DASHBOARD API",
                    Description = "SIGMA DASHBOARD API Swagger Documentation",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "SIGMAITC", Url = "http://www.sigmaitc.se/sv/" },
                    License = new License { Name = "MIT", Url = "https://en.wikipedia.org/wiki/MIT_License" }
                });
               

                //Add XML comment document by uncommenting the following
                // var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "MyApi.xml");
                // options.IncludeXmlComments(filePath);

            });
        }

        private Action<MvcOptions> AddJsonOptions(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
                loggerFactory.AddDebug(LogLevel.Error);

            app.UseCors("access");
            
            if (Configuration["DesignTime"] != "true")
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    
                }
            }
            app.UseSwagger();
            
            // Visit http://localhost:8890/swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DASHBOARD API V1");
            });
            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");

                    
                }

                );

        }
    }
}
