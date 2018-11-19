// 文件名：AprilWebDbContext.cs
// 
// 创建标识：温朋朋 2018-05-29 17:51
// 
// 修改标识：温朋朋2018-05-29 17:51
// 
// ------------------------------------------------------------------------------

using System.Data.Entity;
using April.EntityFramework;
using Domain.Core.AprilSessions;
using Domain.Core.Auditings;
using Domain.Core.Permissions.Authorizes;
using Domain.Core.Permissions.Members;
using Domain.Core.Permissions.Menus;
using Domain.Core.Permissions.Roles;
using Domain.Core.Permissions.Users;

namespace Domain.EntityFramework.EntityFramework
{
    public class AprilWebDbContext:AprilDbContext
    {
        /// <summary>
        ///     用户
        /// </summary>
        public virtual IDbSet<UserBase> UserBases { get; set; }
        /// <summary>
        ///     会员
        /// </summary>
        public virtual IDbSet<Member> Members { get; set; }
        /// <summary>
        ///     角色
        /// </summary>
        public virtual IDbSet<Role> Roles { get; set; }
        /// <summary>
        ///     菜单
        /// </summary>
        public virtual IDbSet<Menu> Menus { get; set; }
        /// <summary>
        ///     用户申明
        /// </summary>
        public virtual IDbSet<UserClaim> UserClaims { get; set; }
        /// <summary>
        ///     菜单授权
        /// </summary>
        public virtual IDbSet<MenuAppAuthorize> MenuAppAuthorizes { get; set; }
        /// <summary>
        ///     审计日志
        /// </summary>
        public virtual IDbSet<AuditLog> AuditLogs { get; set; }
        public AprilWebDbContext()
            :base("Default")
        {
            
        }
        public AprilWebDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer<AprilWebDbContext>(null);
            //Database.SetInitializer(new CreateDatabaseIfNotExists<AprilWebDbContext>());
        }
        public IAprilSessionExtensions AprilSession { get; set; }
    }
}