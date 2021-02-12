/*
* ==============================================================================
*
* Filename: DepartmentDto
* ClrVersion: 4.0.30319.42000
* Description: DepartmentDto
*
* Version: 1.0
* Created: 2020/1/11 14:31:37
* Compiler: Visual Studio 2017
*
* Author: liuyuqing
* Copyright: 广东满天星云信息技术有限公司
*
* ==============================================================================
*/
using System;
namespace GIS.Authority.Entity
{
    public class RoleGroupPermissionDto : BaseDto
    {
        /// <summary>
        /// 模块类型
        /// </summary>
        public Guid RoleGroupId { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public Guid ModuleId { get; set; }
    }
}