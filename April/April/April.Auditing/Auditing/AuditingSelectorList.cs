// 文件名：AuditingSelectorList.cs
// 
// 创建标识：温朋朋 2018-05-18 14:10
// 
// 修改标识：温朋朋2018-05-18 14:10
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace April.Web.Auditing
{
    public class AuditingSelectorList : List<NamedTypeSelector>,IAuditingSelectorList

    {
        public bool RemoveByName(string name)
        {
            return RemoveAll(s => s.Name == name) > 0;
        }
    }
}