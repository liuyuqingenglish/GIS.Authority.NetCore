/*
* ==============================================================================
*
* Filename: DeviceDto
* ClrVersion: 4.0.30319.42000
* Description: 设备管理对象
*
* Version: 1.0
* Created: 2019/6/5 11:32:09
* Compiler: Visual Studio 2017
*
* Author: lifu
* Copyright: lyq
*
* ==============================================================================
*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace GIS.Authority.Contract
{
    /// <summary>
    /// 协议解析对象
    /// </summary>
  public  class AnalysisHelper
    {
        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="jsonText">json字符串</param>
        /// <returns>返回对象</returns>
        public static T ToProtocol<T>(string jsonText)
        {
            return ToObject<T>(jsonText);
        }

        /// <summary>
        /// 获取指定的字段
        /// </summary>
        /// <param name="jsonText">json字符串</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns>字符串</returns>
        public static string GetFiledFromJson(string jsonText, string fieldName)
        {
            try
            {
                JObject jobject = JObject.Parse(jsonText);
                string result = jobject[fieldName].ToString();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 序列号对象为json字符串
        /// </summary>
        /// <param name="data">对象</param>
        /// <returns>json字符串</returns>
        public static string ToJson(object data)
        {
            var setting = new JsonSerializerSettings
            {
                //讲首字母转为小写
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
            };
            var json = JsonConvert.SerializeObject(data, setting);

            // return JsonConvert.SerializeObject(data);
            return json;
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <typeparam name="T">对象泛型</typeparam>
        /// <param name="jsonText">json字符串</param>
        /// <returns>对象</returns>
        public static T ToObject<T>(string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText);
        }
    }
}