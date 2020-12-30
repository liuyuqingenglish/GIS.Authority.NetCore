/*
* ==============================================================================
*
* Filename: DepartmentRepository
* ClrVersion: 4.0.30319.42000
* Description: DepartmentRepository
*
* Version: 1.0
* Created: 2019/12/26 17:21:49
* Compiler: Visual Studio 2017
*
* Author: liuyuqing
* Copyright: 广东满天星云信息技术有限公司
*
* ==============================================================================
*/

using DapperExtensions;
using GIS.Authority.Common;
using GIS.Authority.Dal.Base.BaseDal;
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;

namespace GIS.Authority.Dal
{
    public class SystemModuleRepository : BasicRepository<SystemModule>, ISystemModuleRepository
    {
        public bool AddSystemModule(SystemModule module)
        {
            return base.Insert(module);
        }

        public bool DeleteSystemModule(PredicateGroup group)
        {
            throw new NotImplementedException();
        }

        public List<Entity.System> GetSystemModule(PredicateGroup group, PageQuery query)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSystemModule(SystemModule module)
        {
            throw new NotImplementedException();
        }
    }
}