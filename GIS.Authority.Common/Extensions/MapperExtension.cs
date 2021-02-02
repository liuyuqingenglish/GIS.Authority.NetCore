/*
* ==============================================================================
*
* Filename: MapperExtension
* ClrVersion: 4.0.30319.42000
* Description: AutoMapper初始化映射
*
* Version: 1.0
* Created: 2019/8/1 17:40:45
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using GIS.Authority.Entity.Base;
using GIS.Authority;
using GIS.Authority.Common;
namespace GIS.Authority.Common
{
    /// <summary>
    /// AutoMapper拓展
    /// </summary>
    public static class MapperExtension
    {
        /// <summary>
        /// 全局mapper
        /// </summary>
        public static IMapper Mapper;

        /// <summary>
        /// entity实体转TResult类型
        /// </summary>
        /// <typeparam name="TResult">转换结果泛型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns>转换结果</returns>
        public static TResult ToModel<TResult>(this object entity)
        {
            return Mapper.Map<TResult>(entity);
        }

        /// <summary>
        /// TInput类型的list转成TResult类型
        /// </summary>
        /// <typeparam name="TInput">输入泛型</typeparam>
        /// <typeparam name="TResult">输出泛型</typeparam>
        /// <param name="list">输入数组</param>
        /// <returns>输出数组</returns>
        public static List<TResult> ToListDto<TInput, TResult>(this IEnumerable<TInput> list)
        {
            return list?.Select(x => x.ToModel<TResult>()).ToList();
        }

        /// <summary>
        /// TInput类型的PageResult转成TResult类型
        /// </summary>
        /// <typeparam name="TInput">输入泛型</typeparam>
        /// <typeparam name="TResult">输出泛型</typeparam>
        /// <param name="pageResult">翻页结果</param>
        /// <returns>转化翻页结果</returns>
        public static PageResult<TResult> ToPageModel<TInput, TResult>(this PageResult<TInput> pageResult)
        {
            return new PageResult<TResult> { Total = pageResult.Total, Row = pageResult.Row.ToListDto<TInput, TResult>() };
        }

        /// <summary>
        /// 自动更新
        /// </summary>
        /// <typeparam name="TResult">结果类型</typeparam>
        /// <param name="entity">数据库实体</param>
        /// <param name="input">输入更新</param>
        public static void UpdateFrom<TResult>(this TResult entity, TResult input)
        {
            //var properties = entity.GetType().GetProperties();
            //foreach (var property in properties)
            //{
            //    var notMapper = property.GetCustomAttribute<NoMappingAttribute>();
            //    if (notMapper == null)
            //    {
            //        property.SetValue(entity, property.GetValue(input));
            //    }
            //}
        }
    }
}