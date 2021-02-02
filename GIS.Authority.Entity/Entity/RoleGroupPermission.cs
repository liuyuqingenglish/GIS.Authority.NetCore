using DapperExtensions.Mapper;
using GIS.Authority.Entity.Base.BaseEntity;
namespace GIS.Authority.Entity
{
    /// <summary>
    /// 模块
    /// </summary>
    public class RoleGroupPermission : OperatorEntity
    {
        /// <summary>
        /// 给XPath设默认值
        /// </summary>
        public RoleGroupPermission()
        {
        }

        /// <summary>
        /// 组织id
        /// </summary>
        public int RoleGroupId { get; set; }

        /// <summary>
        /// 模型id
        /// </summary>
        public int ModuleId { get; set; }
    }

    /// <summary>
    /// Mapper
    /// </summary>
    public sealed class RoleGroupPermissionMapper : ClassMapper<RoleGroupPermission>
    {
        /// <summary>
        /// DepartmentMapper
        /// </summary>
        public RoleGroupPermissionMapper()
        {
            Table("RoleGroupPermission");
            AutoMap();
        }
    }
}