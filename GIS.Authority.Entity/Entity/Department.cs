using DapperExtensions.Mapper;
using System;
using GIS.Authority.Entity.Base.BaseEntity;
namespace GIS.Authority.Entity
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department : CommonEntity
    {
        /// <summary>
        /// 给XPath设默认值
        /// </summary>
        public Department()
        {
            XPath = string.Empty;
        }

        /// <summary>
        /// 组织id
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// 父级部门Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 保存父级信息(xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxx|xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxx|xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxx)
        /// </summary>
        public string XPath { get; set; } = string.Empty;
    }

    /// <summary>
    /// Mapper
    /// </summary>
    public sealed class DepartmentMapper : ClassMapper<Department>
    {
        /// <summary>
        /// DepartmentMapper
        /// </summary>
        public DepartmentMapper()
        {
            Table("Department");
            AutoMap();
        }
    }
}