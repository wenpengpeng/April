// 文件名：CreateOrUpdateManagerInputDto.cs
// 
// 创建标识：温朋朋 2018-06-21 17:07
// 
// 修改标识：温朋朋2018-06-21 17:07
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace April.Application.Permissions.Managers.Dtos
{
    public class CreateOrUpdateManagerInputDto
    {
        /// <summary>
        ///     Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        ///     管理员
        /// </summary>
        public EditManagerUserBaseDto UserBase { get; set; }

        /// <summary>
        ///     角色Id集合
        /// </summary>
        public List<long> RoleIds { get; set; }
    }
}