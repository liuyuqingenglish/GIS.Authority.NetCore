/*
* ==============================================================================
*
* Filename: BaseDto
* ClrVersion: 4.0.30319.42000
* Description: BaseDto
*
* Version: 1.0
* Created: 2020/1/11 14:32:37
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
    public class BaseDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public Guid? CreateUserId { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        public Guid? LastUpdateUserId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disable { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
    }
}