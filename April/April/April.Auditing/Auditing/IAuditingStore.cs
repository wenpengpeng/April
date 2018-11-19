// 文件名：IAuditingStore.cs
// 
// 创建标识：温朋朋 2018-05-18 15:58
// 
// 修改标识：温朋朋2018-05-18 15:58
// 
// ------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace April.Web.Auditing
{
    public interface IAuditingStore
    {
        void Save(AuditInfo auditInfo);
        Task SaveAsync(AuditInfo auditInfo);
    }
}