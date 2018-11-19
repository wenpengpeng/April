// 文件名：IAuditingConfiguration.cs
// 
// 创建标识：温朋朋 2018-05-18 13:56
// 
// 修改标识：温朋朋2018-05-18 13:56
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace April.Web.Auditing
{
    public interface IAuditingConfiguration
    {
        /// <summary>
        ///     指示是否开启审计功能（默认为True)
        /// </summary>
        bool IsEnabled { get; set; }
        /// <summary>
        ///     为true则为匿名用户添加审计日志（默认为false）
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }
        /// <summary>
        ///     选择那些必须审计的类和接口的select集合
        /// </summary>
        IAuditingSelectorList Selectors { get; }
        /// <summary>
        ///     忽略审计的类型集合
        /// </summary>
        List<Type> IgnoredTypes { get; }
    }
}