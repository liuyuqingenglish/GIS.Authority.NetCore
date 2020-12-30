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
using GIS.Authority.Entity;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GIS.Authority.Dal.Base.BaseDal;
namespace GIS.Authority.Dal
{
    public class ModuleRepository : BasicRepository<Module>, IModuleRepository
    {
        public bool AddModule(Module mod)
        {
            throw new NotImplementedException();
        }

        public bool DeleteModule(PredicateGroup group)
        {
            throw new NotImplementedException();
        }

        public List<Module> GetModule(PredicateGroup group, PageQuery query)
        {
            return base.GetList().ToList();
        }

        public bool UpdateModule(Module mod)
        {
            throw new NotImplementedException();
        }
    }
}