// 文件名：AuditLogMap.cs
// 
// 创建标识：温朋朋 2018-05-24 9:29
// 
// 修改标识：温朋朋2018-05-24 9:29
// 
// ------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using WebTest.Entities;

namespace WebTest.Mapping
{
    public class AuditLogMap:EntityTypeConfiguration<AuditLog>
    {
        public AuditLogMap()
        {
            HasKey(p => new { p.Id })
                .ToTable("AuditLog");
            // Properties:
            Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired()
                .HasColumnType("bigint");
            Property(p => p.UserId)
                .HasColumnName("UserId")
                .HasColumnType("bigint");
            Property(p => p.ServiceName)
                .HasColumnName("ServiceName")
                .HasMaxLength(256)
                .HasColumnType("nvarchar2");
            Property(p => p.MethodName)
                .HasColumnName("MethodName")
                .HasMaxLength(256)
                .HasColumnType("nvarchar2");
            Property(p => p.Parameters)
                .HasColumnName("Parameters")
                .HasMaxLength(1024)
                .HasColumnType("nvarchar2");
            Property(p => p.ExecutionTime)
                .HasColumnName("ExecutionTime")
                .IsRequired()
                .HasColumnType("date");
            Property(p => p.ExecutionDuration)
                .HasColumnName("ExecutionDuration")
                .IsRequired()
                .HasColumnType("int");
            Property(p => p.ClientIpAddress)
                .HasColumnName("ClientIpAddress")
                .HasMaxLength(64)
                .HasColumnType("nvarchar2");
            Property(p => p.ClientName)
                .HasColumnName(@"ClientName")
                .HasMaxLength(128)
                .HasColumnType("nvarchar2");
            Property(p => p.BrowserInfo)
                .HasColumnName("BrowserInfo")
                .HasMaxLength(256)
                .HasColumnType("nvarchar2");
            Property(p => p.Exception)
                .HasColumnName("Exception")
                .HasMaxLength(2000)
                .HasColumnType("nvarchar2");            
            Property(p => p.ImpersonatorTenantId)
                .HasColumnName("ImpersonatorTenantId")
                .HasColumnType("int");
            Property(p => p.CustomData)
                .HasColumnName("CustomData")
                .HasMaxLength(2000)
                .HasColumnType("nvarchar2");
        }
    }
}