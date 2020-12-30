/*
* ==============================================================================
*
* Filename: Enum
* ClrVersion: 4.0.30319.42000
* Description: 排序项
*
* Version: 1.0
* Created: 2019/7/27 15:20:08
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

namespace GIS.Authority.Common
{
    /// <summary>
    /// 排序项
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public OrderType OrderType { get; set; }
    }
}