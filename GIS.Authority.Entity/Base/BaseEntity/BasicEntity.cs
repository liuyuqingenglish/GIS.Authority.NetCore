/*
* ==============================================================================
*
* Filename: BasicEntity
* ClrVersion: 4.0.30319.42000
* Description: 基础实体类，包含id，创建时间，更新时间
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

using System;

namespace GIS.Authority.Entity.Base.BaseEntity
{
    /// <summary>
    ///  基础实体类，包含id，创建时间，更新时间
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public class BasicEntity : BaseEntity<Guid>
    {
        /// <summary>
        /// 基础实体类，包含id，创建时间，更新时间
        /// </summary>
        public BasicEntity()
        {
            LastUpdateTime = CreatedTime = DateTime.Now;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }
    }
}