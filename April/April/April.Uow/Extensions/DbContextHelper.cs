// 文件名：DbContextHelper.cs
// 
// 创建标识：温朋朋 2018-05-16 9:21
// 
// 修改标识：温朋朋2018-05-16 9:21
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using April.Core.Reflection;
using April.Uow.Repositories;

namespace April.Uow.Extensions
{
    public static class DbContextHelper
    {
        /// <summary>
        ///     获取DbContext中DbSet反射信息
        /// </summary>
        /// <param name="dbContextType"></param>
        /// <returns></returns>
        public static IEnumerable<EntityTypeInfo> GetEntityTypeInfos(Type dbContextType)
        {
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                (ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>)) ||
                 ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))) &&
                ReflectionHelper.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0], typeof(IEntity<>))
                select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property.DeclaringType);
        }
    }
}