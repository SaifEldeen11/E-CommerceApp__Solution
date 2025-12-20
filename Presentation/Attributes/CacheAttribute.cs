using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int DurationInSeconds = 120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Create Cache Key
            string cacheKey = CreateCacheKey(context.HttpContext.Request);

            // Search for value with Cache key 
            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheValue = await cacheService.GetAsync(cacheKey);
            // Return Value if not null
            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK

                };
                return;
            }

            // if null 
            // 1. Invoke Next
            var ExcutedContext = await next.Invoke();
            // 2. Set Value(response) with Cache Key
            // Return Value
             if(ExcutedContext.Result is OkObjectResult result)
             {
                await cacheService.SetAsync(cacheKey, result, TimeSpan.FromSeconds(DurationInSeconds));
             }
        }
        private string CreateCacheKey(HttpRequest request)
        {
            // /api/Products
            // /api/Products?brandId=2&typeId=1
            // /api/Products?=typeId=1&brandId=2
            StringBuilder key = new StringBuilder();
            key.Append(request.Path);
            key.Append("?");
            foreach (var item in request.Query.OrderBy(Q => Q.Key))
            {
                key.Append($"{item.Key}={item.Value}&");
            }
            return key.ToString();
        }
    }
}
