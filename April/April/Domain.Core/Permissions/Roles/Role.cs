// 文件名：Role.cs
// 
// 创建标识：温朋朋 2018-05-28 10:16
// 
// 修改标识：温朋朋2018-05-28 10:16
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using April.Uow.Repositories;
using Domain.Core.Enums.Users;
using Domain.Core.Permissions.Authorizes;
using Domain.Core.Permissions.Menus;
using Domain.Core.Permissions.Users;
using Microsoft.AspNet.Identity;

namespace Domain.Core.Permissions.Roles
{
    public class Role: ChildAccount.ChildAccount, IRole<long>
    {
        #region 属性        
        /// <summary>
        ///     角色名字
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     角色代码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public virtual bool IsValid { get; set; }

        /// <summary>
        ///     默认管理员类别
        /// </summary>
        public virtual DefaultRoleTypeEnum? DefaultRoleType { get; set; }

        /// <summary>
        ///     是否系统角色（不允许修改）
        /// </summary>
        public virtual bool IsSystem { get; set; }       

        public virtual string CreationTime { get; set; } = DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture);

        #endregion 属性

        #region 导航属性

        /// <summary>
        ///     UserBases外键属性
        /// </summary>
        public virtual ICollection<UserBase> UserBases { get; set; }

        /// <summary>
        ///     MenuAppAuthorizes外键属性
        /// </summary>
        public virtual ICollection<MenuAppAuthorize> MenuAppAuthorizes { get; set; }

        /// <summary>
        ///     Menus外键属性
        /// </summary>
        public virtual ICollection<Menu> Menus { get; set; }

        #endregion 导航属性

       
    }
}