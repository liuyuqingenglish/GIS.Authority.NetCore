/*
* ==============================================================================
*
* Filename: BaseEntity
* ClrVersion: 4.0.30319.42000
* Description: 基础实体类，包含id
*
* Version: 1.0
* Created: 2020/4/9 15:31:25
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
    /// 基础实体类，包含id
    /// </summary>
    /// <typeparam name="TE">泛型可以是int，可以是guid等</typeparam>
    public class BaseEntity<TE>
    {
        /// <summary>
        /// id
        /// </summary>
        public TE Id { get; set; }
    }
}