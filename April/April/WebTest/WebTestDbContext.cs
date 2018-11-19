// 文件名：WebTestDbContext.cs
// 
// 创建标识：温朋朋 2018-05-16 14:34
// 
// 修改标识：温朋朋2018-05-16 14:34
// 
// ------------------------------------------------------------------------------

using System.Data.Entity;
using April.EntityFramework;
using WebTest.Entities;

namespace WebTest
{
    public class WebTestDbContext:AprilDbContext
    {
        public WebTestDbContext()
            : base("WebTest")
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public WebTestDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.AutoDetectChangesEnabled = true; //对多对多，一对多进行curd操作时需要为true
            Configuration.LazyLoadingEnabled = false;

            //this.Configuration.AutoDetectChangesEnabled = false;//禁止状态追踪
            //this.Configuration.ProxyCreationEnabled = false;//禁止动态拦截System.Data.Entity.DynamicProxies.
            //自动创建表，如果Entity有改到就更新到表结构
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<DbEntity, Configuration>());
            //Database.SetInitializer<WebTestDbContext>(new DropCreateDatabaseIfModelChanges<WebTestDbContext>());//数据模型发生改变就重新创建数据库
        }
    }
}