// 文件名：Manager.cs
// 
// 创建标识：温朋朋 2018-05-28 10:35
// 
// 修改标识：温朋朋2018-05-28 10:35
// 
// ------------------------------------------------------------------------------

using System;
using System.Globalization;
using April.Uow.Repositories;
using Domain.Core.Permissions.Users;

namespace Domain.Core.Permissions.Managers
{
    public class Manager:IEntity<long>
    {
        /// <summary>
        ///     Id
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        ///     用户基础表
        /// </summary>
        public virtual long UserId { get; set; }
        /// <summary>
        ///     创建时间
        /// </summary>
        public virtual string CreationTime { get; set; } = DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture);
        #region 导航属性

        /// <summary>
        ///     UserBase外键属性
        /// </summary>
        public virtual UserBase UserBase { get; set; }

        #endregion 导航属性
    }
}