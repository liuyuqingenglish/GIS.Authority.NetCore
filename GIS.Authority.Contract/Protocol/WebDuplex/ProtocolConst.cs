/*
* ==============================================================================
*
* Filename: ProtocolConst
* ClrVersion: 4.0.30319.42000
* Description:协议常量
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
    /// 协议常量
    /// </summary>
    public class ProtocolConst
    {
        /// <summary>
        /// 协议头
        /// </summary>
        public const string PROTOCOL_FIRST_HEAD = "gis_fl";

        /// <summary>
        /// 协议头的名称，用于解析json字符串-protocolHead
        /// </summary>
        public const string PROTOCOL_HEAD_FLAG = "protocolHead";

        /// <summary>
        /// 协议类型的名称，用于解析json字符串-protocolType
        /// </summary>
        public const string PROTOCOL_TYPE_FLAG = "protocolType";
    }
}
