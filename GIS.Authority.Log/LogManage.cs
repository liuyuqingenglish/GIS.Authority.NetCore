/*
* ==============================================================================
*
* Filename: CalculationHelper
* ClrVersion: 4.0.30319.42000
* Description:日志管理对象
*
* Version: 1.0
* Created: 2020/3/25 16:49:48
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GIS.Authority.Log
{
    /// <summary>
    /// 日志管理对象
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// 分隔符
        /// </summary>
        private const string TAG_SPLIT = "\r\n";

        /// <summary>
        /// 日志编码的头
        /// </summary>
        private const string TAG_LOG_HEADER = "LogCode:";

        /// <summary>
        /// 日志操作对象
        /// </summary>
        private static ILogger Instance = null;

        /// <summary>
        /// 写入日志的间隔
        /// </summary>
        private static int WriteLogInterval = 3000;

        /// <summary>
        /// 日志缓存
        /// </summary>
        private static ConcurrentDictionary<string, ConcurrentStack<string>> Logs = new ConcurrentDictionary<string, ConcurrentStack<string>>();

        static LogHelper()
        {
            Initialization(ELogInterfaceType.Log4net, WriteLogInterval);

            #region 定期写入日志

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        KeyValuePair<string, ConcurrentStack<string>>[] tempLog = Logs.ToArray();
                        Logs.Clear();
                        if (tempLog != null && tempLog.Length > 0)
                        {
                            KeyValuePair<string, List<string>>[] writeLogs = GetLogs(tempLog);
                            if (!Instance.WriteLog(writeLogs))
                            {
                                //写入失败，就重新加入缓存
                                foreach (KeyValuePair<string, ConcurrentStack<string>> item in tempLog)
                                {
                                    if (Logs.ContainsKey(item.Key))
                                    {
                                        Logs[item.Key].PushRange(item.Value.Reverse().ToArray(), 0, item.Value.Count);
                                    }
                                    else
                                    {
                                        Logs.TryAdd(item.Key, item.Value);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        Thread.Sleep(WriteLogInterval);
                    }
                }
            });

            #endregion 定期写入日志
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="type">启用的日志类型</param>
        /// <param name="saveInterval">保存日志的时间间隔</param>
        public static void Initialization(ELogInterfaceType type, int saveInterval)
        {
            WriteLogInterval = saveInterval;
            switch (type)
            {
                case ELogInterfaceType.Log4net:
                    Instance = new Logger();
                    break;
            }
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="info">日志信息</param>
        /// <returns>返回日志Code</returns>
        public static string AddLog(ELogLevel type, string info)
        {
            return AddLogToBuffer(type, info);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="info">日志信息</param>
        /// <returns>返回日志Code</returns>
        public static string AddLog(ELogLevel type, params string[] info)
        {
            return AddLogToBuffer(type, info);
        }

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="ex">异常信息</param>
        /// <returns>返回日志Code</returns>
        public static string AddLog(ELogLevel type, Exception ex)
        {
            return AddLogToBuffer(ELogLevel.Warn, ex.Message, ex.StackTrace);
        }

        /// <summary>
        /// 添加日志到缓存
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="ex">异常信息</param>
        /// <param name="info">附加信息</param>
        /// <returns>返回日志Code</returns>
        public static string AddLog(ELogLevel type, Exception ex, string info)
        {
            return AddLogToBuffer(ELogLevel.Warn, info, ex.Message, ex.StackTrace);
        }

        /// <summary>
        /// 添加日志到缓存
        /// </summary>
        /// <param name="type">日志类型</param>
        /// <param name="message">日志内容</param>
        /// <returns>返回日志Code</returns>
        private static string AddLogToBuffer(ELogLevel type, params string[] message)
        {
            if (message == null || message.Length == 0)
            {
                return string.Empty;
            }

            try
            {
                long tick = DateTime.Now.Ticks;
                Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                int code = new Random().Next(100000, 999999);
                string codeStr = string.Format("{0}{1}", TAG_LOG_HEADER, code);

                StringBuilder sb = new StringBuilder();
                sb.Append(codeStr);
                sb.Append(TAG_SPLIT);

                List<string> parmsStr = new List<string>();
                parmsStr.AddRange(message);

                //记录堆栈信息
                parmsStr.AddRange(GetMethodName());

                for (int i = 0; i < parmsStr.Count; i++)
                {
                    if (i == parmsStr.Count - 1)
                    {
                        sb.Append(parmsStr[i]);
                    }
                    else
                    {
                        sb.AppendFormat("{0}", parmsStr[i]);
                    }
                }

                sb.Append(TAG_SPLIT);
                sb.Append(TAG_SPLIT);

                string log = sb.ToString();
                string key = ((int)type).ToString();
                ConcurrentStack<string> oldInfo = null;
                if (Logs.TryGetValue(key, out oldInfo))
                {
                    oldInfo.Push(log);
                }
                else
                {
                    oldInfo = new ConcurrentStack<string>();
                    oldInfo.Push(log);
                    Logs.TryAdd(key, oldInfo);
                }

                return codeStr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "----" + ex.StackTrace);
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取调用堆栈的所有方法名称--这里非常耗时,先注释掉
        /// </summary>
        /// <returns>返回堆栈方法</returns>
        private static List<string> GetMethodName()
        {
            List<string> result = new List<string>();

            //System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            //System.Diagnostics.StackFrame[] sfs = st.GetFrames();
            //foreach (StackFrame sf in sfs)
            //{
            //    //获取堆栈信息中的方法名
            //    string name = sf.GetMethod().Name;
            //    result.Add(name);
            //}
            return result;
        }

        /// <summary>
        /// 获取日志内容
        /// </summary>
        /// <param name="logInfos">日志数据</param>
        /// <returns>返回待写入日志</returns>
        private static KeyValuePair<string, List<string>>[] GetLogs(KeyValuePair<string, ConcurrentStack<string>>[] logInfos)
        {
            Dictionary<string, List<string>> logs = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, ConcurrentStack<string>> item in logInfos)
            {
                if (logs.ContainsKey(item.Key))
                {
                    logs[item.Key].AddRange(item.Value.ToArray());
                }
                else
                {
                    List<string> tempLogs = new List<string>();
                    tempLogs.AddRange(item.Value.ToArray());
                    logs.Add(item.Key, tempLogs);
                }
            }

            return logs.ToArray();
        }
    }
}