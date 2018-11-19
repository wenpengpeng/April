// 文件名：RoleEditDto.cs
// 
// 创建标识：温朋朋 2018-06-21 14:28
// 
// 修改标识：温朋朋2018-06-21 14:28
// 
// ------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using April.Common.AutoMap;
using Domain.Core.Permissions.Roles;

namespace April.Application.Permissions.Roles.Dtos
{
    [AutoMap(typeof(Role))]
    public class RoleEditDto
    {
        /// <summary>
        ///     Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        ///     角色名称
        /// </summary>
        [Required(ErrorMessage = "角色名称必填")]
        [MaxLength(20, ErrorMessage = "角色名称长度不能超过20")]
        public string Name { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }
    }
}