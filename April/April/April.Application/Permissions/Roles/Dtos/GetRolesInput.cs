// 文件名：GetRolesInput.cs
// 
// 创建标识：温朋朋 2018-06-21 14:27
// 
// 修改标识：温朋朋2018-06-21 14:27
// 
// ------------------------------------------------------------------------------

using Domain.Core.Pages;

namespace April.Application.Permissions.Roles.Dtos
{
    public class GetRolesInput:PageQueryEntity
    {
        /// <summary>
        /// 模糊查询参数
        /// </summary>
        public string FilterText { get; set; }
    }
}