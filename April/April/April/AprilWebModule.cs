// 文件名：AprilWebModule.cs
// 
// 创建标识：温朋朋 2018-05-30 14:31
// 
// 修改标识：温朋朋2018-05-30 14:31
// 
// ------------------------------------------------------------------------------

using April.Application;
using April.Common;
using April.Core.Module;
using April.EntityFramework;
using April.Uow;
using April.Web;
using April.Web.Auditing;
using Autofac;
using Castle.DynamicProxy;
using Domain.Core;
using Domain.Core.Auditings;
using Domain.EntityFramework;

namespace April
{
    [DependsOn(typeof(EntityFrameworkModule),
        typeof(AprilUowModule),
        typeof(WebModule),
        typeof(DomainCoreModule),
        typeof(DomainEfModule),
        typeof(AprilCommonModule),
        typeof(AprilApplicationModule))]
    public class AprilWebModule: AprilBaseModule
    {
        protected override void Load(ContainerBuilder builder)
        {            
            builder.RegisterType<AuditStore>().As<IAuditingStore>();
        }
    }
}