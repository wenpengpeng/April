// 文件名：DomainCoreModule.cs
// 
// 创建标识：温朋朋 2018-05-25 17:27
// 
// 修改标识：温朋朋2018-05-25 17:27
// 
// ------------------------------------------------------------------------------

using April.Core.Module;
using April.Core.Reflection;
using April.Core.Session;
using April.Web.Services;
using Autofac;
using Domain.Core.AprilSessions;

namespace Domain.Core
{    
    public class DomainCoreModule:AprilBaseModule
    {
        private readonly ITypeFinder _typeFinder;

        public DomainCoreModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        protected override void Load(ContainerBuilder builder)
        {            
            builder.RegisterType<AprilSessionExtensions>().As<IAprilSessionExtensions>().PropertiesAutowired();

            //注册Manage
            RegisterManage(builder); 
        }
        /// <summary>
        ///     注册Manage
        /// </summary>
        /// <param name="builder"></param>
        private void RegisterManage(ContainerBuilder builder)
        {
            var manageTypes = _typeFinder.Find(m=>m.IsPublic&&m.IsClass&&!m.IsAbstract
                                                &&typeof(IDomainService).IsAssignableFrom(m));
            foreach (var type in manageTypes)
            {
                builder.RegisterType(type);
            }
        }
    }
}