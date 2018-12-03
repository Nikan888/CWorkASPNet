using Test_Project_Hotel.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;
using Test_Project_Hotel.Models;

namespace Test_Project_Hotel.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next)
        {
            // инициализация базы данных
            _next = next;

        }
        public Task Invoke(HttpContext context, IServiceProvider serviceProvider, HotelContext dbContext)
        {
            if (!(context.Session.Keys.Contains("starting")))
            {
                
                DbInitializer.Initialize(dbContext);

                context.Session.SetString("starting", "Yes");
            }
            
            return _next.Invoke(context);
        }
    }
}
