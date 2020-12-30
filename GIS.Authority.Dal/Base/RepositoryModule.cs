using Autofac;
using GIS.Authority.Dal.Base.BaseDal;
using GIS.Authority.Dal.Base.IBaseDal;
using GIS.Authority.Dal.UnitOfWork;

namespace GIS.Authority.Dal.Base
{
    /// <summary>
    /// IOC注册仓储
    /// </summary>
    public static class RepositoryModule
    {
        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="builder">builder</param>
        public static void InjectRepository(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(System.Reflection.Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            builder.RegisterGeneric(typeof(BasicRepository<>)).As(typeof(IBasicRepository<>));
            builder.RegisterType<UnitOfWork.UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        }
    }
}