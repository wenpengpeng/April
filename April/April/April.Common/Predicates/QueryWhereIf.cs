// 文件名：QueryWhereIf.cs
// 
// 创建标识：温朋朋 2018-05-31 13:53
// 
// 修改标识：温朋朋2018-05-31 13:53
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace April.Common.Predicates
{
    /// <summary>
    ///     WhereIf扩展类
    /// </summary>
    public static class QueryWhereIf
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, List<AprilPredicate<T>> predicates)
            where T : class
        {
            return predicates.Aggregate(source,
                (current, predicate) =>
                    predicate.Condition ? 
                    current.Where(predicate.Expression) : current);
        }
    }
}