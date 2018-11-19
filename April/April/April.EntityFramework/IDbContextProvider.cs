// 文件名：IDbContextProvider.cs
// 
// 创建标识：温朋朋 2018-05-15 11:17
// 
// 修改标识：温朋朋2018-05-15 11:17
// 
// ------------------------------------------------------------------------------

using System.Data.Entity;

namespace April.EntityFramework
{
    public interface IDbContextProvider<out TDbContext>
        where TDbContext:DbContext
    {
        TDbContext GetDbContext();
    }
}