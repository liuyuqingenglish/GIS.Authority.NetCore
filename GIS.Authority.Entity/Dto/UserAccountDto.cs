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
using System.Collections.Generic;

namespace GIS.Authority.Entity
{
    public class UserAccountDto : BaseDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 组织id
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// webtoken
        /// </summary>
        public string WebToken { get; set; }

        /// <summary>
        /// webtoken
        /// </summary>
        public string MobileToken { get; set; }

        /// <summary>
        /// 登录错误次数
        /// </summary>
        public int LoginFaultCount { get; set; }

        /// <summary>
        /// 是否第一次登录
        /// </summary>
        public bool LsFirstLonin { get; set; }

        /// <summary>
        /// 权限模块
        /// </summary>
        public List<RolePermissionDto> Modules { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否保存登录
        /// </summary>
        public bool IsRemain { get; set; }
    }
}