// 文件名：RoleListDto.cs
// 
// 创建标识：温朋朋 2018-06-21 14:24
// 
// 修改标识：温朋朋2018-06-21 14:24
// 
// ------------------------------------------------------------------------------

using April.Common.AutoMap;
using Domain.Core.Permissions.Roles;

namespace April.Application.Permissions.Roles.Dtos
{
    [AutoMap(typeof(Role))]
    public class RoleListDto
    {
        /// <summary>
        ///     Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        ///     角色名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     是否系统角色（不允许修改）
        /// </summary>
        public bool IsSystem { get; set; }
    }
}