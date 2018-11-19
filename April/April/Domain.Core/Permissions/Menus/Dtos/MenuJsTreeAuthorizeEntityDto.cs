// 文件名：MenuJsTreeAuthorizeEntityDto.cs
// 
// 创建标识：温朋朋 2018-06-19 17:37
// 
// 修改标识：温朋朋2018-06-19 17:37
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Domain.Core.Permissions.Menus.Dtos
{
    /// <summary>
    ///     菜单JStree树形菜单DTO
    /// </summary>
    public class MenuJsTreeAuthorizeEntityDto
    {
        /// <summary>
        ///   MenuJsTreeAuthorizeEntityDto
        /// </summary>
        public MenuJsTreeAuthorizeEntityDto()
        {
            Children = new List<MenuJsTreeAuthorizeEntityDto>();
        }

        /// <summary>
        ///  显示的文字
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///  代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///   是否选择
        /// </summary>
        public bool Checked => State.Selected;

        /// <summary>
        ///   是否选择
        /// </summary>
        public bool Open => State.Opened;

        /// <summary>
        ///   子级列表
        /// </summary>
        public List<MenuJsTreeAuthorizeEntityDto> Children { get; set; }

        /// <summary>
        ///   状态
        /// </summary>
        public State State { get; set; }

        /// <summary>
        ///   图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///  类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///  Id
        /// </summary>
        public string Id { get; set; }
    }
    /// <summary>
    ///  JStree节点状态
    /// </summary>
    public class State
    {
        /// <summary>
        ///   是否已经选择
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        ///   是否展开
        /// </summary>
        public bool Opened { get; set; }
    }
}