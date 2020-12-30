/*
* ==============================================================================
*
* Filename: IOrderNo
* ClrVersion: 4.0.30319.42000
* Description: 排序接口
*
* Version: 1.0
* Created: 2019/7/26 17:26:06
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

namespace GIS.Authority.Entity.Base.BaseEntity
{
    /// <summary>
    /// 排序接口
    /// </summary>
    public interface IOrderNo
    {
        /// <summary>
        /// 排序
        /// </summary>
        int OrderNo { get; set; }
    }
}