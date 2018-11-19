// 文件名：EntityHelper.cs
// 
// 创建标识：温朋朋 2018-05-16 10:49
// 
// 修改标识：温朋朋2018-05-16 10:49
// 
// ------------------------------------------------------------------------------

using System;
using System.Reflection;
using April.Core.Reflection;
using April.Uow.Repositories;

namespace April.Uow.Extensions
{
    public static class EntityHelper
    {
        public static bool IsEntity(Type type)
        {
            return ReflectionHelper.IsAssignableToGenericType(type, typeof(IEntity<>));
        }
        public static Type GetPrimaryKeyType<TEntity>()
        {
            return GetPrimaryKeyType(typeof(TEntity));
        }

        /// <summary>
        ///     Gets primary key type of given entity type
        /// </summary>
        public static Type GetPrimaryKeyType(Type entityType)
        {
            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }
            throw new Exception("出错了");
        }
    }
}