/*
* ==============================================================================
*
* Filename: Infrastructure
* ClrVersion: 4.0.30319.42000
* Description: 分页请求结果
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
    /// 分页请求结果
    /// </summary>
    /// <typeparam name="T">分页内容类型</typeparam>
    public class PageResult<T>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public PageResult()
        {
            Row = new List<T>();
            Total = 0;
        }

        /// <summary>
        /// 请求内容
        /// </summary>
        public IList<T> Row { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int Total { get; set; }
    }
}