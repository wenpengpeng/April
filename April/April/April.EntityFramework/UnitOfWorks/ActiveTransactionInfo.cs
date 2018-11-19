// 文件名：ActiveTransactionInfo.cs
// 
// 创建标识：温朋朋 2018-05-15 13:43
// 
// 修改标识：温朋朋2018-05-15 13:43
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data.Entity;

namespace April.EntityFramework.UnitOfWorks
{
    public class ActiveTransactionInfo
    {
        public DbContextTransaction DbContextTransaction { get; }
        public DbContext StartDbContext { get; }
        public List<DbContext> AttendedDbContexts { get; }

        public ActiveTransactionInfo(DbContextTransaction dbContextTransaction, DbContext startedDbContext)
        {
            DbContextTransaction = dbContextTransaction;
            StartDbContext = startedDbContext;
            AttendedDbContexts = new List<DbContext>();
        }
    }
}