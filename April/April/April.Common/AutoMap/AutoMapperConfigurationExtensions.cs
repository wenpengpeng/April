// 文件名：AprilAutomapperConfigurationExtensions.cs
// 
// 创建标识：温朋朋 2018-06-21 10:05
// 
// 修改标识：温朋朋2018-06-21 10:05
// 
// ------------------------------------------------------------------------------

using System;
using System.Reflection;
using AutoMapper;

namespace April.Common.AutoMap
{
    public static class AutoMapperConfigurationExtensions
    {
        public static void CreateAutoAttributeMaps(this IMapperConfigurationExpression configuration, Type type)
        {
            foreach (var autoMapAttribute in type.GetTypeInfo().GetCustomAttributes<AutoMapAttributeBase>())
            {
                autoMapAttribute.CreateMap(configuration,type);
            }
        }
    }
}