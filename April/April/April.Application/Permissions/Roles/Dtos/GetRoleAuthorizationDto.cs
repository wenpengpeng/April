// 文件名：GetRoleAuthorizationDto.cs
// 
// 创建标识：温朋朋 2018-06-21 14:25
// 
// 修改标识：温朋朋2018-06-21 14:25
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using April.Common.AutoMap;
using Domain.Core.Permissions.Roles;

namespace April.Application.Permissions.Roles.Dtos
{
    [AutoMap(typeof(Role))]
    public class GetRoleAuthorizationDto
    {
        /// <summary>
        /// RoleId
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// MenuIdList
        /// </summary>
        public List<long> MenuIdList { get; set; }
    }
}