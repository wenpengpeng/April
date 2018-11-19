// 文件名：DomainEfModule.cs
// 
// 创建标识：温朋朋 2018-06-01 15:16
// 
// 修改标识：温朋朋2018-06-01 15:16
// 
// ------------------------------------------------------------------------------
using April.Core.Module;
using April.Core.Reflection;
using Autofac;
using Domain.Core.Repositories;
using Domain.EntityFramework.Repositories;

namespace Domain.EntityFramework
{
    public class DomainEfModule:AprilBaseModule
    {        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(AprilRepository<,>)).As(typeof(IAprilRepository<,>));
        }
    }
}