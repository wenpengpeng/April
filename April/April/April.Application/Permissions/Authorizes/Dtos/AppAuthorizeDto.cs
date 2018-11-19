// 文件名：AppAuthorizeDto.cs
// 
// 创建标识：温朋朋 2018-06-21 15:31
// 
// 修改标识：温朋朋2018-06-21 15:31
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using April.Application.Authorizations.Entity;
using April.Common.AutoMap;

namespace April.Application.Permissions.Authorizes.Dtos
{
    [AutoMap(typeof(AppAuthorize))]
    public class AppAuthorizeDto
    {
        /// <summary>
        /// AppAuthorize
        /// </summary>
        public AppAuthorizeDto()
        {
            AppMethodAuthorizes = new List<AppMethodAuthorizeDto>();
        }

        /// <summary>
        /// 操作码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// AppMethodAuthorizes
        /// </summary>
        public List<AppMethodAuthorizeDto> AppMethodAuthorizes { get; set; }
    }
}