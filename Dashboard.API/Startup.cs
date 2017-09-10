using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dashboard.API.EF.IRepository;
using Microsoft.AspNetCore.Identity;
using Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Dashboard.API.EF.Repository;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dashboard.API.EF.Db;
using System.IO;
using AutoMapper;
using Dashboard.Data.ViewModelsAPI;

namespace Dashboard.API
{
    public class Startup
    {
        public IConfigurationRoot _config { get; set; }

        public Startup(IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)                                     
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            _config = builder.Build();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton(_config);
            services.AddDbContext<DashboardContext>(option => {
                option.UseSqlServer(_config["ConnectionStrings:DashboardContextConnection"]);
            });
            //services.AddTransient<DashboardContextSeedData>();
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DashboardContext>()
                .AddDefaultTokenProviders();




            //services.AddEntityFrameworkSqlServer();
            //    .AddDbContext<DashboardContext>();
            // Add framework services.
            //services.AddDbContext<DashboardContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            services.AddMvc()
                .AddJsonOptions(config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // It was already camelcasing before this config
                });
            services.Configure<IdentityOptions>(options =>
            {
            });

            services.AddLogging();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
                loggerFactory.AddDebug(LogLevel.Error);

            Mapper.Initialize(config =>
            {
                config.CreateMap<CommitmentViewModel, Commitment>().ReverseMap();             
                config.CreateMap<UserViewModel, User>().ReverseMap();           
                config.CreateMap<ProjectViewModel, Project>().ReverseMap();
                config.CreateMap<PictureForCreation, Picture>()
                    .ForMember(m => m.FileName, options => options.Ignore())
                    .ForMember(m => m.Id, options => options.Ignore())
                    .ForMember(m => m.UserId, options => options.Ignore());
                config.CreateMap<PictureToUpdate, Picture>()
                    .ForMember(m => m.FileName, options => options.Ignore())
                    .ForMember(m => m.Id, options => options.Ignore())
                    .ForMember(m => m.UserId, options => options.Ignore());


            });
            Mapper.AssertConfigurationIsValid();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Dashboard}/{action=Index}/{id?}");
            });
            
        }
    }
}
