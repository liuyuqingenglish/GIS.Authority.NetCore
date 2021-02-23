using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;
using GIS.Authority.Common;
namespace GIS.Authority.Service
{
    public interface IRoleService
    {
        PageResult<RoleDto> GetRoleDto(PageQueryCondition<ProtocolQueryRole,PageQuery> query);

        bool AddRole(RoleDto dto);

        bool UpdateRole(RoleDto dto);

        bool DeleteRole(List<Guid> roleId);
    }
}