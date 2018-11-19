// 文件名：UserBaseMap.cs
// 
// 创建标识：温朋朋 2018-05-29 11:28
// 
// 修改标识：温朋朋2018-05-29 11:28
// 
// ------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Domain.Core.Permissions.Users;

namespace Domain.EntityFramework.Mapping.Permissions
{
    public class UserBaseMap:EntityTypeConfiguration<UserBase>
    {
        public UserBaseMap()
        {
            HasKey(p => new { p.Id })
                .ToTable("UserBase");
            // Properties:
            Property(p => p.Id)
                .HasColumnName(@"Id")
                .IsRequired()
                .HasColumnType("bigint");          
            Property(p => p.BelongUserId)
                .HasColumnName(@"BelongUserId")
                .HasColumnType("bigint");
            Property(p => p.UserName)
                .HasColumnName(@"UserName")
                .HasMaxLength(20)
                .HasColumnType("nvarchar2");
            Property(p => p.IsLockoutEnaled)
                .HasColumnName(@"IsLockoutEnaled")
                .IsRequired()
                .HasColumnType("bit");
            Property(p => p.AccessFailedCount)
                .HasColumnName(@"AccessFailedCount")
                .HasColumnType("int");
            Property(p => p.SecurityStamp)
                .HasColumnName(@"SecurityStamp")
                .HasMaxLength(100)
                .HasColumnType("nvarchar2");
            Property(p => p.PasswordHash)
                .HasColumnName(@"PasswordHash")
                .HasMaxLength(200)
                .HasColumnType("nvarchar2");
            Property(p => p.IsEmailComfirmed)
                .HasColumnName(@"IsEmailComfirmed")
                .IsRequired()
                .HasColumnType("bit");
            Property(p => p.Email)
                .HasColumnName(@"Email")
                .HasMaxLength(50)
                .HasColumnType("nvarchar2");
            Property(p => p.PhoneNumber)
                .HasColumnName(@"PhoneNumber")
                .HasMaxLength(11)
                .HasColumnType("nvarchar2");
            Property(p => p.IsPhoneNumberComfirmed)
                .HasColumnName(@"IsPhoneNumberComfirmed")
                .IsRequired()
                .HasColumnType("bit");
            Property(p => p.RealName)
                .HasColumnName(@"RealName")
                .HasMaxLength(20)
                .HasColumnType("nvarchar2");
            Property(p => p.IsRelationExpert)
                .HasColumnName(@"IsRelationExpert")
                .IsRequired()
                .HasColumnType("bit");           
            Property(p => p.CreationTime)
                .HasColumnName(@"CreationTime")                
                .HasColumnType("nvarchar2");           
            // Associations:
            HasMany(p => p.Managers)
                .WithRequired(c => c.UserBase)
                .HasForeignKey(p => new { p.UserId })
                .WillCascadeOnDelete(false);
            HasMany(p => p.Members)
                .WithRequired(c => c.UserBase)
                .HasForeignKey(p => new { p.UserId })
                .WillCascadeOnDelete(false);
            HasMany(p => p.UserClaims)
                .WithRequired(c => c.UserBase)
                .HasForeignKey(p => new { p.UserId })
                .WillCascadeOnDelete(false);
            HasMany(p => p.Roles)
                .WithMany(c => c.UserBases)
                .Map(manyToMany => manyToMany
                    .ToTable("UserRole")
                    .MapLeftKey("UserId")
                    .MapRightKey("RoleId"));
        }
    }
}