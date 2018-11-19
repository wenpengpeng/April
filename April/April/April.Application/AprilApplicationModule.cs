// 文件名：AprilApplicationModule.cs
// 
// 创建标识：温朋朋 2018-06-21 17:43
// 
// 修改标识：温朋朋2018-06-21 17:43
// 
// ------------------------------------------------------------------------------

using System.Reflection;
using April.Application.Authorizations;
using April.Core;
using April.Core.Module;
using April.Web.Auditing;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Domain.Core;

namespace April.Application
{
    [DependsOn(typeof(AprilCoreModule),
        typeof(DomainCoreModule))]
    public class AprilApplicationModule:AprilBaseModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ApplicationServiceIntercept());
            var assembly = Assembly.Load("April.Application");
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("AppService"))
                .AsImplementedInterfaces().InterceptedBy(typeof(ApplicationServiceIntercept)).EnableInterfaceInterceptors();
        }
    }
}