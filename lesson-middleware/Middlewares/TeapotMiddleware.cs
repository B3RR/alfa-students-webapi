using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace lesson_middleware.Middlewares
{
    public class TeapotMiddleware
    {
        private readonly RequestDelegate _next;

        public TeapotMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {

            await _next(context);
            if (context.Response.StatusCode == 418)
            {
                var nl = Environment.NewLine;
                await context.Response.WriteAsync("REQUEST - " + String.Join(nl + "REQUEST - ", context.Request.Headers.Select(x => $"{x.Key}: {x.Value}")));
                await context.Response.WriteAsync(nl + "RESPONSE - " + String.Join(nl + "RESPONSE - ", context.Response.Headers.Select(x => $"{x.Key}: {x.Value}")));
            }


        }
    }
}