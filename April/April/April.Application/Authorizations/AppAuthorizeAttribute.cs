// 文件名：AppAuthorizeAttribute.cs
// 
// 创建标识：温朋朋 2018-06-21 15:55
// 
// 修改标识：温朋朋2018-06-21 15:55
// 
// ------------------------------------------------------------------------------

using System;

namespace April.Application.Authorizations
{
    /// <summary>
    ///     授权验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =true)]
    public class AppAuthorizeAttribute:Attribute
    {
        /// <summary>
        /// 操作码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}