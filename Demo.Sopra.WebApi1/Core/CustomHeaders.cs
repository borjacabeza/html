using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;

namespace Demo.Sopra.WebApi1
{

    public class CustomHeaders
    {
        private readonly RequestDelegate _next;

        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("X-Application-Name", "Northwind API");
            httpContext.Response.Headers.Add("X-Application-Version", GetType().Assembly.GetName().Version.ToString());
            httpContext.Response.Headers.Add("X-Application-Copyright", "Empresa Uno, SL");
            httpContext.Response.Headers.Add("X-Server-Name", Environment.MachineName);
            httpContext.Response.Headers.Add("X-Server-OSVersion", Environment.OSVersion.ToString());

            return _next(httpContext);
        }

        public CustomHeaders(RequestDelegate next)
        {
            _next = next;
        }
    }
    
    public static class CustomHeadersExtensions
    {
        public static IApplicationBuilder UseCustomHeaders(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomHeaders>();
        }
    }
}


