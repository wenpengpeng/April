// 文件名：StudentMap.cs
// 
// 创建标识：温朋朋 2018-05-21 15:50
// 
// 修改标识：温朋朋2018-05-21 15:50
// 
// ------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using WebTest.Entities;

namespace WebTest.Mapping
{
    public class StudentMap:EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            HasKey(p => new {p.Id})
                .ToTable("Student");
            Property(p => p.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .HasColumnType("nvarchar");
            Property(p => p.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("bigint");
            Property(p => p.Age)
                .HasColumnName("Age")                
                .HasColumnType("int");
        }
    }
}