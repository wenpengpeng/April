// 文件名：IEfTransactionStrategy.cs
// 
// 创建标识：温朋朋 2018-05-15 13:39
// 
// 修改标识：温朋朋2018-05-15 13:39
// 
// ------------------------------------------------------------------------------

using April.Core.Ioc;
using System.Data.Entity;

namespace April.EntityFramework.UnitOfWorks
{
    public interface IEfTransactionStrategy
    {
        void InitOptions();

        DbContext CreateDbContex<TDbContex>(string connectionString,IIocResolve iocResolve)
            where TDbContex : DbContext;

        void Commit();
        void Dispose(IIocResolve iocResolve);
    }
}