// 文件名：TestModule.cs
// 
// 创建标识：温朋朋 2018-05-09 10:50
// 
// 修改标识：温朋朋2018-05-09 10:50
// 
// ------------------------------------------------------------------------------

using April.Core;
using April.Core.Ioc;
using April.Core.Module;
using April.EntityFramework;
using April.Uow;
using April.Web;
using Autofac;

namespace IocTest
{  
    [DependsOn(typeof(WebModule),typeof(EntityFrameworkModule),typeof(AprilUowModule))]
    public class TestModule:AprilBaseModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Person>().As<IPerson>();
        }
    }
}