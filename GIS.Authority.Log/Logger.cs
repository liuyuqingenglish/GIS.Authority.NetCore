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

using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace GIS.Authority.Log
{
    /// <summary>
    /// 日志操作对象
    /// </summary>
    internal class Logger : ILogger
    {
        /// <summary>
        /// 日志接口
        /// </summary>
        private static ILog LoggerOperator;

        /// <summary>
        /// 调试日志接口实现
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Debug(string message)
        {
            ILog log = GetLogManager();
            Debug(message, log);
        }

        /// <summary>
        /// 错误日志接口实现
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Error(string message)
        {
            ILog log = GetLogManager();
            Error(message, log);
        }

        /// <summary>
        /// 致命日志接口实现
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Fatal(string message)
        {
            ILog log = GetLogManager();
            Fatal(message, log);
        }

        /// <summary>
        /// 一般日志接口实现
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Info(string message)
        {
            ILog log = GetLogManager();
            Info(message, log);
        }

        /// <summary>
        /// 警告日志接口实现
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Warn(string message)
        {
            ILog log = GetLogManager();
            Warn(message, log);
        }

        /// <summary>
        /// 批量写入日志
        /// </summary>
        /// <param name="logs">日志消息</param>
        /// <returns>返回是否成功</returns>
        public bool WriteLog(KeyValuePair<string, List<string>>[] logs)
        {
            if (logs == null || logs.Length <= 0)
            {
                return false;
            }

            ILog log = GetLogManager();
            foreach (KeyValuePair<string, List<string>> item in logs)
            {
                if (!WriteLogs(item.Value, (ELogLevel)int.Parse(item.Key), log))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 调试日志接口实现
        /// </summary>
        /// <param name="message">日志</param>
        /// <param name="type">类型</param>
        /// <param name="log">日志接口</param>
        private bool WriteLogs(List<string> message, ELogLevel type, ILog log)
        {
            if (message == null || message.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < message.Count; i++)
            {
                if (!WriteLog(message[i], type, log))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 调试日志接口实现
        /// </summary>
        /// <param name="message">日志</param>
        /// <param name="type">类型</param>
        /// <param name="log">日志接口</param>
        private bool WriteLog(string message, ELogLevel type, ILog log)
        {
            switch (type)
            {
                case ELogLevel.Debug:
                    Debug(message, log);
                    break;

                case ELogLevel.Info:
                    Info(message, log);
                    break;

                case ELogLevel.Warn:
                    Warn(message, log);
                    break;

                case ELogLevel.Error:
                    Error(message, log);
                    break;

                case ELogLevel.Fatal:
                    Fatal(message, log);
                    break;
            }

            return true;
        }

        /// <summary>
        /// 调试日志接口实现
        /// </summary>
        /// <param name="message">日志</param>
        /// <param name="log">日志接口</param>
        private void Debug(string message, ILog log)
        {
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }

        /// <summary>
        /// 错误日志接口实现
        /// </summary>
        /// <param name="message">日志</param>
        /// <param name="log">日志接口</param>
        private void Error(string message, ILog log)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        /// <summary>
        /// 致命日志接口实现
        /// </summary>
        /// <param name="message">日志</param>
        /// <param name="log">日志接口</param>
        private void Fatal(string message, ILog log)
        {
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }

        /// <summary>
        /// 一般日志接口实现
        /// </summary>
        /// <param name="message">日志</param>
        /// <param name="log">日志接口</param>
        private void Info(string message, ILog log)
        {
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }

        /// <summary>
        /// 警告日志接口实现
        /// </summary>
        /// <param name="message">日志</param>
        /// <param name="log">日志接口</param>
        private void Warn(string message, ILog log)
        {
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }

        /// <summary>
        /// 获取日志接口
        /// </summary>
        /// <returns>返回日志接口</returns>
        private ILog GetLogManager()
        {
            if (LoggerOperator == null)
            {
                ILoggerRepository repository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(repository, new FileInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "log4net.config")));

                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    var tempRepo = repository as Hierarchy;
                    if (tempRepo != null)
                    {
                        var appenders = tempRepo.GetAppenders();
                        if (appenders != null)
                        {
                            foreach (var appender in appenders)
                            {
                                if (appender is FileAppender)
                                {
                                    var fileLogAppender = appender as FileAppender;
                                    fileLogAppender.File = fileLogAppender.File.Replace(@"\", Path.DirectorySeparatorChar.ToString());
                                    fileLogAppender.ActivateOptions();
                                }
                            }
                        }
                    }
                }

                LoggerOperator = LogManager.GetLogger(repository.Name, "PushService");
            }

            return LoggerOperator;
        }
    }
}