// 文件名：PersonMap.cs
// 
// 创建标识：温朋朋 2018-05-16 14:57
// 
// 修改标识：温朋朋2018-05-16 14:57
// 
// ------------------------------------------------------------------------------

using System.Data.Entity.ModelConfiguration;
using WebTest.Entities;

namespace WebTest.Mapping
{
    public class PersonMap: EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            HasKey(p => new {p.Id})
                .ToTable("Person");
            Property(p => p.Id)
                .HasColumnName(@"Id")
                .IsRequired()
                .HasColumnType("bigint");
            Property(p => p.Name)
                .HasColumnName("Name")                
                .HasMaxLength(20)
                .HasColumnType("nvarchar");
            Property(p => p.Age)
                .HasColumnName("Age")
                .HasColumnType("int");
        }
    }
}