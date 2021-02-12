using GIS.Authority.Entity;
using GIS.Authority.Contract;
using System;
using System.Collections.Generic;
using GIS.Authority.Common;
namespace GIS.Authority.Service
{
    public interface IUserAccountService
    {
        PageResult<UserAccountDto> GetUserAccountDto(PageQueryCondition<ProtocolQueryUserAccount> query);

        bool AddUserAccount(UserAccountDto dto);

        bool UpdateUserAccount(UserAccountDto dto);

        bool DeleteUserAccount(List<Guid> userId);
        UserAccountDto GetUserDto(string account, string password);

        List<RoleGroupPermissionDto> GetUserRolePermission(Guid userId);
    }
}