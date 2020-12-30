/*
* ==============================================================================
*
* Filename: Infrastructure
* ClrVersion: 4.0.30319.42000
* Description: 分页请求
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

using System.Collections.Generic;

namespace GIS.Authority.Common
{
    /// <summary>
    /// 分页请求
    /// </summary>
    public class PageQuery
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public PageQuery()
        {
            OrderList = new List<OrderItem>();
        }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 排序选项
        /// </summary>
        public IList<OrderItem> OrderList { get; set; }
    }
}