// 文件名：AssemblyFinder.cs
// 
// 创建标识：温朋朋 2018-05-15 17:24
// 
// 修改标识：温朋朋2018-05-15 17:24
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace April.Core.Reflection
{
    public class AssemblyFinder:IAssemblyFinder
    {
        /// <summary>
        ///     获取当前程序的所有Assembly
        /// </summary>
        /// <returns></returns>
        public List<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
    }
}