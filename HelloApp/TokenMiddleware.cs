using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HelloApp
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        String pattern;

        public TokenMiddleware( RequestDelegate next, String pattern)
        {
            this._next = next;
            this.pattern = pattern;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (String.IsNullOrWhiteSpace(token) || token != pattern)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
