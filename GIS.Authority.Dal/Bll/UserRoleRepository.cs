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
    public class UserRoleRepository : BasicRepository<UserRole>, IUserRoleRepository
    {
        public bool AddUserRole(UserRole userRole)
        {
            return base.Insert(userRole);
        }

        public bool DeleteUserRole(PredicateGroup group)
        {
            return base.Delete(group);
        }

        public PageResult<UserRole> GetUserRole(PredicateGroup group, PageQuery query)
        {
            return base.FindByPage(group, query);
        }

        public bool UpdateUserRole(UserRole userRole)
        {
            return base.Update(userRole);
        }

        public List<UserRole> GetUserRole(PredicateGroup group)
        {
            return base.GetList(group).ToList();
        }
    }
}