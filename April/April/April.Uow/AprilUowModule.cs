// 文件名：AprilUowModule.cs
// 
// 创建标识：温朋朋 2018-05-10 14:48
// 
// 修改标识：温朋朋2018-05-10 14:48
// 
// ------------------------------------------------------------------------------

using April.Core.Module;
using April.Uow.Repositories;
using April.Uow.UnitOfWorks;
using Autofac;

namespace April.Uow
{
    public class AprilUowModule:AprilBaseModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWorkBase>()
                .As<IActiveUnitOfWork, IUnitOfWorkCompleteHandle, IUnitOfWork>();
            builder.RegisterType<CurrentUnitOfWorkProvider>()
                .As<ICurrentUnitOfWorkProvider>();
            
            builder.RegisterType<UnitOfWorkManager>().As<IUnitOfWorkManager>();
            builder.RegisterType<UnitOfWorkInterceptor>().InstancePerLifetimeScope();            
            builder.RegisterGeneric(typeof(BaseRepository<,>)).As(typeof(IBaseRepository<,>));            
        }
    }
}