﻿// 文件名：DisableAuditingAttribute.cs
// 
// 创建标识：温朋朋 2018-05-18 16:29
// 
// 修改标识：温朋朋2018-05-18 16:29
// 
// ------------------------------------------------------------------------------

using System;

namespace April.Web.Auditing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisableAuditingAttribute:Attribute
    {
        
    }
}