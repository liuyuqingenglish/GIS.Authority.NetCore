/*
* ==============================================================================
*
* Filename: FormatHelper
* ClrVersion: 4.0.30319.42000
* Description: 格式化辅助对象
*
* Version: 1.0
* Created: 2020/3/25 16:33:58
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using System;

namespace GIS.Authority.Common.Helper
{
    /// <summary>
    /// 格式化辅助对象
    /// </summary>
    public class FormatHelper
    {
        /// <summary>
        /// 日期时间格式
        /// </summary>
        public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 日期时间格式
        /// </summary>
        public const string DATE_FORMAT = "yyyy-MM-dd";

        /// <summary>
        /// 时间格式化字符串
        /// </summary>
        public const string TIME_FORMAT = "yyyyMMddHHmmss";

        /// <summary>
        /// 日期时间格式
        /// </summary>
        public const string DATETIME_FORMAT2 = "yyyy/MM/dd HH:mm:ss";

        /// <summary>
        /// 日期时间格式
        /// </summary>
        public const string DATETIME_NO_BREAK_CHAR_FORMATDATETIME_FORMAT = "yyyyMMddHHmmss";

        /// <summary>
        /// 格式化DataTime
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>字符串</returns>
        public static string FormatDataTime(DateTime time)
        {
            return time.ToString(DATETIME_FORMAT);
        }

        /// <summary>
        /// 精简的日期时间格式
        /// </summary>
        public const string DateTimeShortFormat = "yyyyMMddHHmmss";

        /// <summary>
        /// 格式化DataTime
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>字符串</returns>
        public static string FormatShortDataTime(DateTime time)
        {
            return time.ToString(DateTimeShortFormat);
        }

        /// <summary>
        /// 字符串转化为Guid?类型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>Guid</returns>
        public static Guid? GetGuid(string str)
        {
            try
            {
                return new Guid(str.Trim());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转为DateTime?类型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>DateTime</returns>
        public static DateTime? GetDateTime(string str)
        {
            try
            {
                return DateTime.Parse(str.Trim());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转为bool?类型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>bool</returns>
        public static bool? GetBool(string str)
        {
            try
            {
                return bool.Parse(str.Trim());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转为bool?类型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>bool</returns>
        public static double? GetTimeSpan(string str)
        {
            try
            {
                return double.Parse(str.Trim());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转为int?类型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>int</returns>
        public static int? GetInt(string str)
        {
            try
            {
                return int.Parse(str.Trim());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转为double?类型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>double</returns>
        public static double? GetDouble(string str)
        {
            try
            {
                return double.Parse(str.Trim());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 字符串转为float?类型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>float</returns>
        public static float? GetFloat(string str)
        {
            try
            {
                return float.Parse(str.Trim());
            }
            catch
            {
                return null;
            }
        }
    }
}