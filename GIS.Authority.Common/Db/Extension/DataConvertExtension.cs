/*
* ==============================================================================
*
* Filename: DataConvertExtension
* ClrVersion: 4.0.30319.42000
* Description: DataConvertExtension
*
* Version: 1.0
* Created: 2020/7/3 17:21:49
* Compiler: Visual Studio 2017
*
* Author: liuyuqing
* Copyright: 广东满天星云信息技术有限公司
*
* ==============================================================================
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace GIS.Authority.Common
{
    public class DataConvertExtension<T> where T : new()
    {
        private static T GetEntity(DataRow row)
        {
            T entity = new T();
            Type type = typeof(T);
            MemberInfo[] infos = type.GetMembers();
            foreach (MemberInfo item in infos)
            {
                if (item.MemberType.Equals(MemberTypes.Property))
                {
                    if (row[item.Name].Equals(null))
                    {
                        continue;
                    }
                    if (row.Table.Columns.Contains(item.Name.ToLower()))
                    {
                        type.GetProperty(item.Name).SetValue(entity, row[item.Name]);
                    }
                }
            }
            return entity;
        }

        public static List<T> GetList(DataTable table)
        {
            List<T> list = new List<T>();
            foreach (DataRow item in table.Rows)
            {
                list.Add(GetEntity(item));
            }
            return list;
        }
        public static T Get(DataTable table)
        {
            return GetEntity(table.Rows[0]);
        }
    }
}