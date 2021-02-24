using DapperExtensions.Mapper;
using System;
using GIS.Authority.Entity.Base.BaseEntity;
namespace GIS.Authority.Entity
{
    /// <summary>
    /// 部门
    /// </summary>
    public class UserRole : CommonEntity
    {
        /// <summary>
        /// 系统id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 模型id
        /// </summary>
        public int[] Role { get; set; }

    }

    /// <summary>
    /// Mapper
    /// </summary>
    public sealed class UserRoleMapper : ClassMapper<UserRoleDto>
    {
        /// <summary>
        /// OrganizationMapper
        /// </summary>
        public UserRoleMapper()
        {
            ///映射表名
            Table("UserRole");
            ///指定主键
            Map(x => x.Id).Key(KeyType.Guid);
            Map(x => x.Name).Ignore();
            Map(x => x.Remark).Ignore();
            ///忽略remark列
            //Map(x => x.Remark).Ignore();
            ///自动映射
            AutoMap();
        }
    }
}