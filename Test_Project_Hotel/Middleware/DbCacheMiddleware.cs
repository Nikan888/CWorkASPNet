using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Project_Hotel.Services;
using Test_Project_Hotel.ViewModels;

namespace Test_Project_Hotel.Middleware
{
    public class DbCacheMiddleware
    {
        private readonly RequestDelegate requestDelegate;
        private readonly IMemoryCache memoryCache;
        private string cacheKey;

        public DbCacheMiddleware(RequestDelegate requestDelegate, IMemoryCache memoryCache,
            string cacheKey = "Hotel")
        {
            try
            {
                this.requestDelegate = requestDelegate;
                this.memoryCache = memoryCache;
                this.cacheKey = cacheKey;
            }
            catch (Exception ex)
            {

            }
        }

        public Task Invoke(HttpContext httpContext, HotelService service)
        {
            try
            {
                HomeViewModel model;
                if (!memoryCache.TryGetValue(cacheKey, out model))
                {
                    model = service.GetHomeViewModel();

                    memoryCache.Set(cacheKey, model,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            catch (Exception ex)
            {

            }
            return requestDelegate(httpContext);
        }
    }
}
