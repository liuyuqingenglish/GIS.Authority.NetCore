/*
* ==============================================================================
*
* Filename: DepartmentRepository
* ClrVersion: 4.0.30319.42000
* Description: DepartmentRepository
*
* Version: 1.0
* Created: 2019/12/26 17:21:49
* Compiler: Visual Studio 2017
*
* Author: liuyuqing
* Copyright: 广东满天星云信息技术有限公司
*
* ==============================================================================
*/

using DapperExtensions;
using GIS.Authority.Common;
using GIS.Authority.Dal.Base.BaseDal;
using GIS.Authority.Entity;

namespace GIS.Authority.Dal
{
    public class RoleGroupRepository : BasicRepository<RoleGroup>, IRoleGroupRepository
    {
        public bool AddRoleGroup(RoleGroup role)
        {
            return base.Insert(role);
        }

        public bool DeleteRoleGroup(PredicateGroup group)
        {
            return base.Delete(group);
        }

        public PageResult<RoleGroup> GetRoleGroup(PredicateGroup group, PageQuery query)
        {
            return base.GetPager(group, query);
        }

        public bool UpdateRoleGroup(RoleGroup role)
        {
            return base.Update(role);
        }
    }
}