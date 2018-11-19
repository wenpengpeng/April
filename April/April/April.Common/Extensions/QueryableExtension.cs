using System;
using System.Linq;
using System.Linq.Expressions;

namespace April.Common.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        ///     linq分页扩展
        /// </summary>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }

            return query.Skip(skipCount).Take(maxResultCount);
        }
    }
}