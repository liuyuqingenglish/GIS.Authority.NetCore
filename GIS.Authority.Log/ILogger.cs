/*
* ==============================================================================
*
* Filename: ILogger
* ClrVersion: 4.0.30319.42000
* Description: 日志接口
*
* Version: 1.0
* Created: 2019/7/9 13:50:39
* Compiler: Visual Studio 2017
*
* Author: lifu
* Copyright: lyq
*
* ==============================================================================
*/

using System.Collections.Generic;

namespace GIS.Authority.Log
{
    /// <summary>
    /// 日志接口
    /// </summary>
    internal interface ILogger
    {
        /// <summary>
        /// debug日志接口
        /// </summary>
        /// <param name="message">日志消息</param>
        void Debug(string message);

        /// <summary>
        /// error日志接口
        /// </summary>
        /// <param name="message">日志消息</param>
        void Error(string message);

        /// <summary>
        /// 致命日志接口
        /// </summary>
        /// <param name="message">日志消息</param>
        void Fatal(string message);

        /// <summary>
        /// 一般信息日志接口
        /// </summary>
        /// <param name="message">日志消息</param>
        void Info(string message);

        /// <summary>
        /// 警告日志接口
        /// </summary>
        /// <param name="message">日志消息</param>
        void Warn(string message);

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logs">日志消息</param>
        /// <returns>返回是否成功</returns>
        bool WriteLog(KeyValuePair<string, List<string>>[] logs);
    }
}