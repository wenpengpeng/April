// 文件名：Person.cs
// 
// 创建标识：温朋朋 2018-05-04 9:47
// 
// 修改标识：温朋朋2018-05-04 9:47
// 
// ------------------------------------------------------------------------------

using System;

namespace IocTest
{
    public class Person:IPerson
    {
        public void SayHello()
        {
            Console.WriteLine("你好");
        }
    }
}