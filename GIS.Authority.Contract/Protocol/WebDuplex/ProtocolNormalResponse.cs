/*
* ==============================================================================
*
* Filename: ProtocolNormalResponse
* ClrVersion: 4.0.30319.42000
* Description:通用回复协议
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
    /// 通用回复协议
    /// </summary>
    public class ProtocolNormalResponse : BaseResponse
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProtocolNormalResponse()
            : base()
        {
            this.ProtocolType = (int)GIS.Authority.Contract.ProtocolType.NormalResponse;
        }
    }
}
