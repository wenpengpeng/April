// 文件名：Student.cs
// 
// 创建标识：温朋朋 2018-05-21 15:48
// 
// 修改标识：温朋朋2018-05-21 15:48
// 
// ------------------------------------------------------------------------------

using April.Uow.Repositories;

namespace WebTest.Entities
{
    public class Student:IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}