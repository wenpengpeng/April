// 文件名：MemberMap.cs
// 
// 创建标识：温朋朋 2018-05-29 17:43
// 
// 修改标识：温朋朋2018-05-29 17:43
// 
// ------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using Domain.Core.Permissions.Members;

namespace Domain.EntityFramework.Mapping.Permissions
{
    public class MemberMap:EntityTypeConfiguration<Member>
    {
        public MemberMap()
        {
            HasKey(p => new { p.Id })
                .ToTable("Member");
            // Properties:
            Property(p => p.Id)
                .HasColumnName(@"Id")
                .IsRequired()
                .HasColumnType("number");
            Property(p => p.UserId)
                .HasColumnName(@"UserId")
                .IsRequired()
                .HasColumnType("bigint");
            Property(p => p.MemberCode)
                .HasColumnName(@"MemberCode")
                .HasMaxLength(100)
                .HasColumnType("nvarchar2");
            Property(p => p.CompanyName)
                .HasColumnName(@"CompanyName")
                .HasMaxLength(100)
                .HasColumnType("nvarchar2");
            Property(p => p.CompayTel)
                .HasColumnName(@"CompayTel")
                .HasMaxLength(20)
                .HasColumnType("nvarchar2");
            Property(p => p.BuyerAudit)
                .HasColumnName(@"BuyerAudit")
                .IsRequired()
                .HasColumnType("int");
            Property(p => p.IsSelfSupport)
                .HasColumnName(@"IsSelfSupport")
                .HasColumnType("bit");
            Property(p => p.SupplierAudit)
                .HasColumnName(@"SupplierAudit")
                .IsRequired()
                .HasColumnType("int");
            Property(p => p.UserType)
                .HasColumnName(@"UserType")
                .HasColumnType("int");
            Property(p => p.BidOpeningPassword)
                .HasColumnName(@"BidOpeningPassword")
                .HasMaxLength(200)
                .HasColumnType("nvarchar2");
            Property(p => p.TransactionPassword)
                .HasColumnName(@"TransactionPassword")
                .HasMaxLength(200)
                .HasColumnType("nvarchar2");
            Property(p => p.Remark)
                .HasColumnName(@"Remark")
                .HasMaxLength(2000)
                .HasColumnType("nvarchar2");            
            Property(p => p.CreationTime)
                .HasColumnName(@"CreationTime")
                .HasMaxLength(50)
                .HasColumnType("nvarchar2");
        }
    }
}