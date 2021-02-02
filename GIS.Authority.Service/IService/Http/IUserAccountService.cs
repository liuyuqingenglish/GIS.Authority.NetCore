using GIS.Authority.Entity;
using GIS.Authority.Contract;
using System;
using System.Collections.Generic;

namespace GIS.Authority.Service
{
    public interface IUserAccountService
    {
        List<UserAccountDto> GetUserAccountDto(ProtocolQueryUserAccount query);

        bool AddUserAccount(UserAccountDto dto);

        bool UpdateUserAccount(UserAccountDto dto);

        bool DeleteUserAccount(List<Guid> userId);
        UserAccountDto GetUserDto(string account, string password);

        List<RoleGroupPermissionDto> GetUserRolePermission(Guid userId);
    }
}