// 文件名：WebTestModule.cs
// 
// 创建标识：温朋朋 2018-05-16 14:40
// 
// 修改标识：温朋朋2018-05-16 14:40
// 
// ------------------------------------------------------------------------------

using System.Reflection;
using System.Web.Mvc;
using April.Core.Module;
using April.EntityFramework;
using April.Uow;
using April.Uow.Repositories;
using April.Web;
using April.Web.Auditing;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Autofac.Integration.Mvc;
using Castle.DynamicProxy;
using WebTest.Auditing;
using WebTest.Entities;

namespace WebTest
{
    [DependsOn(typeof(EntityFrameworkModule),
        typeof(AprilUowModule),
        typeof(WebModule))]
    public class WebTestModule:AprilBaseModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuditingInterceptor>()
                .Named<IInterceptor>
                ("AuditingInterceptor")
                .InstancePerLifetimeScope();

            builder.Register(c => new WebTestDbContext("WebTest"));
            builder.RegisterType<AuditStore>().As<IAuditingStore>();

            builder.RegisterType<ThrowException>()
                .EnableInterfaceInterceptors()
                .As<IThrowException>();
            //builder.RegisterType<BaseRepository<Person, long>>().As<IBaseRepository<Person, long>>().PropertiesAutowired();           
        }
    }
}