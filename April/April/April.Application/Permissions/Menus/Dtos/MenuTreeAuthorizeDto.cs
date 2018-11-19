// 文件名：MenuTreeAuthorizeDto.cs
// 
// 创建标识：温朋朋 2018-06-21 15:26
// 
// 修改标识：温朋朋2018-06-21 15:26
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using April.Application.Permissions.Authorizes.Dtos;
using April.Common.AutoMap;
using Domain.Core.Permissions.Menus;
using Domain.Core.Permissions.Menus.Dtos;

namespace April.Application.Permissions.Menus.Dtos
{
    [AutoMap(typeof(Menu))]
    public class MenuTreeAuthorizeDto
    {
        /// <summary>
        /// MenuTreeAuthorizeDto
        /// </summary>
        public MenuTreeAuthorizeDto()
        {
            MenuAppAuthorizes = new List<MenuAppAuthorizeDto>();
        }

        /// <summary>
        ///   Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     父级Id
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 显示名字
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsCheck { get; set; }

        /// <summary>
        /// MenuAppAuthorizes
        /// </summary>
        public List<MenuAppAuthorizeDto> MenuAppAuthorizes { get; set; }
    }
    [AutoMap(typeof(MenuJsTreeAuthorizeEntityDto))]
    public class MenuJsTreeAuthorizeDto : MenuJsTreeAuthorizeEntityDto
    {

    }
}