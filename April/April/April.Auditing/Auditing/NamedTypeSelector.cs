// 文件名：NamedTypeSelector.cs
// 
// 创建标识：温朋朋 2018-05-18 14:03
// 
// 修改标识：温朋朋2018-05-18 14:03
// 
// ------------------------------------------------------------------------------

using System;

namespace April.Web.Auditing
{
    /// <summary>
    /// Used to represent a named type selector.
    /// </summary>
    public class NamedTypeSelector
    {
        /// <summary>
        ///     selector的名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     Predicate
        /// </summary>
        public Func<Type,bool> Predicate { get; set; }
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="predicate"></param>
        public NamedTypeSelector(string name, Func<Type, bool> predicate)
        {
            Name = name;
            Predicate = predicate;
        }
    }
}