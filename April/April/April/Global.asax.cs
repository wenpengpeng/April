using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using April.Common.Json;
using April.Core;
using April.EntityFramework;
using April.Uow.UnitOfWorks;
using April.Web.Auditing;
using AprilWebApi;
using AprilWebApi.Auditing;
using AprilWebApi.Uow;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;

namespace April
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var bootstrap = AprilBootstrap.GetInstance<AprilWebModule>();
            bootstrap.Initiate();


            var newBuilder = new ContainerBuilder();
            

            #region WebApi

            newBuilder.RegisterApiControllers(Assembly.Load("AprilWebApi")).PropertiesAutowired();
            newBuilder.Register(c => new AprilWebApiUowFilter(c.Resolve<IUnitOfWorkManager>()));//工作单元
            newBuilder.Register(c => new AprilWebApiFilter(c.Resolve<IAuditingHelper>()));//审计           
            newBuilder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            newBuilder.RegisterWebApiModelBinderProvider();
            #endregion


            #region MVC
            newBuilder.RegisterControllers(Assembly.GetExecutingAssembly());
            newBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();
            newBuilder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            newBuilder.Register(c => new AprilMvcFilterAttribute(c.Resolve<IUnitOfWorkManager>()));//注册全局过滤器
            newBuilder.Register(c => new JsonNetActionFilter());//注册json全局过滤器
            newBuilder.Register(c => new AprilMvcAuditFilter(c.Resolve<IAuditingHelper>()));//注册审计全局过滤器
            #endregion


            bootstrap.IocManager.UpdateContainer(newBuilder);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(bootstrap.IocManager.Container));//mvc
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(bootstrap.IocManager.Container);//webApi


            //向全局Filter中添加AprilMvcFilterAttribute
            GlobalFilters.Filters.Add(bootstrap.IocManager.Resolve<AprilMvcFilterAttribute>());
            GlobalFilters.Filters.Add(bootstrap.IocManager.Resolve<JsonNetActionFilter>());
            GlobalFilters.Filters.Add(bootstrap.IocManager.Resolve<AprilMvcAuditFilter>());

            //WebApi Filter
            GlobalConfiguration.Configuration.Filters.Add(bootstrap.IocManager.Resolve<AprilWebApiUowFilter>());
            GlobalConfiguration.Configuration.Filters.Add(bootstrap.IocManager.Resolve<AprilWebApiFilter>());
           
             
            //初始化WebApi
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Formatters.FormUrlEncodedFormatter.SupportedMediaTypes.Clear();//清除所有默认支持的type，统一只保留json
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;//忽略循环引用
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();//json中属性开头字母小写的驼峰命名
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";//时间格式

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
