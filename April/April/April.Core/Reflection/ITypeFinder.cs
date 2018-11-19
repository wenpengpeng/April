// 文件名：ITypeFinder.cs
// 
// 创建标识：温朋朋 2018-05-15 17:34
// 
// 修改标识：温朋朋2018-05-15 17:34
// 
// ------------------------------------------------------------------------------

using System;

namespace April.Core.Reflection
{
    public interface ITypeFinder
    {
        Type[] Find(Func<Type, bool> predicate);
        Type[] FindAll();
    }
}