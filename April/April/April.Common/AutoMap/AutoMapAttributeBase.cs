// 文件名：AutoMapAttributeBase.cs
// 
// 创建标识：温朋朋 2018-06-21 9:21
// 
// 修改标识：温朋朋2018-06-21 9:21
// 
// ------------------------------------------------------------------------------

using System;
using AutoMapper;

namespace April.Common.AutoMap
{
    public abstract class AutoMapAttributeBase : Attribute
    {
        public Type[] TargetTypes { get; private set; }

        protected AutoMapAttributeBase(params Type[] targetTypes)
        {
            TargetTypes = targetTypes;
        }

        public abstract void CreateMap(IMapperConfigurationExpression configuration, Type type);
    }
}