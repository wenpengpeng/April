// 文件名：AprilPredicate.cs
// 
// 创建标识：温朋朋 2018-05-31 13:51
// 
// 修改标识：温朋朋2018-05-31 13:51
// 
// ------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace April.Common.Predicates
{
    /// <summary>
    ///     条件过滤类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AprilPredicate<T> where T:class
    {
        /// <summary>
        ///     是否执行Expression
        /// </summary>
        public bool Condition { get; set; }
        /// <summary>
        ///     Expression
        /// </summary>
        public Expression<Func<T,bool>> Expression { get; set; }
    }
}