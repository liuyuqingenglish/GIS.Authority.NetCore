using GIS.Authority.Common;
using GIS.Authority.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
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
        public ValueTuple<string, long> GetTokenByHeader(AuthorizationFilterContext context)
        {
            var tuple = new ValueTuple<string, long>();
            var headers = context.HttpContext.Request.Headers;
            if (headers == null || !headers.ContainsKey(SSL_TOKEN))
            {
                return tuple;
            }
            string token = headers.FirstOrDefault(x => x.Key.Equals(SSL_TOKEN)).Value;
            long ttl = RedisInstanceHelper.GetInstance().GetKeyTtl(token);
            tuple.Item1 = token;
            tuple.Item2 = ttl;
            return tuple;
        }

        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public ValueTuple<string, long> GetTokenByQuery(AuthorizationFilterContext context)
        {
            var tuple = new ValueTuple<string, long>();
            var query = context.HttpContext.Request.Query;
            if (query == null || !query.ContainsKey(SSL_TOKEN))
            {
                return tuple;
            }
            string token = query.FirstOrDefault(x => x.Key.Equals(SSL_TOKEN)).Value;
            long ttl = RedisInstanceHelper.GetInstance().GetKeyTtl(token);
            tuple.Item1 = token;
            tuple.Item2 = ttl;
            return tuple;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = GetTokenByHeader(context);
            if (string.IsNullOrEmpty(token.Item1))
            {
                token = GetTokenByQuery(context);
            }
            if (string.IsNullOrEmpty(token.Item1))
            {
                throw new System.Exception("当前请求无权限，请重新登录");
            }

            ///token即将过期 替换掉token
            if (token.Item2 <= 60)
            {
                string newToken = Guid.NewGuid().ToString();
                string value = RedisInstanceHelper.GetInstance().GetKeyString(token.Item1);
                UserAccountDto dto = JsonHelper.ToObject<UserAccountDto>(value);
                if (!string.IsNullOrEmpty(value))
                {
                    if (dto.IsRemain)
                    {
                        RedisInstanceHelper.GetInstance().defaulTimeHour += 3 * 24;
                    }
                    RedisInstanceHelper.GetInstance().RenameKey(token.Item1, newToken, (int)RedisInstanceHelper.GetInstance().defaulTimeHour * 3600);
                }
                context.Result = new ObjectResult(new ServiceResult<string>(newToken));
            }
        }
    }
}