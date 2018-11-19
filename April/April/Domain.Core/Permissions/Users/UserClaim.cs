// 文件名：UserClaim.cs
// 
// 创建标识：温朋朋 2018-05-28 10:10
// 
// 修改标识：温朋朋2018-05-28 10:10
// 
// ------------------------------------------------------------------------------

using System;
using System.Globalization;
using April.Uow.Repositories;

namespace Domain.Core.Permissions.Users
{
    public class UserClaim:IEntity<long>
    {
        #region 属性
        /// <summary>
        ///     Id
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        ///     申明类型
        /// </summary>
        public virtual string ClaimType { get; set; }

        /// <summary>
        ///     申明值
        /// </summary>
        public virtual string ClaimValue { get; set; }

        /// <summary>
        ///     UserId
        /// </summary>
        public virtual long UserId { get; set; }       
        /// <summary>
        ///     创建时间
        /// </summary>
        public virtual string CreationTime { get; set; } = DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture);

        #endregion 属性

        #region 导航属性

        /// <summary>
        ///     UserBase外键属性
        /// </summary>
        public virtual UserBase UserBase { get; set; }

        #endregion 导航属性

    }
}