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
using GIS.Authority.Dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GIS.Authority.Common;
namespace GIS.Authority.Service
{
    public class RolePermissonService : BaseService, IRolePermissonService
    {
        public const string ROLE_ID = "roleid";
        public RolePermissonService(IUnitOfWork unit) : base(unit)
        {
        }

        public List<RoleGroupPermissionDto> GetRolePermissionDto(ProtocolQueryRolePermision query)
        {
            StringBuilder sql = new StringBuilder();
            return Unit.SystemModuleRepository.TransactionResult<RoleGroupPermissionDto>(sql.ToString());
        }

        public bool UpdateRolePermission(RoleGroupPermissionDto dto)
        {
            return Unit.UserRepository.UpdateUserAccount(dto.ToModel<GIS.Authority.Entity.UserAccount>());
        }
    }
}