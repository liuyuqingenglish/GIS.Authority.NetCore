/*
* ==============================================================================
*
* Filename: Infrastructure
* ClrVersion: 4.0.30319.42000
* Description: 分页请求（包含条件）
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
using GIS.Authority.Common;
namespace GIS.Authority.Common
{
    /// <summary>
    /// 分页请求（包含条件）
    /// </summary>
    /// <typeparam name="TCondition">查询条件泛型</typeparam>
    public class PageQueryCondition<TCondition,TPageQuery>  where TCondition : class, new() where TPageQuery :class
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        public TCondition Condition { get; set; }

        public TPageQuery Query { get; set; }
    }
}