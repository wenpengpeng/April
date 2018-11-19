// 文件名：UnitOfWorkDbContextProvider.cs
// 
// 创建标识：温朋朋 2018-05-21 10:12
// 
// 修改标识：温朋朋2018-05-21 10:12
// 
// ------------------------------------------------------------------------------

using System.Data.Entity;
using April.Uow.UnitOfWorks;

namespace April.EntityFramework.UnitOfWorks
{
    /// <summary>
    ///     工作单元DbContext提供器
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public class UnitOfWorkDbContextProvider<TDbContext>:IDbContextProvider<TDbContext>
        where TDbContext:DbContext
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public UnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }

        public TDbContext GetDbContext()
        {
            return _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>();
        }
    }
}