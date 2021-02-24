using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;

namespace GIS.Authority.Service
{
    public interface IRoleGroupPermissonService
    {
        List<RoleGroupPermissionDto> GetRolePermissionDto(ProtocolQueryRolePermision query);

        bool UpdateRolePermission(RoleGroupPermissionDto dto);
    }
}