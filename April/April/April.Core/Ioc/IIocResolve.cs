// 文件名：IIocResolve.cs
// 
// 创建标识：温朋朋 2018-05-04 13:43
// 
// 修改标识：温朋朋2018-05-04 13:43
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace April.Core.Ioc
{
    public interface IIocResolve
    {
        /// <summary>
        ///     获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scope"></param>
        /// <returns></returns>
        T Resolve<T>(ILifetimeScope scope=null);
        /// <summary>
        ///     获取实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);
        /// <summary>
        ///     根据name获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        T ResolveName<T>(string name);
        /// <summary>
        ///     获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        T Resolve<T>(IEnumerable<Parameter> parameters,ILifetimeScope scope=null);
        /// <summary>
        ///     获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T ResolveParameter<T>(params Parameter[] parameters);
        /// <summary>
        ///     检查某个类型是否注册过了
        /// </summary>
        /// <typeparam name="T"></typeparam>        
        bool IsRegistered<T>() where T : class;

        /// <summary>
        ///     检查某个类型是否注册过了
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="scope">The ILifetimeScope</param>       
        bool IsRegistered(Type type, ILifetimeScope scope = null);

        /// <summary>
        /// Releases 
        /// </summary>
        /// <param name="obj"></param>
        void Release(object obj);
    }
}