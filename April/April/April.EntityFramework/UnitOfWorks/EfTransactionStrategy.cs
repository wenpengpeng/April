// 文件名：EfTransactionStrategy.cs
// 
// 创建标识：温朋朋 2018-05-15 13:41
// 
// 修改标识：温朋朋2018-05-15 13:41
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Transactions;
using April.Core.Ioc;
using Autofac;
using Autofac.Core;

namespace April.EntityFramework.UnitOfWorks
{
    public class EfTransactionStrategy:IEfTransactionStrategy
    {
        /// <summary>
        ///     待提交的事务
        /// </summary>
        protected IDictionary<string, ActiveTransactionInfo> ActiveTransations { get; }
        /// <summary>
        ///     DbContexs
        /// </summary>
        protected List<DbContext> DbContexts { get; }
        /// <summary>
        ///     构造函数
        /// </summary>
        public EfTransactionStrategy()
        {
            DbContexts = new List<DbContext>();
            ActiveTransations = new Dictionary<string, ActiveTransactionInfo>();
        }
        /// <summary>
        ///     TransactionScope
        /// </summary>
        protected TransactionScope CurrentTransaction { get; set; }
        public virtual void InitOptions()
        {
            //初始化一些选项

            StartTransaction();
        }

        private void StartTransaction()
        {
            if (CurrentTransaction != null)
                return;
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted,
                Timeout=TimeSpan.FromSeconds(30)
            };
            CurrentTransaction = new TransactionScope(TransactionScopeOption.Required,transactionOptions,TransactionScopeAsyncFlowOption.Enabled);
        }
        public DbContext CreateDbContex<TDbContext>(string connectionString,IIocResolve iocResolve) where TDbContext : DbContext
        {
            DbContext dbContext;

            ActiveTransactionInfo activeTransaction;
            activeTransaction = ActiveTransations.TryGetValue(connectionString, out activeTransaction)
                ? activeTransaction
                : default(ActiveTransactionInfo);
            if (activeTransaction == null)//没找到则ioc通过NamedParameter创建一个
            {
                dbContext = iocResolve.ResolveParameter<TDbContext>(new NamedParameter("nameOrConnectionString",
                    connectionString));
                var dbTransaction = dbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
                activeTransaction = new ActiveTransactionInfo(dbTransaction, dbContext);
                ActiveTransations[connectionString] = activeTransaction;
            }
            else//找到了则通过ioc把DbConnection作为参数创建一个DbContext
            {
                dbContext = iocResolve.ResolveParameter<TDbContext>(new Parameter[]{
                    new NamedParameter("existingConnection", activeTransaction.DbContextTransaction.UnderlyingTransaction.Connection),
                    new NamedParameter("contextOwnsConnection",false)});//通过connection参数resolve一个dbcontext对象

                //dbContext = iocResolve.ResolveParameter<TDbContext>(new TypedParameter(typeof(DbConnection),
                    //activeTransaction.DbContextTransaction.UnderlyingTransaction.Connection));
                dbContext.Database.UseTransaction(activeTransaction.DbContextTransaction.UnderlyingTransaction);
                activeTransaction.AttendedDbContexts.Add(dbContext);
            }
            DbContexts.Add(dbContext);
            return dbContext;
        }
        /// <summary>
        ///     提交事务
        /// </summary>
        public virtual void Commit()
        {
            foreach (var activeTransaction in ActiveTransations.Values)
            {
                activeTransaction.DbContextTransaction.Commit();
            }
            if (CurrentTransaction == null)
                return;
            CurrentTransaction.Complete();
            CurrentTransaction.Dispose();
            CurrentTransaction = null;
        }
        /// <summary>
        ///     Dispose
        /// </summary>
        /// <param name="iocResolve"></param>
        public virtual void Dispose(IIocResolve iocResolve)
        {
            foreach (var activeTransaction in ActiveTransations.Values)
            {
                foreach (var attendedDbContext in activeTransaction.AttendedDbContexts)
                {
                    iocResolve.Release(attendedDbContext);
                }
                activeTransaction.DbContextTransaction.Dispose();
                iocResolve.Release(activeTransaction.StartDbContext);
            }

            //清除事务
            ActiveTransations.Clear();

            foreach (var dbContext in DbContexts)
            {
                iocResolve.Release(dbContext);
            }
            //清除DbContexts
            DbContexts.Clear();

            if (CurrentTransaction == null) return;
            CurrentTransaction.Dispose();
            CurrentTransaction = null;
        }
    }
}