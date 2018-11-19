// 文件名：Menu.cs
// 
// 创建标识：温朋朋 2018-05-28 10:26
// 
// 修改标识：温朋朋2018-05-28 10:26
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using April.Uow.Repositories;
using Domain.Core.Enums.Users;
using Domain.Core.Permissions.Authorizes;
using Domain.Core.Permissions.Roles;

namespace Domain.Core.Permissions.Menus
{
    public class Menu:IEntity<long>
    {

        #region 属性
        /// <summary>
        ///     Id
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        ///     图标
        /// </summary>
        public virtual string Icon { get; set; }

        /// <summary>
        ///     父级Id
        /// </summary>
        public virtual long? ParentId { get; set; }

        /// <summary>
        ///     显示名字
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        ///     请求URL
        /// </summary>
        public virtual string RequestUrl { get; set; }

        /// <summary>
        ///     跳转目标
        /// </summary>
        public virtual MenuTargetEnum? Target { get; set; }

        /// <summary>
        ///     是否菜单
        /// </summary>
        public virtual bool IsMenu { get; set; }

        /// <summary>
        ///     是否展开
        /// </summary>
        public virtual bool IsExpand { get; set; }

        /// <summary>
        ///     是否公用
        /// </summary>
        public virtual bool IsPublic { get; set; }

        /// <summary>
        ///     是否是接口
        /// </summary>
        public virtual bool IsInterface { get; set; }

        /// <summary>
        ///     描述备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public virtual bool IsValid { get; set; }

        /// <summary>
        ///     菜单代码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        ///     权限分类（枚举：前端，后台）
        /// </summary>
        public virtual MenuCategoryEnum? Category { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public virtual string Layer { get; set; }

        /// <summary>
        /// 是否存在子集
        /// </summary>
        public virtual bool HasLevel { get; set; }

        /// <summary>
        ///  排序
        /// </summary>
        public virtual int Sort { get; set; } 
        /// <summary>
        ///     创建时间
        /// </summary>
        public virtual string CreationTime { get; set; }     
        #endregion 属性

        #region 导航属性

        /// <summary>
        ///     MenuAppAuthorizes外键属性
        /// </summary>
        public virtual ICollection<MenuAppAuthorize> MenuAppAuthorizes { get; set; }

        /// <summary>
        ///     Roles外键属性
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        ///     Childrens外键属性
        /// </summary>
        public virtual ICollection<Menu> Childrens { get; set; }

        /// <summary>
        ///     Parent外键属性
        /// </summary>
        public virtual Menu Parent { get; set; }

        #endregion 导航属性
    }
}