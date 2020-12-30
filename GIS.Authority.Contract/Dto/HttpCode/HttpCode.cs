/*
* ==============================================================================
*
* Filename: HttpCode
* ClrVersion: 4.0.30319.42000
* Description: http代码
*
* Version: 1.0
* Created: 2019/7/13 14:35:44
* Compiler: Visual Studio 2017
*
* Author: lifu
* Copyright: lyq
*
* ==============================================================================
*/

using System;

namespace GIS.Authority.Contract
{
    /// <summary>
    /// http状态
    /// </summary>
    public enum HttpCode
    {
        /// <summary>
        /// 正常
        /// </summary>
        TAG_SUCCESSFUL = 200,

        /// <summary>
        /// 请求错误
        /// </summary>
        TAG_REQUEST_ERROR = 400,

        /// <summary>
        /// 未授权
        /// </summary>
        TAG_UNAUTHORIZED = 401,

        /// <summary>
        /// 未注册
        /// </summary>
        TAG_UNREGISTER = 402,

        /// <summary>
        /// 没有包含token信息
        /// </summary>
        TAG_NO_TOKEN = 403,

        /// <summary>
        /// 在其他设备上登录
        /// </summary>
        TAG_LOGIN_ON_OTHER_DEVICE = 409,

        /// <summary>
        /// 设备未注册
        /// </summary>
        TAG_NO_DEVICE = 408,

        /// <summary>
        /// 系统错误
        /// </summary>
        TAG_SYSTEM_ERROR = 500,

        /// <summary>
        /// 服务超时
        /// </summary>
        TAG_TIME_OUT = 600,

        /// <summary>
        /// 未知异常
        /// </summary>
        TAG_UNKNOWN_ERROR = 700,
    }

    /// <summary>
    /// http常量
    /// </summary>
    public class HttpConst
    {
        /// <summary>
        /// 校验失败错误信息的标示符
        /// </summary>
        public const string TAG_CHECK_ERROR = "CheckFailed";

        /// <summary>
        /// 校验失败编码的标示符
        /// </summary>
        public const string TAG_CHECK_ERROR_CODE = "CheckFailedCode";

        /// <summary>
        /// 校验成功，保存用户对象的键值
        /// </summary>
        public const string TAG_USER_DTO = "CheckSucessUserDto";

        /// <summary>
        /// 校验成功，保存用户的token值
        /// </summary>
        public const string TAG_USER_TOKEN = "CheckSucessUserToken";

        /// <summary>
        /// 授权的token字符串-HiCloudToken
        /// </summary>
        public const string TAG_TOKEN_FLAG = "HiCloudToken";
    }
}
