/*
* ==============================================================================
*
* Filename: BaseRequest
* ClrVersion: 4.0.30319.42000
* Description:请求消息的基类
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
    /// 请求消息的基类
    /// </summary>
    public class BaseRequest : BaseProtocol
    {
        /// <summary>
        /// token
        /// </summary>
        public string HiCloudToken { set; get; }
    }
}