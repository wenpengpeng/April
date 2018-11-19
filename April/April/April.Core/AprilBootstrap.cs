// 文件名：AprilBootstrap.cs
// 
// 创建标识：温朋朋 2018-05-09 9:54
// 
// 修改标识：温朋朋2018-05-09 9:54
// 
// ------------------------------------------------------------------------------

using System;
using April.Core.Ioc;
using April.Core.Module;
using Autofac;

namespace April.Core
{
    /// <summary>
    ///     初始化装载程序
    /// </summary>
    public class AprilBootstrap
    {
        /// <summary>
        ///     是否初始化
        /// </summary>
        private static bool _isInit;
        /// <summary>
        ///     IocManager
        /// </summary>
        public IIocManager IocManager { get; set; }
        /// <summary>
        ///     开始Module（即指入口程序的Module）
        /// </summary>
        public Type StartupModule { get; set; }
        /// <summary>
        ///     构造函数(默认传入单例IocManager)
        /// </summary>
        public AprilBootstrap():this(Ioc.IocManager.Instance)
        {
            
        }
        /// <summary>
        ///     构造函数(默认传入单例IocManager)
        /// </summary>
        /// <param name="startupModule"></param>
        public AprilBootstrap(Type startupModule):this(startupModule,Ioc.IocManager.Instance)
        {
            
        }
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="iocManager"></param>
        public AprilBootstrap(IIocManager iocManager)
        {
            IocManager = iocManager;
        }
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="startupModule"></param>
        /// <param name="iocManager"></param>
        public AprilBootstrap(Type startupModule, IIocManager iocManager)
        {
            StartupModule = startupModule;
            IocManager = iocManager;
        }
        /// <summary>
        ///     获取AprilBootstrap实例
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <returns></returns>
        public static AprilBootstrap GetInstance<TStartupModule>() where TStartupModule : AprilBaseModule
        {
            return new AprilBootstrap(typeof(TStartupModule));
        }
        /// <summary>
        ///     获取AprilBootstrap实例
        /// </summary>
        /// <returns></returns>
        public static AprilBootstrap GetInstance()
        {
            return new AprilBootstrap();
        }
        /// <summary>
        ///     初始化方法
        /// </summary>
        public void Initiate()
        {
            if (_isInit)
                return;

            var builder = new ContainerBuilder();
            //注册AprilCore层Module(采用传入一个实例方式注册)
            builder.RegisterModule(new AprilCoreModule());
            IocManager.SetContainer(builder);//初始化builder和container

            //获取AprilModuleManage实例并Register和Resolve相关Module
            var manage = IocManager.Resolve<IAprilModuleManage>();
            manage.InitModules(StartupModule);
            manage.LoadModules(StartupModule);

            _isInit = true;
        }
    }
}