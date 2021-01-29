using DapperExtensions.Mapper;
using System;
using GIS.Authority.Entity.Base.BaseEntity;
namespace GIS.Authority.Entity
{
    /// <summary>
    /// 部门
    /// </summary>
    public class SystemConfig : OperatorEntity
    {


        /// <summary>
        /// 配置类型
        /// </summary>
        public string LastUpdateTime { get; set; }

        /// <summary>
        /// 配置数据
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 配置类型
        /// </summary>
        public string ConfigType { get; set; }

        /// <summary>
        /// 配置数据
        /// </summary>
        public string ConfigData { get; set; }

        /// <summary>
        /// 系统id
        /// </summary>
        public Guid SystemId { get; set; }
    }

    /// <summary>
    /// Mapper
    /// </summary>
    public sealed class SystemConfigMapper : ClassMapper<SystemConfig>
    {
        /// <summary>
        /// OrganizationMapper
        /// </summary>
        public SystemConfigMapper()
        {
            ///映射表名
            Table("SystemConfig");
            ///指定主键
            Map(x => x.Id).Key(KeyType.Guid);
            ///忽略remark列
            //Map(x => x.Remark).Ignore();
            ///自动映射
            AutoMap();
        }
    }
}