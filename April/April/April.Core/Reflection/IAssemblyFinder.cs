// 文件名：IAssemblyFinder.cs
// 
// 创建标识：温朋朋 2018-05-15 17:22
// 
// 修改标识：温朋朋2018-05-15 17:22
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;

namespace April.Core.Reflection
{
    public interface IAssemblyFinder
    {
        /// <summary>
        ///     获取所有的程序集
        /// </summary>
        /// <returns></returns>
        List<Assembly> GetAllAssemblies();
    }
}