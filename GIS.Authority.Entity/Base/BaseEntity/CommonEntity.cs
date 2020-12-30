/*
* ==============================================================================
*
* Filename: CommonEntity
* ClrVersion: 4.0.30319.42000
* Description: 普通实体，包括名称、备注、禁用
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
    /// 普通实体，包括名称、备注、禁用
    /// </summary>
    public class CommonEntity : OperatorEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}