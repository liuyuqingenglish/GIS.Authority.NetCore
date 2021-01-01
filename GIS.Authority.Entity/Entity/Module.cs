using DapperExtensions.Mapper;
using System;
using GIS.Authority.Entity.Base.BaseEntity;
namespace GIS.Authority.Entity
{
    /// <summary>
    /// 模块
    /// </summary>
    public class Module : DisableEntity
    {
        /// <summary>
        /// 给XPath设默认值
        /// </summary>
        public Module()
        {
            XPath = string.Empty;
        }

        /// <summary>
        /// 模块类型(0- 菜单 ，1-模块，2-操作)
        /// </summary>
        public int ModuleType { get; set; }

        /// <summary>
        /// 相对地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// 父级部门Id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 保存父级信息(xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxx|xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxx|xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxx)
        /// </summary>
        public string XPath { get; set; } = string.Empty;
    }

    /// <summary>
    /// Mapper
    /// </summary>
    public sealed class ModuleMapper : ClassMapper<Module>
    {
        /// <summary>
        /// DepartmentMapper
        /// </summary>
        public ModuleMapper()
        {
            Table("Module");
            AutoMap();
        }
    }
}