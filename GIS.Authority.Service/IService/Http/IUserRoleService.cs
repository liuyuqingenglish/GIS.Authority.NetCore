using GIS.Authority.Contract;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;
using GIS.Authority.Common;
namespace GIS.Authority.Service
{
    public interface IUserRoleService
    {
        PageResult<UserRoleDto> GetUserRoleDto(PageQueryCondition<ProtocolQueryRole,PageQuery> query);

        bool AddUserRole(UserRoleDto dto);

        bool UpdateUserRole(UserRoleDto dto);

        bool DeleteUserRole(List<Guid> roleId);
    }
}