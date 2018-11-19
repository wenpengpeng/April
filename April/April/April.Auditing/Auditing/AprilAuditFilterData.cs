// 文件名：AprilAuditFilterData.cs
// 
// 创建标识：温朋朋 2018-06-05 16:17
// 
// 修改标识：温朋朋2018-06-05 16:17
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;
using System.Web;

namespace April.Web.Auditing
{
    /// <summary>
    ///     在actionFilter中使用，原理就是把AuditInfo和Stopwatch包裹成AprilAuditFilterData然后保存在httpcontext.item中
    /// </summary>
    public class AprilAuditFilterData
    {
        private const string AprilAuditFilterDataHttpContextKey = "_AprilAuditFilterData";
        /// <summary>
        ///     记时器
        /// </summary>
        public Stopwatch Stopwatch { get; }
        /// <summary>
        ///     审计信息实体
        /// </summary>
        public AuditInfo AuditInfo { get; }

        public AprilAuditFilterData(Stopwatch stopwatch, AuditInfo auditInfo)
        {
            Stopwatch = stopwatch;
            AuditInfo = auditInfo;
        }
        public static void Set(HttpContextBase httpContext, AprilAuditFilterData auditFilterData)
        {
            GetAuditDataStack(httpContext).Push(auditFilterData);//向stack中推入一个实体
        }

        public static AprilAuditFilterData GetOrNull(HttpContextBase httpContext)
        {
            var stack = GetAuditDataStack(httpContext);
            return stack.Count <= 0
                ? null
                : stack.Pop();//pop（）方法移除并返回stack中最顶部的实体
        }
        /// <summary>
        ///     获取AprilAuditFilterData,若为空则new一个（用stack（表示可变大小的后进先出集合（对于相同类型的指定类型））包裹）
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static Stack<AprilAuditFilterData> GetAuditDataStack(HttpContextBase httpContext)
        {
            var stack = httpContext.Items[AprilAuditFilterDataHttpContextKey] as Stack<AprilAuditFilterData>;

            if (stack == null)
            {
                stack = new Stack<AprilAuditFilterData>();
                httpContext.Items[AprilAuditFilterDataHttpContextKey] = stack;
            }

            return stack;
        }
    }
}