/*
* ==============================================================================
*
* Filename: DateTimeHelper
* ClrVersion: 4.0.30319.42000
* Description:时间辅助对象
*
* Version: 1.0
* Created: 2020/4/20 18:13:45
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using System;

namespace GIS.Authority.Common
{
    /// <summary>
    /// 时间辅助对象
    /// </summary>
    public class DateTimeHelper
    {
        /// <summary>
        /// 转换时间为时间戳
        /// </summary>
        /// <param name="date">需要传递UTC时间,避免时区误差,例:DataTime.UTCNow</param>
        /// <returns>时间戳字符串</returns>
        public static string DateTimeToTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds).ToString();
        }

        /// <summary>
        /// 转换时间戳为时间
        /// </summary>
        /// <param name="timeStamp">时间戳,DataTime.UTC</param>
        /// <returns>时间戳字符串</returns>
        public static DateTime TimestampToDateTime(string timeStamp)
        {
            DateTime dateTimeStart = new DateTime(1970, 1, 1);
            long ltime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(ltime);
            return dateTimeStart.Add(toNow);
        }
    }
}