// 文件名：AuditingModule.cs
// 
// 创建标识：温朋朋 2018-05-23 14:25
// 
// 修改标识：温朋朋2018-05-23 14:25
// 
// ------------------------------------------------------------------------------

using System.Linq;
using System.Reflection;
using April.Core;
using April.Core.Module;
using April.Core.Reflection;
using April.EntityFramework;
using April.Uow;
using April.Web.Auditing;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;

namespace April.Web
{
    [DependsOn(typeof(AprilCoreModule),typeof(EntityFrameworkModule))]
    public class WebModule:AprilBaseModule
    {
        private readonly ITypeFinder _typeFinder;

        public WebModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuditingInterceptor>();
            //builder.RegisterType<AuditingInterceptor>()
            //    .Named<IInterceptor>("AuditingInterceptor")
            //    .InstancePerLifetimeScope();
            builder.RegisterType<AuditingSelectorList>().As<IAuditingSelectorList>();
            builder.RegisterType<JsonnetAuditSerializer>().As<IAuditSerializer>();
            builder.RegisterType<AuditingConfiguration>().As<IAuditingConfiguration>();
            builder.RegisterType<WebAuditInfoProvider>().As<IClientInfoProvider>();
            builder.RegisterType<AprilAuditInfoProvider>().As<IAuditInfoProvider>();
            builder.RegisterType<AuditHelper>().As<IAuditingHelper>();
            //builder.RegisterType<ThrowException>().EnableClassInterceptors();
            

            //审核拦截注册  
            //对于以类方式的注入，Autofac Interceptor 要求类的方法为必须为 virtual 方法。
            //值得注意的是：对于 子类，重写（override）父类的虚方法时，能应用到拦截器。
            //父类可在 IoC 中注册也可不需要注册，但子类必须在 IoC 中注册（对于类的拦截器，类都必须要注册，当然，拦截器也必须要注册）。
            //这就有点鬼扯了 所以还是全部采用EnableInterfaceInterceptors接口方式，想通过AuditedAttribute方式实现审计则必须通过接口注册方式
            AuditingInterceptRegister();
        }
        /// <summary>
        ///     审核拦截注册
        /// </summary>
        private void AuditingInterceptRegister()
        {
            if (IocManager == null)
                return;            
            //获取需要审核拦截注册的类型（实现IApplicationService接口的服务类先不包含在里面，后面单独注入）
            var types = _typeFinder.Find(x => x.IsPublic && x.IsClass && !x.IsAbstract                                              
                                              &&(x.GetTypeInfo().IsDefined(typeof(AuditedAttribute),true)
                                              ||x.GetMethods().Any(s=>s.IsDefined(typeof(AuditedAttribute),true))));
            var builder = new ContainerBuilder();
            foreach (var type in types)
            {
                builder.RegisterType(type).AsImplementedInterfaces().EnableInterfaceInterceptors().InterceptedBy(typeof(AuditingInterceptor));
            }
            IocManager.UpdateContainer(builder);
        }
    }
}