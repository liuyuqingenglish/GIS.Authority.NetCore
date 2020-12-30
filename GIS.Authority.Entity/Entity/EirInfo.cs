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
    public class EirInfo : BaseEntity<Guid>
    {
        #region 公共属性

        /// <summary>
        /// EirId
        /// </summary>
        public Guid EirId { get; set; }

        /// <summary>
        /// EirIp
        /// </summary>
        public string EirIp { get; set; }

        /// <summary>
        /// EirPort
        /// </summary>
        public string EirPort { get; set; }

        /// <summary>
        /// EirFqdn
        /// </summary>
        public string EirFqdn { get; set; }

        /// <summary>
        /// EirName
        /// </summary>
        public string EirName { get; set; }

        /// <summary>
        /// EirDescription
        /// </summary>
        public string EirDescription { get; set; }

        /// <summary>
        /// CreateTime
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// UpdateTime
        /// </summary>
        public string UpdateTime { get; set; }

        #endregion 公共属性
    }

    public class EirInfoMapper : ClassMapper<EirInfo>
    {
        public EirInfoMapper()
        {
            Table("eir_info");
            Map(item => item.EirId).Column("eir_id");
            Map(item => item.EirIp).Column("eir_ip");
            Map(item => item.EirPort).Column("eir_port");
            Map(item => item.EirName).Column("eir_name");
            Map(item => item.CreateTime).Column("create_time");
            Map(item => item.UpdateTime).Column("update_time");
            Map(item => item.EirFqdn).Column("eir_fqdn");
            Map(item => item.EirDescription).Column("eir_description");
            AutoMap();
        }
    }
}