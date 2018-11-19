// 文件名：GetManagerListDto.cs
// 
// 创建标识：温朋朋 2018-06-21 17:07
// 
// 修改标识：温朋朋2018-06-21 17:07
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using April.Application.Permissions.Roles.Dtos;
using April.Common.AutoMap;
using Domain.Core.Permissions.Managers;

namespace April.Application.Permissions.Managers.Dtos
{
    [AutoMap(typeof(Manager))]
    public class GetManagerListDto
    {
        /// <summary>
        /// 后台用户Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///   所有角色列表，其中ischecked=true的则是当前用户所属的角色
        /// </summary>
        public List<GetAllIncludeUserRole> RoleList { get; set; }
        /// <summary>
        ///     UserBase外键属性
        /// </summary>
        public GetUserBaseDto UserBase { get; set; }
    }
}