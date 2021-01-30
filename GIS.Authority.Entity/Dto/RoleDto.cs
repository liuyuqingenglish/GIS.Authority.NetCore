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
    public class RoleDto : BaseDto
    {
        public string OrganizationId { get; set; }

        public int RoleGroup { get; set; }
    }
}