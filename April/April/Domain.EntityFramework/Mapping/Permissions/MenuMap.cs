// 文件名：MenuMap.cs
// 
// 创建标识：温朋朋 2018-05-29 17:29
// 
// 修改标识：温朋朋2018-05-29 17:29
// 
// ------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Domain.Core.Permissions.Menus;

namespace Domain.EntityFramework.Mapping.Permissions
{
    public class MenuMap:EntityTypeConfiguration<Menu>
    {
        public MenuMap()
        {
            this
                .HasKey(p => new { p.Id })
                .ToTable("Menu");
            // Properties:
            this
                .Property(p => p.Id)
                .HasColumnName(@"Id")
                .IsRequired()
                .HasColumnType("bigint");
            this
                .Property(p => p.Icon)
                .HasColumnName(@"Icon")
                .HasMaxLength(100)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.ParentId)
                .HasColumnName(@"ParentId")
                .HasColumnType("bigint");
            this
                .Property(p => p.DisplayName)
                .HasColumnName(@"DisplayName")
                .HasMaxLength(50)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.RequestUrl)
                .HasColumnName(@"RequestUrl")
                .HasMaxLength(100)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.Target)
                .HasColumnName(@"Target")
                .HasColumnType("int");
            this
                .Property(p => p.IsMenu)
                .HasColumnName(@"IsMenu")
                .IsRequired()
                .HasColumnType("bit");
            this
                .Property(p => p.IsExpand)
                .HasColumnName(@"IsExpand")
                .IsRequired()
                .HasColumnType("bit");
            this
                .Property(p => p.IsPublic)
                .HasColumnName(@"IsPublic")
                .IsRequired()
                .HasColumnType("bit");
            this
                .Property(p => p.IsInterface)
                .HasColumnName(@"IsInterface")
                .IsRequired()
                .HasColumnType("bit");
            this
                .Property(p => p.Remark)
                .HasColumnName(@"Remark")
                .HasMaxLength(200)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.IsValid)
                .HasColumnName(@"IsValid")
                .IsRequired()
                .HasColumnType("bit");
            this
                .Property(p => p.Code)
                .HasColumnName(@"Code")
                .HasMaxLength(50)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.Category)
                .HasColumnName(@"Category")
                .HasColumnType("int");
            this
                .Property(p => p.Layer)
                .HasColumnName(@"Layer")
                .HasMaxLength(500)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.HasLevel)
                .HasColumnName(@"HasLevel")
                .IsRequired()
                .HasColumnType("bit");
            this
                .Property(p => p.Sort)
                .HasColumnName(@"Sort")
                .IsRequired()
                .HasColumnType("int");            
            this
                .Property(p => p.CreationTime)
                .HasColumnName(@"CreationTime")
                .HasMaxLength(50)
                .HasColumnType("nvarchar2");            
            // Associations:
            this
                .HasMany(p => p.Childrens)
                .WithOptional(c => c.Parent)
                .HasForeignKey(p => new { p.ParentId })
                .WillCascadeOnDelete(false);
            this
                .HasMany(p => p.MenuAppAuthorizes)
                .WithRequired(c => c.Menu)
                .HasForeignKey(p => new { p.MenuId })
                .WillCascadeOnDelete(false);
            this
                .HasMany(p => p.Roles)
                .WithMany(c => c.Menus)
                .Map(manyToMany => manyToMany
                    .ToTable("RoleMenu")
                    .MapLeftKey("MenuId")
                    .MapRightKey("RoleId"));
        }
    }
}