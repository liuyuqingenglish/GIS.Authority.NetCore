/*
* ==============================================================================
*
* Filename: OrderNoExtension
* ClrVersion: 4.0.30319.42000
* Description: 排序基础方法
*
* Version: 1.0
* Created: 2020/3/31 20:30:41
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using GIS.Authority.Entity.Base.BaseEntity;

namespace GIS.Authority.Common
{
    /// <summary>
    /// 排序基础方法
    /// </summary>
    public static class OrderNoExtension
    {
        /// <summary>
        /// 排序基础方法
        /// </summary>
        /// <typeparam name="TOrder">有实现IOrderNo的实体都能使用
        /// <see cref="IOrderNo"/>
        /// </typeparam>
        /// <param name="items">排序数组</param>
        /// <param name="moveItemNo">移动项本来的位置</param>
        /// <param name="targetOrderNo">移动的位置</param>
        public static void SortEntity<TOrder>(List<TOrder> items, int moveItemNo, int targetOrderNo)
            where TOrder : BasicEntity, IOrderNo
        {
            var moveItem = items.OrderByDescending(x => x.LastUpdateTime).FirstOrDefault(x => x.OrderNo == moveItemNo);
            if (moveItem == null)
            {
                throw new ArgumentException(nameof(moveItemNo));
            }

            if (moveItem.OrderNo > targetOrderNo)
            {
                foreach (var item in items.Where(r => r.OrderNo < moveItem.OrderNo && r.OrderNo >= targetOrderNo))
                {
                    item.OrderNo += 1;
                }
            }
            else
            {
                foreach (var item in items.Where(r => r.OrderNo > moveItem.OrderNo && r.OrderNo <= targetOrderNo))
                {
                    item.OrderNo -= 1;
                }
            }

            moveItem.OrderNo = targetOrderNo;

            var sortItems = items.OrderBy(r => r.OrderNo).ThenByDescending(x => x.LastUpdateTime).ToList();
            for (var i = 0; i < sortItems.Count; i++)
            {
                sortItems[i].OrderNo = i + 1;
            }
        }
    }
}