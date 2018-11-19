// 文件名：DependencyLifeStyle.cs
// 
// 创建标识：温朋朋 2018-05-04 11:28
// 
// 修改标识：温朋朋2018-05-04 11:28
// 
// ------------------------------------------------------------------------------
namespace April.Core.Ioc
{
    public enum DependencyLifeStyle
    {
        /// <summary>
        /// Singleton object. Created a single object on first resolving
        /// and same instance is used for subsequent resolves.
        /// </summary>
        Singleton,

        /// <summary>
        /// Transient object. Created one object for every resolving.
        /// </summary>
        Transient
    }
}