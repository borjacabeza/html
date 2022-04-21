using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Demo.Sopra.WebApi1
{

    public class APISecurity
    {
        private readonly RequestDelegate _next;

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                string apikey =  httpContext.Request.Headers["APIKey"];

                if(apikey == "UBsnxHho!PHt4bzvOm^%uMVw68qzSeVI") await _next(httpContext);
                else await httpContext.Response.WriteAsync(JsonConvert.SerializeObject( new { code = 999, message = "No Autorizado"}));
            }
            catch (System.Exception e)
            {
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject( new { code = 998, message = "No Autorizado"}));               
            }
        }

        public APISecurity(RequestDelegate next)
        {
            _next = next;
        }
    }
    
    public static class APISecurityExtensions
    {
        public static IApplicationBuilder UseAPISecurity(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APISecurity>();
        }
    }
}