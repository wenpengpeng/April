using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using April.Core;
using April.Core.Ioc;
using April.Web;
using April.Web.Auditing;
using Autofac;
using Autofac.Extras.DynamicProxy;
using WebTest;
using WebTest.Auditing;

namespace IocTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var bootstrap = AprilBootstrap.GetInstance<TestModule>();
            //bootstrap.Initiate();
            //var builder = new ContainerBuilder();
            //builder.RegisterType<AuditStore>().As<IAuditingStore>();
            ////builder.RegisterType<ThrowException>().EnableClassInterceptors().InterceptedBy(typeof(AuditingInterceptor));
            //builder.RegisterType<ThrowExceptionChild>().AsImplementedInterfaces().EnableInterfaceInterceptors().InterceptedBy(typeof(AuditingInterceptor));
            //bootstrap.IocManager.UpdateContainer(builder);
            //var thro = bootstrap.IocManager.Resolve<IThrowException>();
            //thro.Throw();
            Console.ReadKey();
        }
    }
}
