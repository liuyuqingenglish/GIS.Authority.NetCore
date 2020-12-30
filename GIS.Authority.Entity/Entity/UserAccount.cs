using DapperExtensions.Mapper;
using System;
using GIS.Authority.Entity.Base.BaseEntity;
namespace GIS.Authority.Entity
{
    /// <summary>
    /// 部门
    /// </summary>
    public class UserAccount : CommonEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 组织id
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// webtoken
        /// </summary>
        public string WebToken { get; set; }

        /// <summary>
        /// webtoken
        /// </summary>
        public string MobileToken { get; set; }

        /// <summary>
        /// 登录错误次数
        /// </summary>
        public int LoginFaultCount { get; set; }

        /// <summary>
        /// 是否第一次登录
        /// </summary>
        public bool LsFirstLonin { get; set; }

        /// <summary>
        /// 系统主页面地址
        /// </summary>

        public string MainPageUrl { get; set; }
    }

    /// <summary>
    /// Mapper
    /// </summary>
    public sealed class UserAccountMapper : ClassMapper<UserAccount>
    {
        /// <summary>
        /// OrganizationMapper
        /// </summary>
        public UserAccountMapper()
        {
            ///映射表名
            Table("UserAccount");
            ///指定主键
            Map(x => x.Id).Key(KeyType.Guid);
            ///忽略remark列
            //Map(x => x.Remark).Ignore();
            ///自动映射
            AutoMap();
        }
    }
}