/*
* ==============================================================================
*
* Filename: BaseProtocol
* ClrVersion: 4.0.30319.42000
* Description:协议基类
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

namespace GIS.Authority.Contract
{
    /// <summary>
    /// 协议基类
    /// </summary>
    public class BaseProtocol
    {
        /// <summary>
        /// 协议头
        /// </summary>
        public string ProtocolHead { set; get; }

        /// <summary>
        /// 协议类型--ProtocolType
        /// </summary>
        public int ProtocolType { set; get; }

        /// <summary>
        /// 将json对象解析为协议对象
        /// </summary>
        /// <param name="protocol">json字符串</param>
        /// <typeparam name="T">泛型</typeparam>
        /// <returns>返回解析对象</returns>
        public static T ToProtocol<T>(string protocol)
        {
            return AnalysisHelper.ToProtocol<T>(protocol);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseProtocol()
        {
            //设置协议头
            ProtocolHead = ProtocolConst.PROTOCOL_FIRST_HEAD;
            ProtocolType = -1;
        }

        /// <summary>
        /// 将对象封装为json字符串
        /// </summary>
        /// <returns>json字符串</returns>
        public string ToJson()
        {
            return AnalysisHelper.ToJson(this);
        }

        /// <summary>
        /// 更深层次的解析
        /// </summary>
        /// <returns>对象</returns>
        public object AnalysisProtocol()
        {
            return null;
        }

        /// <summary>
        /// 更深层次的封装
        /// </summary>
        /// <returns>字符串</returns>
        public string PackageProtocol()
        {
            return string.Empty;
        }
    }
}
