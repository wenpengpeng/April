// 文件名：TypeFinder.cs
// 
// 创建标识：温朋朋 2018-05-15 17:37
// 
// 修改标识：温朋朋2018-05-15 17:37
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core.Internal;

namespace April.Core.Reflection
{
    public class TypeFinder:ITypeFinder
    {
        private readonly IAssemblyFinder _assemblyFinder;
        private readonly object _syncObj = new object();
        private Type[] _types;

        public TypeFinder(IAssemblyFinder assemblyFinder)
        {
            _assemblyFinder = assemblyFinder;
        }

        public Type[] Find(Func<Type, bool> predicate)
        {
            return GetAllTypes().Where(predicate).ToArray();
        }

        public Type[] FindAll()
        {
            return GetAllTypes().ToArray();
        }

        private Type[] GetAllTypes()
        {
            if (_types == null)
            {
                lock (_syncObj)
                {
                    if (_types == null)
                        _types = CreateTypeList().ToArray();
                }
            }
            return _types;
        }

        private List<Type> CreateTypeList()
        {
            var allTypes = new List<Type>();
            var assemblies = _assemblyFinder.GetAllAssemblies().Distinct();

            foreach (var assembly in assemblies)
            {                
                    Type[] typesInThisAssembly;
                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInThisAssembly = ex.Types;
                    }
                    if (typesInThisAssembly.IsNullOrEmpty())
                        continue;
                    allTypes.AddRange(typesInThisAssembly.Where(type => type != null));                              
            }
            return allTypes;
        }
    }
}