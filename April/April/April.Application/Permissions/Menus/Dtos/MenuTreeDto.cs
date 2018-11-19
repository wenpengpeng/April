// 文件名：MenuTreeDto.cs
// 
// 创建标识：温朋朋 2018-06-21 15:29
// 
// 修改标识：温朋朋2018-06-21 15:29
// 
// ------------------------------------------------------------------------------

using April.Common.AutoMap;
using Domain.Core.Permissions.Menus;

namespace April.Application.Permissions.Menus.Dtos
{
    [AutoMap(typeof(Menu))]
    public class MenuTreeDto
    {
        /// <summary>
        ///   Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     父级Id
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get;
            set;
        }

        /// <summary>
        /// 显示名字
        /// </summary>
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// 菜单代码
        /// </summary>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        ///     请求URL
        /// </summary>
        public string RequestUrl
        {
            get; set;
        }

        /// <summary>
        /// 是否存在子集
        /// </summary>
        public bool HasLevel { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }
    }
}