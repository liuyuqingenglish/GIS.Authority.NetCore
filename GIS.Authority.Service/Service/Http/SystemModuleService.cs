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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GIS.Authority.Dal.UnitOfWork;
using GIS.Authority.Common;
namespace GIS.Authority.Service
{
    public class SystemModuleService : BaseService, ISystemModuleService
    {
        public SystemModuleService(IUnitOfWork unit) : base(unit)
        {
        }

        public List<ModuleDto> GetSystemModuleDto(ProtocolQuerySystemModule query)
        {
            StringBuilder sql = new StringBuilder();
            return Unit.SystemModuleRepository.TransactionResult<ModuleDto>(sql.ToString());
        }

        public bool UpdateSystemModule(ModuleDto dto)
        {
            return Unit.UserRepository.UpdateUserAccount(dto.ToModel<GIS.Authority.Entity.UserAccount>());
        }
    }
}