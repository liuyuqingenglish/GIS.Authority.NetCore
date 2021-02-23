/*
* ==============================================================================
*
* Filename: BasicProfile
* ClrVersion: 4.0.30319.42000
* Description: 基础mapper
*
* Version: 1.0
* Created: 2019/8/1 17:28:30
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using AutoMapper;
using System;
using GIS.Authority.Common;
using GIS.Authority.Entity;

namespace GIS.Authority.Service
{
    /// <summary>
    /// 基础mapper
    /// </summary>
    public class BasicProfile : Profile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public BasicProfile()
        {
            CreateMap<DateTime, string>().ConvertUsing(d => d.ToTimeString());
            CreateMap<DateTime?, string>().ConvertUsing(d => d.Value.ToTimeString());
            CreateMap<bool?, bool>().ConvertUsing(d => d.HasValue && d.Value);
            CreateMap<bool, string>().ConvertUsing(d => d ? "是" : "否");
            //// imea
        }
    }
}