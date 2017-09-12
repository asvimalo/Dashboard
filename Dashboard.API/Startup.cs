using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Dashboard.Data.EF.IRepository;
using Microsoft.AspNetCore.Identity;
using Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Dashboard.Data.EF.Repository;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Dashboard.Data.EF.Db;
using System.IO;
using AutoMapper;
using Dashboard.Data.ViewModelsAPI;

namespace Dashboard.Data
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

            //services.AddIdentity<User, IdentityRole>()
            //    .AddEntityFrameworkStores<DashboardContext>()
            //    .AddDefaultTokenProviders();




            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DashboardContext>();
            // Add framework services.
            //services.AddDbContext<DashboardContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddAutoMapper();
            //services.AddTransient<DashboardContextSeedData>();
            services.AddMvc()
                .AddJsonOptions(config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // It was already camelcasing before this config
                });
            //services.Configure<IdentityOptions>(options =>
            //{
            //});

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
                config.CreateMap<CommitmentViewModel, Commitment>()
                    .ForMember(m => m.CommitmentId, options => options.Ignore())
                    .ForMember(m => m.ProjectId, options => options.Ignore())
                    .ForMember(m => m.UserId, options => options.Ignore()).ReverseMap();

                config.CreateMap<UserViewModel, User>().ReverseMap();     
                
                config.CreateMap<ProjectViewModel, Project>()
                    .ForMember(m => m.ProjectId, options => options.Ignore()).ReverseMap();

                config.CreateMap<PictureViewModel, Picture>()
                    .ForMember(m => m.PictureId, options => options.Ignore())
                    .ForMember(m => m.UserId, options => options.Ignore()).ReverseMap();

                config.CreateMap<PictureForCreation, Picture>()
                    .ForMember(m => m.FileName, options => options.Ignore())
                    .ForMember(m => m.PictureId, options => options.Ignore())
                    .ForMember(m => m.UserId, options => options.Ignore());

                config.CreateMap<PictureToUpdate, Picture>()
                    .ForMember(m => m.FileName, options => options.Ignore())
                    .ForMember(m => m.PictureId, options => options.Ignore())
                    .ForMember(m => m.UserId, options => options.Ignore());


            });
            Mapper.AssertConfigurationIsValid();
            if (_config["DesignTime"] != "true")
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    //var initializer = scope.ServiceProvider.GetRequiredService<DashboardContextSeedData>();
                    //initializer.SeedData().Wait();
                }
            }
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Dashboard}/{action=Index}/{id?}");
            });
            
        }
    }
}
