/*
* ==============================================================================
*
* Filename: ProtocolHeartbeat
* ClrVersion: 4.0.30319.42000
* Description:心跳
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
    /// 心跳
    /// </summary>
    public class ProtocolHeartbeat : BaseRequest
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ProtocolHeartbeat()
            : base()
        {
            this.ProtocolType = (int)GIS.Authority.Contract.ProtocolType.Heartbeat;
        }
    }
}
