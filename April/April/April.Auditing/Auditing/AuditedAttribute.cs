// 文件名：AuditedAttribute.cs
// 
// 创建标识：温朋朋 2018-05-18 13:38
// 
// 修改标识：温朋朋2018-05-18 13:38
// 
// ------------------------------------------------------------------------------

using System;

namespace April.Web.Auditing
{
    /// <summary>
    /// This attribute is used to apply audit logging for a single method or
    /// all methods of a class or interface.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method|AttributeTargets.Property)]
    public class AuditedAttribute:Attribute
    {
        
    }
}