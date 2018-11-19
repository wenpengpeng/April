// 文件名：IAuditSerializer.cs
// 
// 创建标识：温朋朋 2018-05-18 17:09
// 
// 修改标识：温朋朋2018-05-18 17:09
// 
// ------------------------------------------------------------------------------
namespace April.Web.Auditing
{
    public interface IAuditSerializer
    {
        /// <summary>
        ///     Serialize对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Serialize(object obj);
    }
}