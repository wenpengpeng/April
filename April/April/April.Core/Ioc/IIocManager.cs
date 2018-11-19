// 文件名：IIocManager.cs
// 
// 创建标识：温朋朋 2018-05-04 9:16
// 
// 修改标识：温朋朋2018-05-04 9:16
// 
// ------------------------------------------------------------------------------

using System;
using Autofac;
using CommonServiceLocator;

namespace April.Core.Ioc
{
    public interface IIocManager:IIocRegistrar,IIocResolve
    {
        /// <summary>
        ///     Autofac容器
        /// </summary>
        IContainer Container { get; set; }
        /// <summary>
        ///     ServiceLocatorCurrent
        /// </summary>
        IServiceLocator ServiceLocatorCurrent { get; set; }
        /// <summary>
        ///     设置ContainerBuilder
        /// </summary>
        /// <param name="builder"></param>
        void SetContainer(ContainerBuilder builder);
        /// <summary>
        ///     更新ContainerBuilder
        /// </summary>
        /// <param name="builder"></param>
        void UpdateContainer(ContainerBuilder builder);
    }
}