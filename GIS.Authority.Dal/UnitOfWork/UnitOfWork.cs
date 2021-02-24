/*
* ==============================================================================
*
* Filename: UnitOfWork
* ClrVersion: 4.0.30319.42000
* Description: 工作单元
*
* Version: 1.0
* Created: 2020/3/31 21:13:29
* Compiler: Visual Studio 2017
*
* Author: lyq
* Copyright: lyq
*
* ==============================================================================
*/

using Autofac;
using GIS.Authority.Dal.IBll;

namespace GIS.Authority.Dal.UnitOfWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    internal class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 请求线程
        /// </summary>
        private readonly ILifetimeScope _scope;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="scope">scope</param>
        public UnitOfWork(ILifetimeScope scope)
        {
            _scope = scope;
        }

        /// <summary>
        /// 报警记录数据库对象
        /// </summary>
        public IImeiRepository ImeaRepository => ResolveRepository<IImeiRepository>();

        /// <summary>
        /// 报警记录数据库对象
        /// </summary>
        public ICheckRecordRepository CheckRecordRepository => ResolveRepository<ICheckRecordRepository>();

        /// <summary>
        /// 报警记录数据库对象
        /// </summary>
        public IEirInfoRepository EirInfoRepository => ResolveRepository<IEirInfoRepository>();


        public IDepartmentRepository DepartmentRepository => ResolveRepository<IDepartmentRepository>();

        public IOrganizeRepository OrganizeRepositiry => ResolveRepository<IOrganizeRepository>();

        public IUserRepository UserRepository => ResolveRepository<IUserRepository>();

        public IUserRoleRepository UserRoleGroupRepository => ResolveRepository<IUserRoleRepository>();

        public ISystemRepository SystemRepository => ResolveRepository<ISystemRepository>();

        public ISystemModuleRepository SystemModuleRepository => ResolveRepository<ISystemModuleRepository>();

        public ISystemConfigRepository SystemConfigRepository => ResolveRepository<ISystemConfigRepository>();

        public IRoleRepository RoleRepository => ResolveRepository<IRoleRepository>();

        public IModuleRepository ModuleRepository => ResolveRepository<IModuleRepository>();

        public IRoleGroupPerssionRepository RolePerssionRepository => ResolveRepository<IRoleGroupPerssionRepository>();


        /// <summary>
        /// 获取仓储
        /// </summary>
        /// <typeparam name="TRepository">仓储泛型</typeparam>
        /// <returns>仓储实例</returns>
        private TRepository ResolveRepository<TRepository>()
            where TRepository : class
        {
            var repository = _scope.Resolve<TRepository>();
            return repository;
        }
    }
}