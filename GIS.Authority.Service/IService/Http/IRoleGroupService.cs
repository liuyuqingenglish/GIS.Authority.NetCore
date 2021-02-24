using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;
using GIS.Authority.Common;
namespace GIS.Authority.Service
{
    public interface IRoleGroupService
    {
        PageResult<RoleDto> GetRoleGroupDto(PageQueryCondition<ProtocolQueryRole,PageQuery> query);

        bool AddRoleGroup(RoleDto dto);

        bool UpdateRoleGroup(RoleDto dto);

        bool DeleteRoleGroup(List<Guid> roleId);
    }
}