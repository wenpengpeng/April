// 文件名：EfUnitOfWork.cs
// 
// 创建标识：温朋朋 2018-05-10 16:57
// 
// 修改标识：温朋朋2018-05-10 16:57
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Configuration;
using System.Data.Entity;
using System.Threading.Tasks;
using April.Core.Ioc;
using April.Uow.UnitOfWorks;

namespace April.EntityFramework.UnitOfWorks
{
    public class EfUnitOfWork:UnitOfWorkBase
    {     
        protected IDictionary<string,DbContext> ActiveDbContexts { get; }
        protected IIocResolve IocResolve { get; }
        private readonly IEfTransactionStrategy _efTransactionStrategy;
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="efTransactionStrategy"></param>
        /// <param name="iocResolve"></param>
        public EfUnitOfWork(IEfTransactionStrategy efTransactionStrategy,IIocResolve iocResolve)
        {
            _efTransactionStrategy = efTransactionStrategy;
            IocResolve = iocResolve;

            ActiveDbContexts = new Dictionary<string, DbContext>();
        }
        /// <summary>
        ///    SaveChanges 
        /// </summary>
        public override void SaveChanges()
        {
            foreach (var dbContext in ActiveDbContexts.Values.ToImmutableList())
            {
                SaveChangesInDbContext(dbContext);
            }
        }
        /// <summary>
        ///     SaveChangesAsync
        /// </summary>
        /// <returns></returns>
        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in ActiveDbContexts.Values.ToImmutableList())
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }

        protected override void CompleteUnit()
        {
            SaveChanges();//先保存更改

            _efTransactionStrategy.Commit();

        }

        protected override async Task CompleteUnitAsync()
        {
            await SaveChangesAsync();//先保存更改

            _efTransactionStrategy.Commit();
        }

        protected override void DisposeUnit()
        {
            _efTransactionStrategy.Dispose(IocResolve);
            ActiveDbContexts.Clear();
        }

        protected override void BeginUnit()
        {
            _efTransactionStrategy.InitOptions();
        }

        public virtual TDbContext GetOrCreateDbContext<TDbContext>()
            where TDbContext : DbContext
        {            
            var connectionString = "Default";//这里简化直接写死  测试时用WebTest           
            var dbContextKey = typeof(TDbContext).FullName + "#" + connectionString;
            DbContext dbContext;
            if (!ActiveDbContexts.TryGetValue(dbContextKey, out dbContext))
            {
                dbContext = _efTransactionStrategy.CreateDbContex<TDbContext>(connectionString,IocResolve);
                if (!dbContext.Database.CommandTimeout.HasValue)
                {
                    dbContext.Database.CommandTimeout = 30;//默认30秒
                }
                ActiveDbContexts[dbContextKey] = dbContext;
            }
            return (TDbContext)dbContext;
        }
        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }
        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }
    }
}