using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Reflection;
using Another_URL_Shortener.Attributes;
using Another_URL_Shortener.Configuration;
using Another_URL_Shortener.Models;
using Another_URL_Shortener.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Another_URL_Shortener
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Anothe URL Shortener", Version = "v1" });
            });
            //services.AddDbContext<ApplicationDbContext>(options => 
            //    // options.UseInMemoryDatabase("ShortURL")
            //    //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
            //));
            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(Configuration.GetConnectionString("DockerAppLocalSql")));

            services.Configure<CustomConfigs>(Configuration.GetSection("CustomConfigs"));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IServiceHandler<>), typeof(ServiceHandler<>));

            ServiceLocator.LoadSelfRegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Another URL Shortener v1"));
            }

#if DEBUG
            app.UseDeveloperExceptionPage();
#endif

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    // Reference: https://www.codeproject.com/Tips/5311615/Completely-Selfconfigurating-Service-with-NET-5
    public static class ServiceLocator
    {
        public static void LoadSelfRegisterServices(IServiceCollection services)
        {
            foreach (var type in AppDomain.CurrentDomain.GetAssemblies()
                         .SelectMany(f=>f.GetTypes())
                         .Where(e=>e.GetCustomAttribute<ServiceAttribute>(false) != null))
            {
                var serviceAttribute = type.GetCustomAttribute<ServiceAttribute>(false);
                var actorTypes = serviceAttribute.RegisterAs;

                if (serviceAttribute is SelfRegisterServiceAttribute)
                {
                    services.AddScoped(type);
                    foreach (var actorType in actorTypes)
                    {
                        services.AddScoped(actorType, (sCol) => sCol.GetService(type));
                    }
                }
                //else
                //{
                //    services.AddSingleton(type);
                //    foreach (var actorType in actorTypes)
                //    {
                //        services.AddSingleton(actorType, (sCol) => sCol.GetService(type));
                //    }
                //}
            }
        }
    }
}
