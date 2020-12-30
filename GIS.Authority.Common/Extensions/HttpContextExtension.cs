using Microsoft.AspNetCore.Http;

namespace GIS.Authority.Common
{
    public class HttpContextExtension
    {
        public static IHttpContextAccessor httpContextAccessor;

        public static HttpContext httpContext => httpContextAccessor.HttpContext;
    }
}