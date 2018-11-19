// 文件名：AuditDto.cs
// 
// 创建标识：温朋朋 2018-06-21 15:36
// 
// 修改标识：温朋朋2018-06-21 15:36
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace April.Application.Permissions.Authorizes.Dtos
{
    public class AuditDto
    {
        /// <summary>
        /// MenuId
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// EditMenuAppAuthorizes
        /// </summary>
        public List<EditMenuAppAuthorizeDto> EditMenuAppAuthorizes { get; set; }
    }
}