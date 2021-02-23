using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;
using GIS.Authority.Common;
namespace GIS.Authority.Service
{
    public interface IDepartmentService
    {
        PageResult<DepartmentDto> GetDepartmentDto( PageQueryCondition<ProtocolQueryDepartment,PageQuery> query);

        bool AddDepartment(DepartmentDto dto);

        bool UpdateDepartment(DepartmentDto dto);

        bool DeleteDepartment(Guid depid);

        List<DepartmentDto> GetDepartmentDto();
    }
}