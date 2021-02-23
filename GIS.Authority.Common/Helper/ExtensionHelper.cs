/*
* ==============================================================================
*
* Filename: ExtensionHelper
* ClrVersion: 4.0.30319.42000
* Description: 拓展帮助类
*
* Version: 1.0
* Created: 2019/5/14 17:28:30
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace GIS.Authority.Common
{
    /// <summary>
    /// 可序列化类型接口
    /// </summary>
    public interface IXSerializable
    {
        /// <summary>
        /// 序列化自身
        /// </summary>
        /// <returns>序列化结果</returns>
        string Serialize();

        /// <summary>
        /// 反序列化自身
        /// </summary>
        /// <param name="serialStr">序列化字符串</param>
        /// <param name="refresh">刷新</param>
        /// <returns>this</returns>
        IXSerializable DeSerialize(string serialStr, bool refresh = false);
    }

    /// <summary>
    /// 拓展类
    /// </summary>
    public static class ExtensionHelper
    {
        /// <summary>
        /// 比较字符串是否一致，不区分大小写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="str1">对比字符串</param>
        /// <returns>是否一致</returns>
        public static bool EqualsIgnoreCase(this string str, string str1)
        {
            return string.Equals(str, str1, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 转换为价格的字符串格式
        /// </summary>
        /// <param name="money">decimal数值</param>
        /// <returns>字符串格式</returns>
        public static string ToMoneyString(this decimal money)
        {
            return money.ToString("F2");
        }

        /// <summary>
        /// 转换为日期字符串
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns>字符串</returns>
        public static string ToTimeString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换为整型的字符串格式
        /// </summary>
        /// <param name="money">decimal数值</param>
        /// <returns>字符串格式</returns>
        public static string ToIntString(this decimal money)
        {
            return money.ToString("D");
        }

        /// <summary>
        /// 转换为价格的字符串格式
        /// </summary>
        /// <param name="money">float数值</param>
        /// <returns>字符串格式</returns>
        public static string ToMoneyString(this float money)
        {
            return money.ToString("F2");
        }

        /// <summary>
        /// 转换为日期字符串
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns>字符串</returns>
        public static string ToDateString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 转换为日期字符串
        /// </summary>
        /// <param name="date">时间</param>
        /// <returns>字符串</returns>
        public static string ToDateStringShort(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 替换掉文件名中无效的字符
        /// </summary>
        /// <param name="name">文件名</param>
        /// <param name="replaceChar">替换的字符</param>
        /// <returns>合法的文件名</returns>
        public static string ToValidFileName(this string name, string replaceChar = null)
        {
            replaceChar = replaceChar ?? string.Empty;
            var builder = new StringBuilder(name);
            foreach (var invalidChar in Path.GetInvalidFileNameChars())
            {
                builder.Replace(invalidChar.ToString(), replaceChar);
            }

            return builder.ToString();
        }
    }

    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtension
    {
        #region 常用正则表达式

        /// <summary>
        /// Email正则
        /// </summary>
        private static readonly Regex EmailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);

        /// <summary>
        /// 电话号码正则
        /// </summary>
        private static readonly Regex MobileRegex = new Regex("^1[0-9]{10}$");

        /// <summary>
        /// 手机号正则
        /// </summary>
        private static readonly Regex PhoneRegex = new Regex(@"^(\d{3,4}-?)?\d{7,8}$");

        /// <summary>
        /// Ip地址正则
        /// </summary>
        private static readonly Regex IpRegex = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");

        /// <summary>
        /// 日期正则
        /// </summary>
        private static readonly Regex DateRegex = new Regex(@"(\d{4})-(\d{1,2})-(\d{1,2})");

        /// <summary>
        /// 数字正则
        /// </summary>
        private static readonly Regex NumericRegex = new Regex(@"^[-]?[0-9]+(\.[0-9]+)?$");

        /// <summary>
        /// 邮政编码正则
        /// </summary>
        private static readonly Regex ZipcodeRegex = new Regex(@"^\d{6}$");

        /// <summary>
        /// Id正则
        /// </summary>
        private static readonly Regex IdRegex = new Regex(@"^[1-9]\d{16}[\dXx]$");

        /// <summary>
        /// 账号正则，3到16位（字母，数字，下划线，减号）
        /// </summary>
        private static readonly Regex AccountRegex = new Regex(@"^[a-zA-Z0-9_-]{3,16}$");

        /// <summary>
        /// 密码强度正则，6-30位，包括数字、符号、字母任意两种以上混合的密码验证策略
        /// </summary>
        ////private static readonly Regex PasswordRegex = new Regex(@"^.*(?=.{6,})(?=.*\d)(?=.*[A-Z])(?=.*[a-z]).*$");
        private static readonly Regex PasswordRegex = new Regex(@"^(?![0-9]+$)(?![a-zA-Z]+$)(?!([^(0-9a-zA-Z)]|[\(\)])+$)([^(0-9a-zA-Z)]|[\(\)]|[a-zA-Z]|[0-9]){6,30}$");

        /// <summary>
        /// Url正则，例如http://www.baidu.com/
        /// </summary>
        private static readonly Regex UrlRegex = new Regex(@"(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?");

        /// <summary>
        /// 特殊字符正则
        /// </summary>
        private static readonly Regex SpecialRegex = new Regex(@"[`~!@#$^&*()=|{}':;',\\[\\].<>/?~！@#￥……&*（）——|{}【】‘；：”“'。，、？]");

        /// <summary>
        /// 是否包含中文
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否中文</returns>
        public static bool IsChinese(this string str)
        {
            return Regex.IsMatch(str, @"^.*[\u4e00-\u9fa5]{1,}.*$");
        }

        /// <summary>
        /// 是否为邮箱名
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否为邮箱名</returns>
        public static bool IsEmail(this string str)
        {
            return !string.IsNullOrEmpty(str) && EmailRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否为手机号
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否为手机号</returns>
        public static bool IsMobile(this string str)
        {
            return !string.IsNullOrEmpty(str) && MobileRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否为固话号
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否为固话号</returns>
        public static bool IsPhone(this string str)
        {
            return !string.IsNullOrEmpty(str) && PhoneRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否为IP
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否为IP</returns>
        public static bool IsIp(this string str)
        {
            return IpRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否是身份证号
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否是身份证号</returns>
        public static bool IsIdCard(this string str)
        {
            return !string.IsNullOrEmpty(str) && IdRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否为日期
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否为日期</returns>
        public static bool IsDate(this string str)
        {
            return DateRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否是数值(包括整数和小数)
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否是数值</returns>
        public static bool IsNumeric(this string str)
        {
            return NumericRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否为邮政编码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否为邮政编码</returns>
        public static bool IsZipCode(this string str)
        {
            return string.IsNullOrEmpty(str) || ZipcodeRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否是图片文件名
        /// </summary>
        /// <param name="fileName">字符串</param>
        /// <returns>是否是图片文件名</returns>
        public static bool IsImgFileName(this string fileName)
        {
            if (fileName.IndexOf(".", StringComparison.Ordinal) == -1)
            {
                return false;
            }

            var tempFileName = fileName.Trim().ToLower();
            var extension = tempFileName.Substring(tempFileName.LastIndexOf(".", StringComparison.Ordinal));
            return extension == ".png" || extension == ".bmp" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif";
        }

        /// <summary>
        /// 是否合法账号
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否合法</returns>
        public static bool IsAccount(this string str)
        {
            return !string.IsNullOrEmpty(str) && AccountRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否合法密码
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否合法</returns>
        public static bool IsPassword(this string str)
        {
            return !string.IsNullOrEmpty(str) && PasswordRegex.IsMatch(str) && !IsChinese(str);
        }

        /// <summary>
        /// 是否url
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否合法</returns>
        public static bool IsUrl(this string str)
        {
            return !string.IsNullOrEmpty(str) && UrlRegex.IsMatch(str);
        }

        /// <summary>
        /// 是否包含特殊字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否包含</returns>
        public static bool IsSpecial(this string str)
        {
            return !string.IsNullOrEmpty(str) && SpecialRegex.IsMatch(str);
        }

        #endregion 常用正则表达式

        #region 空判断

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="inputStr">字符串</param>
        /// <returns>是否为空</returns>
        public static bool IsNullOrEmpty(this string inputStr)
        {
            return string.IsNullOrEmpty(inputStr);
        }

        /// <summary>
        /// 是否为空或空格
        /// </summary>
        /// <param name="inputStr">字符串</param>
        /// <returns>是否为空或空格</returns>
        public static bool IsNullOrWhiteSpace(this string inputStr)
        {
            return string.IsNullOrWhiteSpace(inputStr);
        }

        /// <summary>
        /// 拼接字符串
        /// </summary>
        /// <param name="inputStr">原字符串</param>
        /// <param name="obj">拼接的对象</param>
        /// <returns>字符串</returns>
        public static string Format(this string inputStr, params object[] obj)
        {
            return string.Format(inputStr, obj);
        }

        #endregion 空判断

        #region 字符串截取

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="length">截取长度</param>
        /// <returns>字符串</returns>
        public static string Sub(this string inputStr, int length)
        {
            if (string.IsNullOrEmpty(inputStr))
            {
                return null;
            }

            return inputStr.Length >= length ? inputStr.Substring(0, length) : inputStr;
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="oldStr">原字符串</param>
        /// <param name="newStr">新字符串</param>
        /// <returns>替换结果</returns>
        public static string TryReplace(this string inputStr, string oldStr, string newStr)
        {
            return inputStr.IsNullOrEmpty() ? inputStr : inputStr.Replace(oldStr, newStr);
        }

        /// <summary>
        /// 正则替换
        /// </summary>
        /// <param name="inputStr">输入</param>
        /// <param name="pattern">原字符串</param>
        /// <param name="replacement">新字符串</param>
        /// <returns>替换结果</returns>
        public static string RegexReplace(this string inputStr, string pattern, string replacement)
        {
            return inputStr.IsNullOrEmpty() ? inputStr : Regex.Replace(inputStr, pattern, replacement);
        }

        /// <summary>
        /// 截取指定字符串中间的一部分字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="startStr">开始字符串</param>
        /// <param name="endStr">结束字符串</param>
        /// <returns>结果</returns>
        public static string MidStrEx(string source, string startStr, string endStr)
        {
            var result = string.Empty;
            var startIndex = source.IndexOf(startStr, StringComparison.Ordinal);
            if (startIndex == -1)
            {
                return result;
            }

            var tmpStr = source.Substring(startIndex + startStr.Length);
            var endIndex = tmpStr.IndexOf(endStr, StringComparison.Ordinal);
            if (endIndex == -1)
            {
                return result;
            }

            result = tmpStr.Remove(endIndex);

            return result;
        }

        #endregion 字符串截取

        #region 读取配置文件

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        /// <param name="inputStr">配置的key</param>
        /// <returns>数据库链接字符串</returns>
        public static string ValueOfConnectionString(this string inputStr)
        {
            var config = GetConfiguration().GetConnectionString(inputStr);
            if (string.IsNullOrEmpty(config))
            {
                var msg = $"appsettings.json中的ConnectionStrings未设置属性【{inputStr}】";
                throw new Exception(msg);
            }

            return config;
        }

        /// <summary>
        /// 自定义配置参数
        /// </summary>
        /// <param name="inputStr">配置的key</param>
        /// <param name="throwException">是否抛错</param>
        /// <returns>配置的值</returns>
        public static string GetAppSettingValue(this string inputStr, bool throwException = true)
        {
            var config = GetConfiguration();
            if (config.GetChildren().All(x => x.Key != inputStr))
            {
                if (!throwException)
                {
                    return null;
                }

                var msg = $"appsettings.json中的未设置项【{inputStr}】";
                throw new Exception(msg);
            }

            var section = config.GetSection(inputStr);
            return section.Value;
        }

        #endregion 读取配置文件

        #region 字符格式化

        /// <summary>
        /// 字符格式化
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="param">参数</param>
        /// <returns>结果</returns>
        public static string Fmt(this string input, params object[] param)
        {
            if (input.IsNullOrWhiteSpace())
            {
                return null;
            }

            var result = string.Format(input, param);
            return result;
        }

        #endregion 字符格式化

        #region 格式化文本

        /// <summary>
        /// 格式化电话
        /// </summary>
        /// <param name="mobile">电话</param>
        /// <returns>格式化结果</returns>
        public static string FmtMobile(this string mobile)
        {
            if (!mobile.IsNullOrEmpty() && mobile.Length > 7)
            {
                var regex = new Regex(@"(?<=\d{3}).+(?=\d{4})", RegexOptions.IgnoreCase);
                mobile = regex.Replace(mobile, "****");
            }

            return mobile;
        }

        /// <summary>
        /// 格式化证件号码
        /// </summary>
        /// <param name="idcard">证件号</param>
        /// <returns>格式化结果</returns>
        public static string FmtIdCard(this string idcard)
        {
            if (!idcard.IsNullOrEmpty() && idcard.Length > 10)
            {
                var regex = new Regex(@"(?<=\w{6}).+(?=\w{4})", RegexOptions.IgnoreCase);
                idcard = regex.Replace(idcard, "********");
            }

            return idcard;
        }

        /// <summary>
        /// 格式化银行卡号
        /// </summary>
        /// <param name="bankCard">银行卡号</param>
        /// <returns>格式化结果</returns>
        public static string FmtBankCard(this string bankCard)
        {
            if (!bankCard.IsNullOrEmpty() && bankCard.Length > 4)
            {
                var regex = new Regex(@"(?<=\d{4})\d+(?=\d{4})", RegexOptions.IgnoreCase);
                bankCard = regex.Replace(bankCard, " **** **** ");
            }

            return bankCard;
        }

        /// <summary>
        /// Url编码
        /// </summary>
        /// <param name="str">未编码url</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// Url解码
        /// </summary>
        /// <param name="url">未解码url</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(this string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        #endregion 格式化文本

        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <returns>配置文件</returns>
        private static IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true).Build();
        }
    }

    /// <summary>
    /// json对象扩展
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 序列化为json对象
        /// </summary>
        /// <param name="o">对象</param>
        /// <param name="converters">自定义序列化</param>
        /// <returns>json字符串</returns>
        public static string ToJson(this object o, params JsonConverter[] converters)
        {
            if (o is string)
            {
                return o + string.Empty;
            }

            var d = o as IXSerializable;
            if (d == null)
            {
                return JsonConvert.SerializeObject(o, converters);
            }

            var s = d.Serialize();
            return !s.NotValid() ? s : JsonConvert.SerializeObject(o, converters);
        }

        /// <summary>
        /// 将json反序列化为对象
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="type">对象类型</param>
        /// <param name="converters">自定义序列化</param>
        /// <returns>对象</returns>
        public static object FromJson(this string json, Type type = null, params JsonConverter[] converters)
        {
            if (type == typeof(string))
            {
                return json;
            }

            return type == null
                ? JsonConvert.DeserializeObject(json)
                : JsonConvert.DeserializeObject(json, type, converters);
        }

        /// <summary>
        /// 将json反序列化为对象
        /// </summary>
        /// <typeparam name="T">转化类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="converters">自定义序列化</param>
        /// <returns>对象</returns>
        public static T FromJson<T>(this string json, params JsonConverter[] converters)
        {
            if (typeof(T) != typeof(string))
            {
                return JsonConvert.DeserializeObject<T>(json, converters);
            }

            object o = json;
            return (T)o;
        }
    }

    /// <summary>
    /// 有效性拓展
    /// </summary>
    public static class ValidExtensions
    {
        /// <summary>
        /// 判断对象是否无效
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>是否无效</returns>
        public static bool NotValid(this object obj)
        {
            return obj == null || (obj + string.Empty).Length == 0;
        }

        /// <summary>
        /// 判断集合是否无效
        /// </summary>
        /// <param name="collection">集合</param>
        /// <returns>是否无效</returns>
        public static bool NotValid(this ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// 判断guid是否无效
        /// </summary>
        /// <param name="obj">guid</param>
        /// <returns>是否无效</returns>
        public static bool NotValid(this Guid obj)
        {
            return obj != Guid.Empty;
        }

        /// <summary>
        /// 判断数组是否无效
        /// </summary>
        /// <param name="enumerable">数组</param>
        /// <returns>是否无效</returns>
        public static bool NotValid(this IEnumerable enumerable)
        {
            if (enumerable == null)
            {
                return true;
            }

            var en = enumerable.GetEnumerator();
            var r = en.MoveNext();

            return !r;
        }
    }

    /// <summary>
    /// 加密拓展
    /// </summary>
    public static class EncryptExtension
    {
        /// <summary>
        /// 将字符串使用base64算法编码
        /// </summary>
        /// <param name="source">待加密的字符串</param>
        /// <param name="encodingName">编码类型（编码名称）
        /// * 代码页 名称
        /// * 1200 "UTF-16LE"、"utf-16"、"ucs-2"、"unicode"或"ISO-10646-UCS-2"
        /// * 1201 "UTF-16BE"或"unicodeFFFE"
        /// * 1252 "windows-1252"
        /// * 65000 "utf-7"、"csUnicode11UTF7"、"unicode-1-1-utf-7"、"unicode-2-0-utf-7"、"x-unicode-1-1-utf-7"或"x-unicode-2-0-utf-7"
        /// * 65001 "utf-8"、"unicode-1-1-utf-8"、"unicode-2-0-utf-8"、"x-unicode-1-1-utf-8"或"x-unicode-2-0-utf-8"
        /// * 20127 "us-ascii"、"us"、"ascii"、"ANSI_X3.4-1968"、"ANSI_X3.4-1986"、"cp367"、"csASCII"、"IBM367"、"iso-ir-6"、"ISO646-US"或"ISO_646.irv:1991"
        /// * 54936 "GB18030"
        /// </param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64String(this string source, string encodingName = "UTF-8")
        {
            byte[] bytes = Encoding.GetEncoding(encodingName).GetBytes(source);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 将字符串使用base64算法解码
        /// </summary>
        /// <param name="base64String">已用base64算法加密的字符串</param>
        /// <param name="encodingName">编码类型</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64String(this string base64String, string encodingName = "UTF-8")
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64String);
                return Encoding.GetEncoding(encodingName).GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将字符串使用MD5算法解码
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>md5加密结果</returns>
        public static string EncodeMd5String(this string input)
        {
            if (input.IsNullOrEmpty())
            {
                return input;
            }

            var md5 = MD5.Create();
            var result = md5.ComputeHash(Encoding.Default.GetBytes(input));
            return BitConverter.ToString(result).Replace("-", string.Empty);
        }
    }
}