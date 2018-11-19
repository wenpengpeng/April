// 文件名：RoleMap.cs
// 
// 创建标识：温朋朋 2018-05-29 13:35
// 
// 修改标识：温朋朋2018-05-29 13:35
// 
// ------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Domain.Core.Permissions.Roles;

namespace Domain.EntityFramework.Mapping.Permissions
{
    public class RoleMap:EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this
                .HasKey(p => new { p.Id })
                .ToTable("Roles");
            // Properties:
            this
                .Property(p => p.Id)
                .HasColumnName(@"Id")
                .IsRequired()
                .HasColumnType("bigint");
            this
                .Property(p => p.BelongUserId)
                .HasColumnName(@"BelongUserId")                
                .HasColumnType("bigint");
            this
                .Property(p => p.Name)
                .HasColumnName(@"Name")
                .HasMaxLength(50)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.Code)
                .HasColumnName(@"Code")
                .HasMaxLength(50)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.IsValid)
                .HasColumnName(@"IsValid")
                .IsRequired()
                .HasColumnType("bit");
            this
                .Property(p => p.DefaultRoleType)
                .HasColumnName(@"DefaultRoleType")
                .HasColumnType("int");
            this
                .Property(p => p.IsSystem)
                .HasColumnName(@"IsSystem")
                .IsRequired()
                .HasColumnType("bit");            
            this
                .Property(p => p.CreationTime)
                .HasColumnName(@"CreationTime")
                .HasMaxLength(50)
                .HasColumnType("nvarchar2");            
        }
    }
}