// 文件名：AppMethodAuthorizeAttribute.cs
// 
// 创建标识：温朋朋 2018-06-21 15:57
// 
// 修改标识：温朋朋2018-06-21 15:57
// 
// ------------------------------------------------------------------------------

using System;

namespace April.Application.Authorizations
{
    /// <summary>
    ///     方法授权验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =true)]
    public class AppMethodAuthorizeAttribute:Attribute
    {
        /// <summary>
        /// 授权级别
        /// </summary>
        public ApplicationAuthorizeEnum AppAuthorize { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}