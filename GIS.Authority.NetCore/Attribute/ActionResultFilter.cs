using GIS.Authority.Common;
using GIS.Authority.Common.Helpers;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace GIS.Authority.NetCore
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ActionResultFilter : Attribute, IActionFilter
    {
        public ActionResultFilter()
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            /// 筛选跳过action动作
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDescriptor.MethodInfo.GetCustomAttributes(typeof(SkipActionAttribute), true).Length > 0)
            {
                return;
            }
            ServiceResult<object> result = null;
       
            if (context.Exception == null)
            {
                if (context.HttpContext.Response.StatusCode != (int)HttpStatusCode.OK)
                {
                    result = new ServiceResult<object>
                    {
                        Success = false,
                        Code = (HttpStatusCode)context.HttpContext.Response.StatusCode,
                        Message = JsonHelper.SerializeObject(context.Result)
                    };
                }
                else
                {
                    if (context.Result is BadRequestObjectResult badRequest)
                    {
                        result = new ServiceResult<object>
                        {
                            Code = (HttpStatusCode)(badRequest.StatusCode ?? 500),
                            Message = JsonHelper.SerializeObject(badRequest.Value)
                        };
                    }
                    else if (context.Result is ObjectResult objectResult)
                    {
                        result = objectResult.Value == null ? new ServiceResult<object>() : new ServiceResult<object>(objectResult.Value);
                    }
                    else if (context.Result is EmptyResult)
                    {
                        result = new ServiceResult<object>();
                    }
                    else if (context.Result is ContentResult contentResult)
                    {
                        result = new ServiceResult<object>(contentResult.Content);
                    }
                    else if (context.Result is StatusCodeResult codeResult)
                    {
                        result = new ServiceResult<object> { Code = (HttpStatusCode)codeResult.StatusCode, Success = true };
                    }
                }
            }
            if (result != null)
            {
                context.Result = new ObjectResult(result);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="action"></param>
        private void RecordLog(ActionExecutedContext context)
        {
            Log.LogHelper.AddLog(Log.ELogLevel.Debug, $"远程ip：{context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress},请求地址：{context.HttpContext.Request.GetEncodedUrl()},请求方法:{context.ActionDescriptor.DisplayName}");

        }
    }
}