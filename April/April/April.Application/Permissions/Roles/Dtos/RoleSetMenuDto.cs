// 文件名：RoleSetMenuDto.cs
// 
// 创建标识：温朋朋 2018-06-21 14:29
// 
// 修改标识：温朋朋2018-06-21 14:29
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace April.Application.Permissions.Roles.Dtos
{
    public class RoleSetMenuDto
    {
        /// <summary>
        ///     角色编号
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        ///     菜单操作授权集合
        /// </summary>
        public List<long> MenuAppAuthorizeIds { get; set; }

        /// <summary>
        ///     菜单编号集合
        /// </summary>
        public List<long> MenuIds { get; set; }
    }
}