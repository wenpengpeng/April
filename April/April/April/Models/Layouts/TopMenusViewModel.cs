// 文件名：TopMenusViewModel.cs
// 
// 创建标识：温朋朋 2018-07-03 14:27
// 
// 修改标识：温朋朋2018-07-03 14:27
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using April.Common.AutoMap;
using Domain.Core.Enums.Users;
using Domain.Core.Permissions.Menus;

namespace April.Models.Layouts
{
    [AutoMap(typeof(Menu))]
    public class TopMenusViewModel
    {

        public TopMenusViewModel()
        {
            ChildMenus = new List<ChildMenusViewModel>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     权限分类（枚举：前端，后台）
        /// </summary>
        public MenuCategoryEnum? Category { get; set; }

        /// <summary>
        /// Children
        /// </summary>
        public List<ChildMenusViewModel> ChildMenus { get; set; }
    }
}