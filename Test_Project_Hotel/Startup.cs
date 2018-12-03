using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Test_Project_Hotel.Middleware;
using Test_Project_Hotel.Data;
using Test_Project_Hotel.Services;
using Microsoft.AspNetCore.Mvc;
using Test_Project_Hotel.Models;
using Microsoft.AspNetCore.Identity;

namespace Test_Project_Hotel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HotelContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<HotelContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<HotelService>();

            services.AddMvc(options =>
            {
                // определение профилей кэширования
                options.CacheProfiles.Add("Caching",
                    new CacheProfile()
                    {
                        Duration = 30
                    });
                options.CacheProfiles.Add("NoCaching",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
            });
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseSession();
            app.UseHotelCache("Hotel");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // добавляем компонент middleware по инициализации базы данных и производим инициализацию базы
            app.UseDbInitializer();
        }
    }
}
