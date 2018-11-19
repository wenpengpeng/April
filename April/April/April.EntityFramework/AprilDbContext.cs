// 文件名：AprilDbContext.cs
// 
// 创建标识：温朋朋 2018-05-11 16:59
// 
// 修改标识：温朋朋2018-05-11 16:59
// 
// ------------------------------------------------------------------------------

using System.Data.Entity;

namespace April.EntityFramework
{
    public abstract class AprilDbContext:DbContext
    {
        protected AprilDbContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {           
        }
    }
}