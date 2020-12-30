using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace GIS.Authority.NetCore
{
    public class AuthorAttribute : IAuthorizationFilter
    {
        /// <summary>
        /// token字段
        /// </summary>
        public const string SSL_TOKEN = "sslToken";

        public AuthorAttribute()
        {
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetTokenByHeader(AuthorizationFilterContext context)
        {
            var headers = context.HttpContext.Request.Headers;
            if (headers == null || !headers.ContainsKey(SSL_TOKEN))
            {
                return string.Empty;
            }
            return headers.FirstOrDefault(x => x.Key.Equals(SSL_TOKEN)).Value;
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetTokenByQuery(AuthorizationFilterContext context)
        {
            var query = context.HttpContext.Request.Query;
            if (query == null || !query.ContainsKey(SSL_TOKEN))
            {
                return string.Empty;
            }
            return query.FirstOrDefault(x => x.Key.Equals(SSL_TOKEN)).Value;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = GetTokenByHeader(context);
            if (string.IsNullOrEmpty(token))
            {
                token = GetTokenByQuery(context);
            }
            if (string.IsNullOrEmpty(token))
            {
                throw new System.Exception("当前请求无权限，请重新登录");
            }
        }
    }
}