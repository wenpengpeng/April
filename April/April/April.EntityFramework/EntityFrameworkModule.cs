// 文件名：EntityFrameworkModule.cs
// 
// 创建标识：温朋朋 2018-05-16 10:28
// 
// 修改标识：温朋朋2018-05-16 10:28
// 
// ------------------------------------------------------------------------------

using April.Core;
using April.Core.Module;
using April.Core.Reflection;
using April.EntityFramework.Repositories;
using April.EntityFramework.UnitOfWorks;
using April.Uow;
using April.Uow.Extensions;
using April.Uow.Repositories;
using April.Uow.UnitOfWorks;
using Autofac;
using Autofac.Extras.DynamicProxy;

namespace April.EntityFramework
{
    [DependsOn(typeof(AprilUowModule),typeof(AprilCoreModule))]
    public class EntityFrameworkModule:AprilBaseModule
    {
        private readonly ITypeFinder _typeFinder;

        public EntityFrameworkModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(UnitOfWorkDbContextProvider<>)).As(typeof(IDbContextProvider<>));
            builder.RegisterType<EfTransactionStrategy>().As<IEfTransactionStrategy>();
            builder.RegisterType<EfUnitOfWork>().As<IUnitOfWork, IActiveUnitOfWork, IUnitOfWorkCompleteHandle>();
            

            RegisterMatchDbContext();
        }
        /// <summary>
        ///     注册匹配的DbContext
        /// </summary>
        private void RegisterMatchDbContext()
        {
            if (IocManager == null)
                return;
            var types = _typeFinder.Find(x => x.IsPublic && x.IsClass && !x.IsAbstract
                                              && typeof(AprilDbContext).IsAssignableFrom(x));
            var build = new ContainerBuilder();
            foreach (var type in types)
            {
                build.RegisterType(type);//register DbContext自身
                var entityTypeInfos = DbContextHelper.GetEntityTypeInfos(type);
                foreach (var entityTypeInfo in entityTypeInfos)
                {
                    var primaryKeyType = EntityHelper.GetPrimaryKeyType(entityTypeInfo.EntityType);
                    var genericRepositoryType = typeof(IBaseRepository<,>).MakeGenericType(entityTypeInfo.EntityType,primaryKeyType);

                    var baseImplType = typeof(EfRepositoryBase<,,>);
                    var implType = baseImplType.MakeGenericType(entityTypeInfo.DeclaringType,entityTypeInfo.EntityType,primaryKeyType);

                    build.RegisterType(implType).As(genericRepositoryType).PropertiesAutowired()
                     .InterceptedBy(typeof(UnitOfWorkInterceptor)).EnableInterfaceInterceptors();//动态注入拦截器 //还应该在Controller上通过全局filter实现AOP
                    //build.RegisterType(implType).InterceptedBy(typeof(UnitOfWorkInterceptor)).EnableInterfaceInterceptors();//动态注入拦截器
                }
            }
            IocManager.UpdateContainer(build);
        }
    }
}