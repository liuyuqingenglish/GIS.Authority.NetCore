/*
* ==============================================================================
*
* Filename: OrderExtension
* ClrVersion: 4.0.30319.42000
* Description: 排序拓展
*
* Version: 1.0
* Created: 2020/3/31 20:30:24
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using DapperExtensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GIS.Authority.Entity.Base;

namespace GIS.Authority.Common
{
    /// <summary>
    /// 排序拓展
    /// </summary>
    internal static class OrderExtension
    {
        /// <summary>
        /// 转换数据结构
        /// </summary>
        /// <typeparam name="T">排序数据结构泛型</typeparam>
        /// <param name="orderList">需要排序的数组</param>
        /// <returns>排序后的数组</returns>
        internal static IList<ISort> ToSortList<T>(this ICollection<OrderItem> orderList)
        {
            var sortList = new List<ISort>();

            //反射判断实体是否存在对应字段
            if (orderList != null && orderList.Count > 0)
            {
                var t = typeof(T);
                foreach (var orderItem in orderList)
                {
                    //假如有重复时，按前面方式排序
                    if (sortList.Any(x => x.PropertyName == orderItem.OrderBy))
                    {
                        continue;
                    }

                    //判断属于实体类属性时才加入
                    var property = t.GetProperty(orderItem.OrderBy);
                    if (property != null && property.MemberType == MemberTypes.Property)
                    {
                        sortList.Add(new Sort
                        {
                            PropertyName = orderItem.OrderBy,
                            Ascending = orderItem.OrderType == OrderType.Asc,
                        });
                    }
                }
            }

            //默认排序方式为id倒序
            if (sortList.Count <= 0)
            {
                sortList.Add(new Sort { PropertyName = "last_modified_date", Ascending = false });
            }

            return sortList;
        }
    }
}