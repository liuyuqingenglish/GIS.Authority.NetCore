/*
* ==============================================================================
*
* Filename: Infrastructure
* ClrVersion: 4.0.30319.42000
* Description: 服务层响应实体
*
* Version: 1.0
* Created: 2019/7/26 17:26:06
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;

namespace GIS.Authority.Common
{
    /// <summary>
    /// 服务层响应实体
    /// </summary>
    /// <typeparam name="TResult">封装结果</typeparam>
    public class ServiceResult<TResult>
    {
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public ServiceResult()
        {
            Code = HttpStatusCode.OK;
            Success = true;
            Message = "请求成功";
        }

        /// <summary>
        /// 设置返回值
        /// </summary>
        /// <param name="result">返回结果</param>
        public ServiceResult(TResult result)
        {
            Code = HttpStatusCode.OK;
            Success = true;
            Message = "请求成功";
            Result = result;
        }

        /// <summary>
        /// 系统错误
        /// </summary>
        /// <param name="exception">错误</param>
        public ServiceResult(Exception exception)
        {
            Success = false;
            Message = exception.Message;
            switch (exception)
            {
                case UnauthorizedAccessException _:
                    Code = HttpStatusCode.Unauthorized;
                    break;

                case AuthenticationException _:
                    Code = HttpStatusCode.Conflict;
                    break;

                case HttpRequestException _:
                    Code = HttpStatusCode.BadRequest;
                    break;

                default:
                    Code = HttpStatusCode.InternalServerError;
                    break;
            }
        }

        /// <summary>
        /// HTTP状态码
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public string Other { get; set; }
    }
}