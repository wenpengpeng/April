// 文件名：SimpleLogAuditingStore.cs
// 
// 创建标识：温朋朋 2018-05-18 16:01
// 
// 修改标识：温朋朋2018-05-18 16:01
// 
// ------------------------------------------------------------------------------

using System.Threading.Tasks;
using Castle.Core.Logging;
using Nito.AsyncEx;

namespace April.Web.Auditing
{
    public class SimpleLogAuditingStore:IAuditingStore
    {
        /// <summary>
        ///     简单实现，实际项目中没用
        /// </summary>
        public static SimpleLogAuditingStore Instance { get; } = new SimpleLogAuditingStore();

        public ILogger Logger { get; set; }

        public SimpleLogAuditingStore()
        {
            Logger = NullLogger.Instance;
        }

        public void Save(AuditInfo auditInfo)
        {
            AsyncContext.Run(()=>SaveAsync(auditInfo));
        }

        public Task SaveAsync(AuditInfo auditInfo)
        {
            if (auditInfo.Exception == null)
            {
                Logger.Info(auditInfo.ToString());
            }
            else
            {
                Logger.Warn(auditInfo.ToString());
            }

            return Task.FromResult(0);
        }
    }
}