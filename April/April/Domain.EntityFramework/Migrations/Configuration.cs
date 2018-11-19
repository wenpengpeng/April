using Domain.EntityFramework.Migrations.Seed;

namespace Domain.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Domain.EntityFramework.EntityFramework.AprilWebDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        /// <summary>
        ///     执行update-database才会执行该方法
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(Domain.EntityFramework.EntityFramework.AprilWebDbContext context)
        {
            new DefaultPermissionCreator(context).Create();
            context.SaveChanges();
        }
    }
}
