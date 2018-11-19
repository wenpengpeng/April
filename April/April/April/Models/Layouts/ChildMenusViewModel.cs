// 文件名：ChildMenusViewModel.cs
// 
// 创建标识：温朋朋 2018-07-03 14:28
// 
// 修改标识：温朋朋2018-07-03 14:28
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using April.Common.AutoMap;
using Domain.Core.Permissions.Menus;

namespace April.Models.Layouts
{
    [AutoMap(typeof(Menu))]
    public class ChildMenusViewModel
    {
        public ChildMenusViewModel()
        {
            ChildMenus = new List<ChildMenusViewModel>();
        }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 顶级Code  （用于标识定位）
        /// </summary>
        public string TopCode { get; set; }

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
        /// Children
        /// </summary>
        public List<ChildMenusViewModel> ChildMenus { get; set; }
    }
}