// 文件名：AutoMapAttribute.cs
// 
// 创建标识：温朋朋 2018-06-21 9:25
// 
// 修改标识：温朋朋2018-06-21 9:25
// 
// ------------------------------------------------------------------------------

using System;
using AutoMapper;

namespace April.Common.AutoMap
{
    public class AutoMapAttribute:AutoMapAttributeBase
    {
        public AutoMapAttribute(params Type[] targetTypes)
            :base(targetTypes)
        {
            
        }
        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
        {
            if (TargetTypes == null || TargetTypes.Length <= 0)
                return;
            foreach (var targetType in TargetTypes)
            {
                configuration.CreateMap(type,targetType,MemberList.Source);
                configuration.CreateMap(targetType,type,MemberList.Destination);
            }
        }
    }
}