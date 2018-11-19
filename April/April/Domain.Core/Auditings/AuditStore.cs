// 文件名：AuditStore.cs
// 
// 创建标识：温朋朋 2018-05-29 11:11
// 
// 修改标识：温朋朋2018-05-29 11:11
// 
// ------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using April.Uow.Repositories;
using April.Web.Auditing;

namespace Domain.Core.Auditings
{
    public class AuditStore: IAuditingStore
    {
        private readonly IBaseRepository<AuditLog, long> _auditLogRepository;

        public AuditStore(IBaseRepository<AuditLog, long> auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }
        /// <summary>
        ///     同步方法保存
        /// </summary>
        /// <param name="auditInfo"></param>
        public void Save(AuditInfo auditInfo)
        {
            try
            {
                if (auditInfo.Exception != null)
                    _auditLogRepository.InsertAndGetId(AuditLog.CreateFromAuditInfo(auditInfo));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        ///     异步方法保存
        /// </summary>
        /// <param name="auditInfo"></param>
        /// <returns></returns>
        public virtual Task SaveAsync(AuditInfo auditInfo)
        {
            try
            {
                return auditInfo.Exception != null
                    ? _auditLogRepository.InsertAsync(AuditLog.CreateFromAuditInfo(auditInfo))
                    : Task.CompletedTask;
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }
    }
}