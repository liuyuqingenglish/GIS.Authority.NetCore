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
    public class CheckRecord : BaseEntity<Guid>
    {
        #region 公共属性

        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public Guid ImeiId { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime ResponseTime { get; set; }

        /// <summary>
        /// supi
        /// </summary>
        public string Supi { get; set; }
        #endregion 公共属性
    }

    public class CheckRecordMapper : ClassMapper<CheckRecord>
    {
        public CheckRecordMapper()
        {
            Table("check_record");
            Map(item => item.RequestTime).Column("request_time");
            Map(item => item.ResponseTime).Column("response_time");
            Map(item => item.ImeiId).Column("imei_id");
            AutoMap();
        }
    }
}