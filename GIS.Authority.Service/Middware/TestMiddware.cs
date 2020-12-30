using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GIS.Authority.Service
{
    public class TestMiddware
    {
        private readonly RequestDelegate _next;

        public TestMiddware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync("sadf");
            await _next(context);
        }
    }
}