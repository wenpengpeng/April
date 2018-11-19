using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using April.Core;
using April.EntityFramework;
using April.Uow.UnitOfWorks;
using April.Web.Auditing;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Autofac.Integration.Mvc;
using WebTest.Auditing;

namespace WebTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()

        {

            var bootstrap = AprilBootstrap.GetInstance<WebTestModule>();
            bootstrap.Initiate();

 
            var newBuilder = new ContainerBuilder();

            //注册AuditStore
            //newBuilder.RegisterType<AuditStore>().As<IAuditingStore>();


            #region MVC
            newBuilder.RegisterControllers(Assembly.GetExecutingAssembly()).EnableInterfaceInterceptors();
            newBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();
            newBuilder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            newBuilder.Register(c => new AprilMvcFilterAttribute(c.Resolve<IUnitOfWorkManager>()));//注册全局过滤器 
            #endregion


            bootstrap.IocManager.UpdateContainer(newBuilder);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(bootstrap.IocManager.Container));

            //向全局Filter中添加AprilMvcFilterAttribute
            GlobalFilters.Filters.Add(bootstrap.IocManager.Resolve<AprilMvcFilterAttribute>());



            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
