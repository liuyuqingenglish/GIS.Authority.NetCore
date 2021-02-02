/*
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
    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IUnitOfWork unit) : base(unit)
        {

        }

        public bool AddRole(RoleDto dto)
        {
            return Unit.SystemRepository.AddSystem(dto.ToModel<GIS.Authority.Entity.System>());
        }

        public bool DeleteRole(List<Guid> orgid)
        {
            PredicateGroup group = new PredicateGroup();
            group.Operator = GroupOperator.And;
            foreach (Guid item in orgid)
            {
                group.Predicates.Add(Predicates.Field<GIS.Authority.Entity.System>(d => d.Id, Operator.Eq, item));
            }
            return Unit.SystemRepository.Delete(group);
        }

        public List<RoleDto> GetRoleDto(ProtocolQueryRole query)
        {
            PredicateGroup group = new PredicateGroup();
            group.Operator = GroupOperator.And;
            if (!string.IsNullOrEmpty(query.OrganizeId))
            {
                group.Predicates.Add(Predicates.Field<Role>(d => d.OrganizationId, Operator.Eq, query.OrganizeId));
            }
            if (!string.IsNullOrEmpty(query.OrganizeId))
            {
                group.Predicates.Add(Predicates.Field<Role>(d => d.Id, Operator.Eq, query.RoleId));
            }
            if (!string.IsNullOrEmpty(query.RoleName))
            {
                group.Predicates.Add(Predicates.Field<Role>(d => d.Name, Operator.Like, query.RoleName));
            }
            return Unit.RoleRepository.GetRole(group, query.Query).ToListDto<GIS.Authority.Entity.Role, RoleDto>().ToList();
        }

        public bool UpdateRole(RoleDto dto)
        {
            return Unit.RoleRepository.UpdateRole(dto.ToModel<GIS.Authority.Entity.Role>());
        }
    }
}