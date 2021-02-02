using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;

namespace GIS.Authority.Service
{
    public interface IDepartmentService
    {
        List<DepartmentDto> GetDepartmentDto(ProtocolQueryDepartment query);

        bool AddDepartment(DepartmentDto dto);

        bool UpdateDepartment(DepartmentDto dto);

        bool DeleteDepartment(Guid depid);

        List<DepartmentDto> GetDepartmentDto();
    }
}