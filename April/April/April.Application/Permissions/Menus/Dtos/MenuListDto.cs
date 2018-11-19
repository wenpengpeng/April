// 文件名：MenuListDto.cs
// 
// 创建标识：温朋朋 2018-06-21 15:24
// 
// 修改标识：温朋朋2018-06-21 15:24
// 
// ------------------------------------------------------------------------------

using April.Common.AutoMap;
using Domain.Core.Permissions.Menus;

namespace April.Application.Permissions.Menus.Dtos
{
    [AutoMapFrom(typeof(Menu))]
    public class MenuListDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     显示名字
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// CodeLinkId
        /// </summary>
        public string CodeLinkId => Code;

        /// <summary>
        /// CodeLink
        /// </summary>
        public string CodeLink => Code;


        /// <summary>
        ///     父级Id
        /// </summary>
        public long? ParentId { get; set; }


        /// <summary>
        /// 是否存在子集
        /// </summary>
        public bool HasLevel { get; set; }

        /// <summary>
        /// 菜单代码
        /// </summary>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 请求url
        /// </summary>
        public string RequestUrl
        {
            get; set;
        }
    }
}