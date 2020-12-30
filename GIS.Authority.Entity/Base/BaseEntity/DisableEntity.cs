/*
* ==============================================================================
*
* Filename: Infrastructure
* ClrVersion: 4.0.30319.42000
* Description: 软删除实体
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

namespace GIS.Authority.Entity.Base.BaseEntity
{
    /// <summary>
    /// 软删除实体
    /// </summary>
    public class DisableEntity : SoftDeleteEntity
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool Disable { get; set; }
    }
}