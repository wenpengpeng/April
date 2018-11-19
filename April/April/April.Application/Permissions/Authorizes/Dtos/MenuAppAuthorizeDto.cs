// 文件名：MenuAppAuthorizeDto.cs
// 
// 创建标识：温朋朋 2018-06-21 15:37
// 
// 修改标识：温朋朋2018-06-21 15:37
// 
// ------------------------------------------------------------------------------

using April.Common.AutoMap;
using Domain.Core.Permissions.Authorizes;

namespace April.Application.Permissions.Authorizes.Dtos
{
    [AutoMap(typeof(MenuAppAuthorize))]
    public class MenuAppAuthorizeDto
    {
        /// <summary>
        ///     Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     操作代码描述
        /// </summary>
        public string OperationDescription { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsCheck { get; set; }
    }
}