// 文件名：AprilDbContext.cs
// 
// 创建标识：温朋朋 2018-08-11 16:40
// 
// 修改标识：温朋朋2018-08-11 16:40
// 
// ------------------------------------------------------------------------------
using April.Core.Module;
using April.ServiceStack.Redis.services;
using Autofac;

namespace April.ServiceStack.Redis
{
    public class ServiceStackRedisModule:AprilBaseModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceStackRedisService>().As<IServiceStackService>();
        }
    }
}