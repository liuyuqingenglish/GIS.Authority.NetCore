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
    public class OrganizationDto : BaseDto
    {
        /// <summary>
        /// 所属系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 系统id
        /// </summary>
        public string SystemId { get; set; }
    }
}