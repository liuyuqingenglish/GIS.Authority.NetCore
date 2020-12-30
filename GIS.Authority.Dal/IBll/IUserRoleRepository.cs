﻿/*
* ==============================================================================
*
* Filename: IDepartmentRepository
* ClrVersion: 4.0.30319.42000
* Description: IDepartmentRepository
*
* Version: 1.0
* Created: 2019/12/26 17:22:15
* Compiler: Visual Studio 2017
*
* Author: liuyuqing
* Copyright: 广东满天星云信息技术有限公司
*
* ==============================================================================
*/

using DapperExtensions;
using GIS.Authority.Common;
using GIS.Authority.Dal.Base.IBaseDal;
using GIS.Authority.Entity;
using System.Collections.Generic;

namespace GIS.Authority.Dal
{
    public interface IUserRoleRepository : IBasicRepository<UserRole>
    {
        PageResult<UserRole> GetUserRole(PredicateGroup group, PageQuery query);

        bool AddUserRole(UserRole userRole);

        bool UpdateUserRole(UserRole userRole);

        bool DeleteUserRole(PredicateGroup group);

        List<UserRole> GetUserRole(PredicateGroup group);
    }
}