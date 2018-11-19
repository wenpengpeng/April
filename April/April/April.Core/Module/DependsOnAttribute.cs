// 文件名：DependsOnAttribute.cs
// 
// 创建标识：温朋朋 2018-05-08 15:03
// 
// 修改标识：温朋朋2018-05-08 15:03
// 
// ------------------------------------------------------------------------------

using System;

namespace April.Core.Module
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =true)]
    public class DependsOnAttribute:Attribute
    {
        /// <summary>
        ///     所依赖的Module类型数组
        /// </summary>
        public Type[] DependsOnModuleTypes { get; set; }
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="dependsOnModuleTypes"></param>
        public DependsOnAttribute(params Type[] dependsOnModuleTypes)
        {
            DependsOnModuleTypes = dependsOnModuleTypes;
        }
    }
}