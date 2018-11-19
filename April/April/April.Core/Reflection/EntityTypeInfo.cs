// 文件名：EntityTypeInfo.cs
// 
// 创建标识：温朋朋 2018-05-15 18:03
// 
// 修改标识：温朋朋2018-05-15 18:03
// 
// ------------------------------------------------------------------------------

using System;

namespace April.Core.Reflection
{
    public class EntityTypeInfo
    {
        /// <summary>
        /// Type of the entity.
        /// </summary>
        public Type EntityType { get; private set; }

        /// <summary>
        /// DbContext type that has DbSet property.
        /// </summary>
        public Type DeclaringType { get; private set; }

        public EntityTypeInfo(Type entityType, Type declaringType)
        {
            EntityType = entityType;
            DeclaringType = declaringType;
        }
    }
}