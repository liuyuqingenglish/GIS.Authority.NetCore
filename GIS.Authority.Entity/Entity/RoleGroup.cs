using DapperExtensions.Mapper;
using GIS.Authority.Entity.Base.BaseEntity;
namespace GIS.Authority.Entity
{
    /// <summary>
    /// 模块
    /// </summary>
    public class RoleGroup : DisableEntity
    {
        /// <summary>
        /// 给XPath设默认值
        /// </summary>
        public RoleGroup()
        {
        }

        /// <summary>
        /// 组织id
        /// </summary>
        public int OrganizationId { get; set; }
    }

    /// <summary>
    /// Mapper
    /// </summary>
    public sealed class RoleGroupMapper : ClassMapper<Role>
    {
        /// <summary>
        /// DepartmentMapper
        /// </summary>
        public RoleGroupMapper()
        {
            Table("RoleGroup");
            AutoMap();
        }
    }
}