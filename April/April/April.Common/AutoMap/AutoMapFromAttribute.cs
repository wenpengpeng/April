// 文件名：AutoMapFromAttribute.cs
// 
// 创建标识：温朋朋 2018-06-21 9:51
// 
// 修改标识：温朋朋2018-06-21 9:51
// 
// ------------------------------------------------------------------------------

using System;
using AutoMapper;

namespace April.Common.AutoMap
{
    public class AutoMapFromAttribute:AutoMapAttributeBase
    {
        public AutoMapFromAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
        public override void CreateMap(IMapperConfigurationExpression configuration, Type type)
        {
            if (TargetTypes == null || TargetTypes.Length <= 0)
                return;
            foreach (var targetType in TargetTypes)
            {
                configuration.CreateMap(type, targetType, MemberList.Destination);
            }
        }
    }
}