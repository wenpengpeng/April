// 文件名：IocManager.cs
// 
// 创建标识：温朋朋 2018-05-04 9:28
// 
// 修改标识：温朋朋2018-05-04 9:28
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;

namespace April.Core.Ioc
{
    /// <summary>
    ///     Autofac依赖注入管理类
    /// </summary>
    public class IocManager:IIocManager
    {
        /// <summary>
        ///     注册器
        /// </summary>
        public static ContainerBuilder ContainerBuilder { get; set; }
        /// <summary>
        ///     autofac容器
        /// </summary>
        public static IContainer Container { get; set; }

        IContainer IIocManager.Container
        {
            get => Container;
            set => Container = value;
        }

        IServiceLocator IIocManager.ServiceLocatorCurrent
        {
            get => ServiceLocatorCurrent;
            set => ServiceLocatorCurrent = value;
        }
        /// <summary>
        ///     单例IocManager实例
        /// </summary>
        public static IocManager Instance { get; }
        /// <summary>
        ///     静态构造函数
        /// </summary>
        static IocManager()
        {
            Instance = new IocManager();
        }

        /// <summary>
        ///     ServiceLocator
        /// </summary>
        public static IServiceLocator ServiceLocatorCurrent { get; set; }
        /// <summary>
        ///     获取实例（保证开启了一个生命周期）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scope"></param>
        /// <returns></returns>
        public T Resolve<T>(ILifetimeScope scope = null)
        {
            if (scope == null)
                scope=Scope();
            return scope.Resolve<T>();
        }
        /// <summary>
        ///     获取实例（内部自动开启一个实例）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            var scope = Scope();
            return scope.Resolve(type);
        }
        /// <summary>
        ///     根据名称获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T ResolveName<T>(string name)
        {
            var scope = Scope();
            return scope.ResolveNamed<T>(name);
        }
        /// <summary>
        ///     获取含参数的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public T Resolve<T>(IEnumerable<Parameter> parameters, ILifetimeScope scope = null)
        {
            if (scope == null)
                scope = Scope();
            return scope.Resolve<T>(parameters);
        }
        /// <summary>
        ///     获取含参数的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T ResolveParameter<T>(params Parameter[] parameters)
        {
            var scope = Scope();
            return scope.Resolve<T>(parameters);
        }
        /// <summary>
        ///     判定某个类型是否注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsRegistered<T>() where T : class
        {
            return IsRegistered(typeof(T));
        }
        /// <summary>
        ///     判定某个类型是否注册
        /// </summary>
        /// <param name="type"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public bool IsRegistered(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
                scope = Scope();
            return scope.IsRegistered(type);
        }

        public void Release(object obj)
        {
            
        }
        /// <summary>
        ///     设置Container
        /// </summary>
        /// <param name="builder"></param>
        public void SetContainer(ContainerBuilder builder)
        {
            ContainerBuilder = builder;
            Container = builder.Build();

            //TODO:后面搞清楚什么东东
            //设置定位器
            ServiceLocatorCurrent = new AutofacServiceLocator(Container);
        }
        /// <summary>
        ///     更新Container
        /// </summary>
        /// <param name="builder"></param>
        public void UpdateContainer(ContainerBuilder builder)
        {
            ContainerBuilder = builder;
            builder.Update(Container);
        }
        /// <summary>
        ///     开启一个生命周期
        /// </summary>
        /// <returns></returns>
        private static ILifetimeScope Scope()
        {
            return Container.BeginLifetimeScope();
        }

        ContainerBuilder IIocRegistrar.ContainerBuilder
        {
            get => ContainerBuilder;
            set => ContainerBuilder = value;
        }
    }
}