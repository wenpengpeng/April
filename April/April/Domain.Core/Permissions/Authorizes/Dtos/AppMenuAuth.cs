// 文件名：AppMenuAuth.cs
// 
// 创建标识：温朋朋 2018-06-20 10:35
// 
// 修改标识：温朋朋2018-06-20 10:35
// 
// ------------------------------------------------------------------------------
namespace Domain.Core.Permissions.Authorizes.Dtos
{
    public class AppMenuAuth
    {
        /// <summary>
        ///     菜单Code
        /// </summary>
        public string MenuCode { get; set; }

        /// <summary>
        ///     AppCode
        /// </summary>
        public string AppCode { get; set; }

        /// <summary>
        ///     OperationCode
        /// </summary>
        public string OperationCode { get; set; }
    }
}