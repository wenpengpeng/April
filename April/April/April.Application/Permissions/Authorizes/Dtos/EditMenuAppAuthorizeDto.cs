// 文件名：EditMenuAppAuthorizeDto.cs
// 
// 创建标识：温朋朋 2018-06-21 15:36
// 
// 修改标识：温朋朋2018-06-21 15:36
// 
// ------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using April.Common.AutoMap;
using Domain.Core.Permissions.Authorizes;

namespace April.Application.Permissions.Authorizes.Dtos
{
    [AutoMap(typeof(MenuAppAuthorize))]
    public class EditMenuAppAuthorizeDto
    {
        /// <summary>
        ///     操作代码描述
        /// </summary>
        [Required(ErrorMessage = "操作代码描述不能为空！")]
        public string OperationDescription { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        ///     菜单代码
        /// </summary>
        [Required(ErrorMessage = "菜单代码不能为空！")]
        public string MenuCode { get; set; }

        /// <summary>
        ///     AppService代码
        /// </summary>
        [Required(ErrorMessage = "AppService代码不能为空！")]
        public string AppCode { get; set; }

        /// <summary>
        ///     操作代码
        /// </summary>
        [Required(ErrorMessage = "操作代码不能为空！")]
        public string OperationCode { get; set; }
    }
}