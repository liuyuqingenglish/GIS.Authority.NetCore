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
using GIS.Authority.Dal;
using GIS.Authority.Dal.UnitOfWork;
using GIS.Authority.Common;
namespace GIS.Authority.Service
{
    /// <summary>
    /// adfsfd
    /// </summary>
    public class DepartmentService : BaseService, IDepartmentService
    {
        public DepartmentService(IUnitOfWork unit) : base(unit)
        {
        }

        public bool AddDepartment(DepartmentDto dto)
        {
            return Unit.DepartmentRepository.AddDepartment(dto.ToModel<Department>());
        }

        public bool DeleteDepartment(Guid depid)
        {
            PredicateGroup group = new PredicateGroup();
            group.Predicates.Add(Predicates.Field<Department>(d => d.Id, Operator.Eq, depid));
            return Unit.DepartmentRepository.Delete(group);
        }

        public PageResult<DepartmentDto> GetDepartmentDto(PageQueryCondition<ProtocolQueryDepartment,PageQuery> query)
        {
            PredicateGroup group = new PredicateGroup();
            if (!string.IsNullOrEmpty(query.Condition.DepartmentId))
            {
                group.Predicates.Add(Predicates.Field<Department>(d => d.Id, Operator.Eq, query.Condition.DepartmentId));
            }
            if (!string.IsNullOrEmpty(query.Condition.OrganizeId))
            {
                group.Predicates.Add(Predicates.Field<Department>(d => d.OrganizationId, Operator.Eq, query.Condition.OrganizeId));
            }
            if (!string.IsNullOrEmpty(query.Condition.DepartmentName))
            {
                group.Predicates.Add(Predicates.Field<Department>(d => d.Name, Operator.Eq, query.Condition.DepartmentName));
            }
            return Unit.DepartmentRepository.GetDepartment(group, query.Query).ToPageModel<Department, DepartmentDto>();
        }

        public List<DepartmentDto> GetDepartmentDto()
        {
            return Unit.DepartmentRepository.GetDepartmentList().ToListDto<Department,DepartmentDto>();
        }

        public bool UpdateDepartment(DepartmentDto dto)
        {
            return Unit.DepartmentRepository.UpdateDepartment(dto.ToModel<Department>());
        }
    }
}