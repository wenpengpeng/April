// 文件名：AprilCoreModule.cs
// 
// 创建标识：温朋朋 2018-05-09 9:46
// 
// 修改标识：温朋朋2018-05-09 9:46
// 
// ------------------------------------------------------------------------------

using April.Core.Ioc;
using April.Core.Module;
using April.Core.Reflection;
using April.Core.Session;
using Autofac;

namespace April.Core
{
    public class AprilCoreModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IocManager>().As<IIocManager, IIocRegistrar, IIocResolve>().PropertiesAutowired();//注册IocManager
            builder.Register(c => new AprilModuleManage(c.Resolve<IIocManager>()))
                .As<IAprilModuleManage>();//注册AprilModuleManage

            builder.RegisterType<HttpContextPrincipalAccessor>().As<IPrincipalAccessor>();
            builder.RegisterType<ClaimsAprilSession>().As<IAprilSession>().PropertiesAutowired();
            //注册AssemblyFinder和TypeFinder
            builder.RegisterType<AssemblyFinder>().As<IAssemblyFinder>().PropertiesAutowired();
            builder.RegisterType<TypeFinder>().As<ITypeFinder>().PropertiesAutowired();
        }
    }
}