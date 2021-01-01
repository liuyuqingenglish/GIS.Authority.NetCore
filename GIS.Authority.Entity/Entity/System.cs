using DapperExtensions.Mapper;
using GIS.Authority.Entity.Base.BaseEntity;
namespace GIS.Authority.Entity
{
    /// <summary>
    /// 部门
    /// </summary>
    public class System : DisableEntity
    {
        /// <summary>
        /// 系统代码
        /// </summary>
        public string Code { get; set; }
    }

    /// <summary>
    /// Mapper
    /// </summary>
    public sealed class SystemMapper : ClassMapper<System>
    {
        /// <summary>
        /// OrganizationMapper
        /// </summary>
        public SystemMapper()
        {
            ///映射表名
            Table("System");
            ///指定主键
            Map(x => x.Id).Key(KeyType.Guid);
            ///忽略remark列
            //Map(x => x.Remark).Ignore();
            ///自动映射
            AutoMap();
        }
    }
}