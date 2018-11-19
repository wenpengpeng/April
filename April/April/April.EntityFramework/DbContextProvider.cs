// 文件名：DbContextProvider.cs
// 
// 创建标识：温朋朋 2018-05-15 11:20
// 
// 修改标识：温朋朋2018-05-15 11:20
// 
// ------------------------------------------------------------------------------

using System.Data.Entity;

namespace April.EntityFramework
{
    /// <summary>
    ///     DbContext提供器
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class DbContextProvider<TDbContext>:IDbContextProvider<TDbContext>
        where TDbContext:DbContext
    {
        public TDbContext DbContext { get; }

        public DbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public TDbContext GetDbContext()
        {
            return DbContext;
        }
    }
}