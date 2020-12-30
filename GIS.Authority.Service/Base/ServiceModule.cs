using Autofac;
using System.Reflection;
using GIS.Authority.Dal.Base;
using Module = Autofac.Module;

namespace GIS.Authority.Service
{
    /// <summary>
    /// IOC注册服务
    /// </summary>
    public class ServiceModule : Module
    {
        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="builder">builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
             .Where(t => t.Name.EndsWith("Service"))
             .AsImplementedInterfaces().SingleInstance();
            builder.InjectRepository();
        }
    }
}