using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using trailers_api;

namespace trailers_api.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "ApiKey";
        public async Task OnActionExecutionAsync (ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue
                (APIKEYNAME, out var extractedApiKey))
            {
                var result = new { StatusCode = 401, Value = new { message = "Api Key was not provided" } };
                context.Result = new ContentResult()
                {
                    StatusCode = result.StatusCode,
                    ContentType = "application/json",
                    Content = JsonConvert.SerializeObject(result)
                };

                return;
            }

            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var apiKey = appSettings.GetValue<string>(APIKEYNAME);

            if (!apiKey.Equals(extractedApiKey))
            {
                var result = new { StatusCode = 401, Value = new { message = "Unauthorized Client" } };
                context.Result = new ContentResult()
                {
                    StatusCode = result.StatusCode,
                    ContentType = "application/json",
                    Content = JsonConvert.SerializeObject(result)
                };

                return;
            }

            await next();
        }
    }
}