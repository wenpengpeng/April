// 文件名：AprilModuleManage.cs
// 
// 创建标识：温朋朋 2018-05-08 15:16
// 
// 修改标识：温朋朋2018-05-08 15:16
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using April.Core.Ioc;
using Autofac;

namespace April.Core.Module
{
    public class AprilModuleManage:IAprilModuleManage
    {
        private readonly IIocManager _iocManager;

        public AprilModuleManage(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }
        /// <summary>
        ///     初始化Module（Register Module）
        /// </summary>
        /// <param name="startupModule"></param>
        public void InitModules(Type startupModule)
        {
            var builder = new ContainerBuilder();
            var moduleTypes = new List<Type> {startupModule};//把自身加入进去
            moduleTypes.AddRange(FindDependedModuleTypes(startupModule));//把startupModule依赖的项加入进去
            moduleTypes.Reverse();//倒序，让自身最后注册
            //循环注册
            foreach (var moduleType in moduleTypes)
            {
                //避免重复注册
                if (!IocManager.Container.IsRegistered(moduleType))
                    builder.RegisterType(moduleType)
                        .Named($"{moduleType}",typeof(AprilBaseModule))
                        .PropertiesAutowired();
            }

            //更新Container
            _iocManager.UpdateContainer(builder);
        }
        /// <summary>
        ///     加载Module（Resolve Module）
        /// </summary>
        /// <param name="startupModule"></param>
        public void LoadModules(Type startupModule)
        {
            var builder = new ContainerBuilder();
            var moduleTypes = new List<Type> { startupModule};
            moduleTypes.AddRange(FindDependedModuleTypes(startupModule));
            moduleTypes.Reverse();
            foreach (var moduleType in moduleTypes)
            {
                //获取类型为moduleType的Module的实例
                var item = IocManager.Container
                        .ResolveNamed($"{moduleType}", typeof(AprilBaseModule))
                    as AprilBaseModule;
                if (item == null)
                    continue;
                item.IocManager = _iocManager;//为各个Module的IocManager属性赋值
                builder.RegisterModule(item);//为每个模块里的具体服务注册
            }
            //更新Container
            _iocManager.UpdateContainer(builder);
        }
        /// <summary>
        ///     获取所有依赖ModuleType不包括自身
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            var list = new List<Type>();
            //如果没有使用DependsOnAttribute则直接退出
            if (!moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
                return list;
            var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependsOnAttribute), true)
                .Cast<DependsOnAttribute>();
            list.AddRange(dependsOnAttributes.SelectMany(d => d.DependsOnModuleTypes));
            return list;
        }
    }
}