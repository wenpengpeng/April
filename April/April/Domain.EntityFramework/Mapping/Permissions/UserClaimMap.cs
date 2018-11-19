// 文件名：UserClaimMap.cs
// 
// 创建标识：温朋朋 2018-05-29 11:41
// 
// 修改标识：温朋朋2018-05-29 11:41
// 
// ------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Domain.Core.Permissions.Users;

namespace Domain.EntityFramework.Mapping.Permissions
{
    public class UserClaimMap:EntityTypeConfiguration<UserClaim>
    {
        public UserClaimMap()
        {
            this
                .HasKey(p => new { p.Id })
                .ToTable("UserClaim");
            // Properties:
            this
                .Property(p => p.Id)
                .HasColumnName(@"Id")
                .IsRequired()
                .HasColumnType("bigint");
            this
                .Property(p => p.ClaimType)
                .HasColumnName(@"ClaimType")
                .HasMaxLength(100)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.ClaimValue)
                .HasColumnName(@"ClaimValue")
                .HasMaxLength(2000)
                .HasColumnType("nvarchar2");
            this
                .Property(p => p.UserId)
                .HasColumnName(@"UserId")
                .IsRequired()
                .HasColumnType("bigint");            
            this
                .Property(p => p.CreationTime)
                .HasColumnName(@"CreationTime")
                .HasMaxLength(50)
                .HasColumnType("nvarchar2");           
        }
    }
}