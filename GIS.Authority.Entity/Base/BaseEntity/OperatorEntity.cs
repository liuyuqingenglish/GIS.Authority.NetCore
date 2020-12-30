/*
* ==============================================================================
*
* Filename: OperatorEntity
* ClrVersion: 4.0.30319.42000
* Description: 操作实体，包括创建者，更新者
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
    /// 操作实体，包括创建者，更新者
    /// </summary>
    public class OperatorEntity : BasicEntity
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        public Guid? CreateUserId { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        public Guid? LastUpdateUserId { get; set; }
    }
}