/*
* ==============================================================================
*
* Filename: BaseResponse
* ClrVersion: 4.0.30319.42000
* Description:回复请求的基类
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

using GIS.Authority.Contract;

namespace GIS.Authority.Contract
{
    /// <summary>
    /// 回复请求的基类
    /// </summary>
    public class BaseResponse : BaseProtocol
    {
        /// <summary>
        /// 结果代码
        /// </summary>
        public int Code = (int)HttpCode.TAG_SUCCESSFUL;

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success = true;

        /// <summary>
        /// 结果信息
        /// </summary>
        public string Message = string.Empty;
    }
}
