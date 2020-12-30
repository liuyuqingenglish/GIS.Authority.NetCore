using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace GIS.Authority.Common.Helpers
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            var json = JsonConvert.SerializeObject(o, Formatting.Indented);
            return json;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            var serializer = new JsonSerializer();
            var sr = new StringReader(json);
            var o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            var t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            var serializer = new JsonSerializer();
            var sr = new StringReader(json);
            var o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            var list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            var t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

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
                Formatting = Formatting.Indented,
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

        public static object ToObject(string jsonText)
        {
            return JsonConvert.DeserializeObject(jsonText);
        }
    }
}