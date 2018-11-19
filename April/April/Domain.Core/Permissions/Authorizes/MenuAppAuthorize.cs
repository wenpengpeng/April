// 文件名：MenuAppAuthorize.cs
// 
// 创建标识：温朋朋 2018-05-28 10:31
// 
// 修改标识：温朋朋2018-05-28 10:32
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using April.Uow.Repositories;
using Domain.Core.Permissions.Menus;
using Domain.Core.Permissions.Roles;

namespace Domain.Core.Permissions.Authorizes
{
    public class MenuAppAuthorize:IEntity<long>
    {
        /// <summary>
        ///     Id
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        ///     菜单代码
        /// </summary>
        public virtual string MenuCode { get; set; }

        /// <summary>
        ///     AppService代码
        /// </summary>
        public virtual string AppCode { get; set; }

        /// <summary>
        ///     操作代码
        /// </summary>
        public virtual string OperationCode { get; set; }

        /// <summary>
        ///     操作代码描述
        /// </summary>
        public virtual string OperationDescription { get; set; }

        /// <summary>
        /// 菜单Id
        /// </summary>
        public virtual long MenuId { get; set; }

        #region 导航属性

        /// <summary>
        ///     UserBase外键属性
        /// </summary>
        public virtual Menu Menu { get; set; }

        /// <summary>
        ///     Roles外键属性
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        #endregion
    }
}