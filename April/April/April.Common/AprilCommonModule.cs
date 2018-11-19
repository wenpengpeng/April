// 文件名：AprilCommonModule.cs
// 
// 创建标识：温朋朋 2018-06-21 10:11
// 
// 修改标识：温朋朋2018-06-21 10:11
// 
// ------------------------------------------------------------------------------

using System;
using System.Reflection;
using April.Common.AutoMap;
using April.Core;
using April.Core.Module;
using April.Core.Reflection;
using Autofac;
using AutoMapper;

namespace April.Common
{
    [DependsOn(typeof(AprilCoreModule))]
    public class AprilCommonModule:AprilBaseModule
    {
        private readonly ITypeFinder _typeFinder;

        private static volatile bool _createdMappingsBefore;
        private static readonly object SyncObj = new object();

        public AprilCommonModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //AutoMapper            
            CreateMappings(builder);

        }

        private void CreateMappings(ContainerBuilder builder)
        {
            lock (SyncObj)
            {
                Action<IMapperConfigurationExpression> configurer = FindAllAutoMapTypes;
                if (!_createdMappingsBefore)
                {
                    Mapper.Initialize(configurer);
                    _createdMappingsBefore = true;
                }
                builder.Register(c => Mapper.Configuration).As<IConfigurationProvider>();
                builder.Register(c => Mapper.Instance).As<IMapper>().SingleInstance();
            }           
        }
        /// <summary>
        ///     找到程序中定义了AutoMapAttribute、AutoMapFromAttribute、AutoMapToAttribute的type
        /// </summary>
        /// <param name="configuration"></param>
        private void FindAllAutoMapTypes(IMapperConfigurationExpression configuration)
        {
            var types = _typeFinder.Find(type =>
            {
                var typeInfo = type.GetTypeInfo();
                return typeInfo.IsDefined(typeof(AutoMapAttribute))||
                       typeInfo.IsDefined(typeof(AutoMapFromAttribute)) ||
                       typeInfo.IsDefined(typeof(AutoMapToAttribute));
            });

            foreach (var type in types)
            {                
                configuration.CreateAutoAttributeMaps(type);
            }
        }
    }
}