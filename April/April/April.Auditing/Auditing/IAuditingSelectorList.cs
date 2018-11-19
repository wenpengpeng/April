// 文件名：IAuditingSelectorList.cs
// 
// 创建标识：温朋朋 2018-05-18 14:02
// 
// 修改标识：温朋朋2018-05-18 14:02
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace April.Web.Auditing
{
    public interface IAuditingSelectorList:IList<NamedTypeSelector>
    {
        /// <summary>
        ///     通过name移除selector
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool RemoveByName(string name);
    }
}