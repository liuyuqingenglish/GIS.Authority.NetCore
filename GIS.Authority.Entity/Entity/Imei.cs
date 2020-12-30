/*
* ==============================================================================
*
* Filename: AlarmRecord
* ClrVersion: 4.0.30319.42000
* Description: 报警记录实体
*
* Version: 1.0
* Created: 2020/4/9 17:09:31
* Compiler: Visual Studio 2017
*
* Author: dgf
* Copyright: lyq
*
* ==============================================================================
*/

using DapperExtensions.Mapper;
using System;
using GIS.Authority.Entity.Base.BaseEntity;

namespace GIS.Authority.Entity
{
    /// <summary>
    /// 报警记录实体
    /// </summary>
    public class Imei : BaseEntity<Guid>
    {
        #region 公共属性

        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime LastModifiedDate { get; set; } = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        /// <summary>
        ///
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        /// <summary>
        /// 部门ID
        /// </summary>
        public Int32 Result { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public bool Deleted { get; set; }

        #endregion 公共属性
    }

    public class ImeiMapper : ClassMapper<Imei>
    {
        public ImeiMapper()
        {
            Table("imei");
            Map(item => item.LastModifiedDate).Column("last_modified_date");
            Map(item => item.CreatedDate).Column("created_date");
            AutoMap();
        }
    }
}