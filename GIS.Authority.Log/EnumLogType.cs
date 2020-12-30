/*
* ==============================================================================
*
* Filename: EnumLogType
* ClrVersion: 4.0.30319.42000
* Description: 日志类型
*
* Version: 1.0
* Created: 2019/7/9 13:31:55
* Compiler: Visual Studio 2017
*
* Author: lifu
* Copyright: lyq
*
* ==============================================================================
*/

namespace GIS.Authority.Log
{
    /// <summary>
    /// 日志级别
    /// </summary>
    public enum ELogLevel
    {
        /// <summary>
        /// 调试信息
        /// </summary>
        Debug = 1,

        /// <summary>
        /// 一般信息-登录、退出、操作日志等
        /// </summary>
        Info = 2,

        /// <summary>
        /// 警告
        /// </summary>
        Warn = 3,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 4,

        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal = 5,
    }

    /// <summary>
    /// 日志接口实现的方式
    /// </summary>
    public enum ELogInterfaceType
    {
        /// <summary>
        /// log4
        /// </summary>
        Log4net = 1,
    }
}