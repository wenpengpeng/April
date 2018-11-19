// 文件名：AuditSore.cs
// 
// 创建标识：温朋朋 2018-05-24 10:01
// 
// 修改标识：温朋朋2018-05-24 10:01
// 
// ------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using April.Uow.Repositories;
using April.Web.Auditing;
using WebTest.Entities;

namespace WebTest.Auditing
{
    public class AuditStore:IAuditingStore
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
        public Task SaveAsync(AuditInfo auditInfo)
        {
            try
            {
                return auditInfo.Exception != null
                    ? _auditLogRepository.InsertAndGetIdAsync(AuditLog.CreateFromAuditInfo(auditInfo))
                    : Task.CompletedTask;
            }
            catch (Exception)
            {
                return Task.CompletedTask;
            }
        }
    }
}