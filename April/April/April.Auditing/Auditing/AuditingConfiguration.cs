// 文件名：AuditingConfiguration.cs
// 
// 创建标识：温朋朋 2018-05-18 14:09
// 
// 修改标识：温朋朋2018-05-18 14:09
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web;
using April.Web.Services;

namespace April.Web.Auditing
{
    public class AuditingConfiguration:IAuditingConfiguration
    {
        public bool IsEnabled { get; set; }
        public bool IsEnabledForAnonymousUsers { get; set; }
        public IAuditingSelectorList Selectors { get; }
        public List<Type> IgnoredTypes { get; }

        public AuditingConfiguration()
        {
            IsEnabled = true;//默认为true

            IsEnabledForAnonymousUsers = true;
            //默认实现了IApplicationService接口的需要审计
            Selectors = new AuditingSelectorList
            {
                new NamedTypeSelector("April.ApplicationServices",
                type=>typeof(IApplicationService).IsAssignableFrom(type))
            };
            //HttpPostedFileBase等作为默认忽略类型
            IgnoredTypes = new List<Type>{typeof(HttpPostedFileBase),
                typeof(IEnumerable<HttpPostedFileBase>),
                typeof(HttpPostedFileWrapper),
                typeof(IEnumerable<HttpPostedFileWrapper>)};
        }
    }
}