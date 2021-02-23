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
using GIS.Authority.Dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using GIS.Authority.Common;

namespace GIS.Authority.Service
{
    public class OrganizeService : BaseService, IOrganizeService
    {
        public OrganizeService(IUnitOfWork unit) : base(unit)
        {
        }

        public bool AddOrganize(OrganizationDto dto)
        {
            return Unit.SystemRepository.AddSystem(dto.ToModel<GIS.Authority.Entity.System>());
        }

        public bool DeleteOrganize(List<Guid> orgid)
        {
            PredicateGroup group = new PredicateGroup();
            group.Operator = GroupOperator.And;
            foreach (Guid item in orgid)
            {
                group.Predicates.Add(Predicates.Field<GIS.Authority.Entity.System>(d => d.Id, Operator.Eq, item));
            }
            return Unit.SystemRepository.Delete(group);
        }

        public PageResult<OrganizationDto> GetOrganizeDto(PageQueryCondition<ProtocolQueryOrganize,PageQuery> query)
        {
            PredicateGroup group = new PredicateGroup();
            group.Operator = GroupOperator.And;
            if (!string.IsNullOrEmpty(query.Condition.SystemId))
            {
                group.Predicates.Add(Predicates.Field<Department>(d => d.Id, Operator.Eq, query.Condition.SystemId));
            }
            if (!string.IsNullOrEmpty(query.Condition.OrganizeName))
            {
                group.Predicates.Add(Predicates.Field<Department>(d => d.Name, Operator.Eq, query.Condition.OrganizeName));
            }
            return Unit.OrganizeRepositiry.GetOrganize(group, query.Query).ToPageModel<GIS.Authority.Entity.Organization, OrganizationDto>();
        }

        public bool UpdateOrganize(OrganizationDto dto)
        {
            return Unit.OrganizeRepositiry.UpdateOrganize(dto.ToModel<GIS.Authority.Entity.Organization>());
        }
    }
}