// 文件名：Person.cs
// 
// 创建标识：温朋朋 2018-05-16 14:55
// 
// 修改标识：温朋朋2018-05-16 14:55
// 
// ------------------------------------------------------------------------------

using April.Uow.Repositories;

namespace WebTest.Entities
{
    public class Person:IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}