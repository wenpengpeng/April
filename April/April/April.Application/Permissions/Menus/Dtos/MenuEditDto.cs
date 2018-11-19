// 文件名：MenuEditDto.cs
// 
// 创建标识：温朋朋 2018-06-21 15:22
// 
// 修改标识：温朋朋2018-06-21 15:22
// 
// ------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using April.Common.AutoMap;
using Domain.Core.Enums.Users;
using Domain.Core.Permissions.Menus;

namespace April.Application.Permissions.Menus.Dtos
{
    [AutoMap(typeof(Menu))]
    public class MenuEditDto
    {
        /// <summary>
        ///     Id
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     父级Id
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        ///     显示名字
        /// </summary>
        [Required]
        public string DisplayName { get; set; }

        /// <summary>
        ///     请求URL
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        ///     描述备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///     是否公用
        /// </summary>
        public bool? IsPublic { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool? IsValid { get; set; }

        /// <summary>
        ///     权限分类（枚举：前端，后台）
        /// </summary>
        public MenuCategoryEnum? Category { get; set; }

        /// <summary>
        ///  排序
        /// </summary>
        public int? Sort { get; set; }
    }
}