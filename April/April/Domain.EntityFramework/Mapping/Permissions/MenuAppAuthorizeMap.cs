// 文件名：MenuAppAuthorizeMap.cs
// 
// 创建标识：温朋朋 2018-05-29 17:37
// 
// 修改标识：温朋朋2018-05-29 17:37
// 
// ------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Domain.Core.Permissions.Authorizes;

namespace Domain.EntityFramework.Mapping.Permissions
{
    public class MenuAppAuthorizeMap:EntityTypeConfiguration<MenuAppAuthorize>
    {
        public MenuAppAuthorizeMap()
        {
            this
                .HasKey(p => new { p.Id })
                .ToTable("MenuAppAuthorize");
            // Properties:
            this
                .Property(p => p.Id)
                .HasColumnName(@"Id")
                .IsRequired()
                .HasColumnType("bigint");
            this
                .Property(p => p.MenuCode)
                .HasColumnName(@"MenuCode")
                .HasMaxLength(100)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.AppCode)
                .HasColumnName(@"AppCode")
                .HasMaxLength(100)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.OperationCode)
                .HasColumnName(@"OperationCode")
                .HasMaxLength(30)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.OperationDescription)
                .HasColumnName(@"OperationDescription")
                .HasMaxLength(100)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.MenuId)
                .HasColumnName(@"MenuId")
                .IsRequired()
                .HasColumnType("bigint");
            // Association:
            this
                .HasMany(p => p.Roles)
                .WithMany(c => c.MenuAppAuthorizes)
                .Map(manyToMany => manyToMany
                    .ToTable("MenuAppAuthorizeRole")
                    .MapLeftKey("MenuAppAuthorizeId")
                    .MapRightKey("RoleId"));
        }
    }
}