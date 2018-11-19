// 文件名：IIocRegistrar.cs
// 
// 创建标识：温朋朋 2018-05-04 11:19
// 
// 修改标识：温朋朋2018-05-04 11:19
// 
// ------------------------------------------------------------------------------

using System;
using System.Reflection;
using Autofac;

namespace April.Core.Ioc
{
    public interface IIocRegistrar
    {
        /// <summary>
        ///     Autofac ContainerBuilder
        /// </summary>
        ContainerBuilder ContainerBuilder { get; set; }
    }
}