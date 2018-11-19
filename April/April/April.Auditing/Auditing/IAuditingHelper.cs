// 文件名：IAuditingHelper.cs
// 
// 创建标识：温朋朋 2018-05-18 14:15
// 
// 修改标识：温朋朋2018-05-18 14:15
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace April.Web.Auditing
{
    public interface IAuditingHelper
    {
        /// <summary>
        ///     是否需要审计
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        bool ShouldSaveAudit(MethodInfo methodInfo,bool defaultValue=false);
        /// <summary>
        ///     创建AuditInfo对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        AuditInfo CreateAuditInfo(Type type,MethodInfo method,object[] arguments);
        /// <summary>
        ///     创建AuditInfo对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        AuditInfo CreateAuditInfo(Type type,MethodInfo method,IDictionary<string,object> arguments);
        /// <summary>
        ///     保存
        /// </summary>
        /// <param name="auditInfo"></param>
        void Save(AuditInfo auditInfo);
        /// <summary>
        ///     异步保存
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        Task SaveAsync(AuditInfo auditInfo);
    }
}