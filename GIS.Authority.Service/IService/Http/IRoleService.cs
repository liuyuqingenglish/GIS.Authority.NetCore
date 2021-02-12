using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;

namespace GIS.Authority.Service
{
    public interface IRoleService
    {
        List<RoleDto> GetRoleDto(ProtocolQueryRole query);

        bool AddRole(RoleDto dto);

        bool UpdateRole(RoleDto dto);

        bool DeleteRole(List<Guid> roleId);
    }
}