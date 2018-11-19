// 文件名：AutoMapExtensions.cs
// 
// 创建标识：温朋朋 2018-06-21 9:57
// 
// 修改标识：温朋朋2018-06-21 9:57
// 
// ------------------------------------------------------------------------------

using AutoMapper;

namespace April.Common.AutoMap
{
    public static class AutoMapExtensions
    {
        /// <summary>
        ///     source转换为TDestination，创建了一个新对象
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }
        /// <summary>
        ///     将source转换到destination，没有创建新对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source,destination);
        }
    }
}