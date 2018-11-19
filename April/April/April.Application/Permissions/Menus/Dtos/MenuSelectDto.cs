// 文件名：MenuSelectDto.cs
// 
// 创建标识：温朋朋 2018-06-21 15:26
// 
// 修改标识：温朋朋2018-06-21 15:26
// 
// ------------------------------------------------------------------------------

using April.Common.AutoMap;
using Domain.Core.Permissions.Menus;

namespace April.Application.Permissions.Menus.Dtos
{
    [AutoMap(typeof(Menu))]
    public class MenuSelectDto
    {
        /// <summary>
        ///   Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 显示名字
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }
    }
}