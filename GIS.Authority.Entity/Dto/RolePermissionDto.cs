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

namespace GIS.Authority.Entity
{
    public class RolePermissionDto : BaseDto
    {
        /// <summary>
        /// 模块类型
        /// </summary>
        public string ModuleType { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 排序
        /// </summary>

        public int OrderNum { get; set; }

        /// <summary>
        /// 父模块
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 上级
        /// </summary>
        public string XPath { get; set; }
    }
}