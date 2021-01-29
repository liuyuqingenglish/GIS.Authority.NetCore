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
using System.Collections.Generic;
using System.Linq;

namespace GIS.Authority.Dal
{
    public class UserRoleGroupRepository : BasicRepository<UserRoleGroup>, IUserRoleGroupRepository
    {
        public bool AddUserRoleGroup(UserRoleGroup UserRoleGroup)
        {
            return base.Insert(UserRoleGroup);
        }

        public bool DeleteUserRoleGroup(PredicateGroup group)
        {
            return base.Delete(group);
        }

        public PageResult<UserRoleGroup> GetUserRoleGroup(PredicateGroup group, PageQuery query)
        {
            return base.FindByPage(group, query);
        }

        public bool UpdateUserRoleGroup(UserRoleGroup UserRoleGroup)
        {
            return base.Update(UserRoleGroup);
        }

        public List<UserRoleGroup> GetUserRoleGroup(PredicateGroup group)
        {
            return base.GetList(group).ToList();
        }
    }
}