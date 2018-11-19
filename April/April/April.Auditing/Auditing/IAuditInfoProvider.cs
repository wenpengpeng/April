// 文件名：IAuditInfoProvider.cs
// 
// 创建标识：温朋朋 2018-05-18 15:43
// 
// 修改标识：温朋朋2018-05-18 15:43
// 
// ------------------------------------------------------------------------------

namespace April.Web.Auditing
{
    public interface IAuditInfoProvider
    {
        /// <summary>
        /// Called to fill needed properties.
        /// </summary>
        /// <param name="auditInfo">Audit info that is partially filled</param>
        void Fill(AuditInfo auditInfo);
    }
}