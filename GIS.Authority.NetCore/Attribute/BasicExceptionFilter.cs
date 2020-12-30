using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net.Http;
using GIS.Authority.Common;
using GIS.Authority.Entity.Base;
using GIS.Authority.Log;

namespace GIS.Authority.NetCore
{
    public class BasicExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var exceptionMsg = string.Empty;
            if (!(exception is HttpRequestException || exception is UnauthorizedAccessException))
            {
                exceptionMsg = exception.Message;
                var url = context.HttpContext.Request.GetEncodedUrl();
                var errorCodeId = RandomHelper.GetRandomId(7);
                exceptionMsg = $"事件id：{errorCodeId} 错误:{exceptionMsg}";
                LogHelper.AddLog(ELogLevel.Debug, exceptionMsg);
            }
            var result = new ServiceResult<object>(exception);
            context.Result = new ObjectResult(result);
        }
    }
}