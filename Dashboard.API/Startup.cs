using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dashboard.Data.EF.Repository;
using Dashboard.Data.EF.IRepository;
using Dashboard.Data.EF.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace Dashboard.API
{

    public class Startup
    {
        public IConfigurationRoot _config { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();


            _config = builder.Build();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            // Add framework services.
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DashboardContext>();

            services.AddSingleton(_config);

            services.AddDbContext<DashboardContext>(options => 
                options.UseSqlServer(_config["ConnectionStrings:DashboardContextConnection"]));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<DashboardContextSeedData>();

            services.AddMvc()
                .AddJsonOptions(config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // It was already camelcasing before this config
                }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            DashboardContextSeedData seeder)
        {

            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        // ensure generic 500 status code on fault.
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            app.UseStaticFiles();

            //AutoMapper.Mapper.Initialize(cfg =>
            //{
                // Map from Image (entity) to Image, and back
                //cfg.CreateMap<Image, Model.Image>().ReverseMap();

                //// Map from ImageForCreation to Image
                //// Ignore properties that shouldn't be mapped
                //cfg.CreateMap<Model.ImageForCreation, Image>()
                //    .ForMember(m => m.FileName, options => options.Ignore())
                //    .ForMember(m => m.Id, options => options.Ignore())
                //    .ForMember(m => m.OwnerId, options => options.Ignore());

                //// Map from ImageForUpdate to Image
                //// ignore properties that shouldn't be mapped
                //cfg.CreateMap<Model.ImageForUpdate, Image>()
                //    .ForMember(m => m.FileName, options => options.Ignore())
                //    .ForMember(m => m.Id, options => options.Ignore())
                //    .ForMember(m => m.OwnerId, options => options.Ignore());
            //});

            //AutoMapper.Mapper.AssertConfigurationIsValid();

            // ensure DB migrations are applied

            //dashboardContext.Database.Migrate();

            // seed the DB with data
            

            app.UseMvc();
            //seeder.DashboardCtxSeedData();
        }
    }
}
