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
using GIS.Authority.Common;
using GIS.Authority.Dal.UnitOfWork;
namespace GIS.Authority.Service
{
    public class SystemService : BaseService, ISystemService
    {
        public SystemService(IUnitOfWork unit) : base(unit)
        {
        }

        public bool AddSystem(SystemDto dto)
        {
            return Unit.SystemRepository.AddSystem(dto.ToModel<GIS.Authority.Entity.System>());
        }

        public bool DeleteSystem(Guid sysid)
        {
            PredicateGroup group = new PredicateGroup();
            group.Predicates.Add(Predicates.Field<GIS.Authority.Entity.System>(d => d.Id, Operator.Eq, sysid));
            return Unit.SystemRepository.Delete(group);
        }

        public PageResult<SystemDto> GetSystemDto( PageQueryCondition<ProtocolQuerySystem,PageQuery> query)
        {
            PredicateGroup group = new PredicateGroup();
            if (!string.IsNullOrEmpty(query.Condition.SystemId))
            {
                group.Predicates.Add(Predicates.Field<Department>(d => d.Id, Operator.Eq, query.Condition.SystemId));
            }
            if (!string.IsNullOrEmpty(query.Condition.SystemName))
            {
                group.Predicates.Add(Predicates.Field<Department>(d => d.Name, Operator.Eq, query.Condition.SystemName));
            }
            return Unit.SystemRepository.GetSystem(group, query.Query).ToPageModel<GIS.Authority.Entity.System, SystemDto>();
        }

        public bool UpdateSystem(SystemDto dto)
        {
            return Unit.SystemRepository.UpdateSystem(dto.ToModel<GIS.Authority.Entity.System>());
        }
    }
}