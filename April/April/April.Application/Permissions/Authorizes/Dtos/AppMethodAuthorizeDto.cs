// 文件名：AppMethodAuthorizeDto.cs
// 
// 创建标识：温朋朋 2018-06-21 15:31
// 
// 修改标识：温朋朋2018-06-21 15:31
// 
// ------------------------------------------------------------------------------

using April.Application.Authorizations.Entity;
using April.Common.AutoMap;

namespace April.Application.Permissions.Authorizes.Dtos
{
    [AutoMap(typeof(AppMethodAuthorize))]
    public class AppMethodAuthorizeDto
    {
        /// <summary>
        /// 操作码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsCheck { get; set; }
    }
}