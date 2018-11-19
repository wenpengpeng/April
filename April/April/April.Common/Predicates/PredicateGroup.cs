// 文件名：PredicateGroup.cs
// 
// 创建标识：温朋朋 2018-05-31 15:05
// 
// 修改标识：温朋朋2018-05-31 15:05
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace April.Common.Predicates
{
    public class PredicateGroup<T> where T:class
    {
        /// <summary>
        /// PredicateGroup
        /// </summary>
        public PredicateGroup()
        {
            Predicates = new List<AprilPredicate<T>>();
        }

        /// <summary>
        /// Predicates
        /// </summary>
        public List<AprilPredicate<T>> Predicates { get; set; }

        /// <summary>
        /// AddPredicate
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="expression"></param>
        public void AddPredicate(bool condition, Expression<Func<T, bool>> expression)
        {
            var predicate = new AprilPredicate<T>
            {
                Condition = condition,
                Expression = expression
            };

            Predicates.Add(predicate);
        }
    }
}