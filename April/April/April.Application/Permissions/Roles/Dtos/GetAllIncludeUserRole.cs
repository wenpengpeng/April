// 文件名：GetAllIncludeUserRole.cs
// 
// 创建标识：温朋朋 2018-06-21 14:23
// 
// 修改标识：温朋朋2018-06-21 14:23
// 
// ------------------------------------------------------------------------------
namespace April.Application.Permissions.Roles.Dtos
{
    public class GetAllIncludeUserRole:RoleListDto
    {
        /// <summary>
        ///   是否选中
        /// </summary>
        public bool IsChecked { get; set; }
    }
}