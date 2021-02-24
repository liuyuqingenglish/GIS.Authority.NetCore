﻿/*
* ==============================================================================
*
* Filename: DepartmentService
* ClrVersion: 4.0.30319.42000
* Description: DepartmentService
*
* Version: 1.0
* Created: 2020/1/11 9:03:15
* Compiler: Visual Studio 2017
*
* Author: liuyuqing
* Copyright: 广东满天星云信息技术有限公司
*
* ==============================================================================
*/

using DapperExtensions;
using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using GIS.Authority.Dal.UnitOfWork;
using GIS.Authority.Common;
namespace GIS.Authority.Service
{
    public class UserRoleService : BaseService, IUserRoleService
    {
        public UserRoleService(IUnitOfWork unit) : base(unit)
        {

        }

        public bool AddUserRole(UserRoleDto dto)
        {
            return Unit.UserRoleGroupRepository.AddUserRoleGroup(dto.ToModel<GIS.Authority.Entity.UserRole>());
        }

        public bool DeleteUserRole(List<Guid> orgid)
        {
            PredicateGroup group = new PredicateGroup();
            group.Operator = GroupOperator.And;
            foreach (Guid item in orgid)
            {
                group.Predicates.Add(Predicates.Field<GIS.Authority.Entity.System>(d => d.Id, Operator.Eq, item));
            }
            return Unit.UserRoleGroupRepository.Delete(group);
        }

        public PageResult<UserRoleDto> GetUserRoleDto( PageQueryCondition<ProtocolQueryRole,PageQuery> query)
        {
            PredicateGroup group = new PredicateGroup();
            group.Operator = GroupOperator.And;
            if (!string.IsNullOrEmpty(query.Condition.OrganizeId))
            {
                group.Predicates.Add(Predicates.Field<Role>(d => d.OrganizationId, Operator.Eq, query.Condition.OrganizeId));
            }
            if (!string.IsNullOrEmpty(query.Condition.OrganizeId))
            {
                group.Predicates.Add(Predicates.Field<Role>(d => d.Id, Operator.Eq, query.Condition.RoleId));
            }
            if (!string.IsNullOrEmpty(query.Condition.RoleName))
            {
                group.Predicates.Add(Predicates.Field<Role>(d => d.Name, Operator.Like, query.Condition.RoleName));
            }
            return Unit.UserRoleGroupRepository.GetUserRoleGroup(group, query.Query).ToPageModel<GIS.Authority.Entity.UserRole, UserRoleDto>();
        }

        public bool UpdateUserRole(UserRoleDto dto)
        {
            return Unit.UserRoleGroupRepository.UpdateUserRoleGroup(dto.ToModel<GIS.Authority.Entity.UserRole>());
        }
    }
}