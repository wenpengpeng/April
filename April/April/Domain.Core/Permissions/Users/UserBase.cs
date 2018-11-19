// 文件名：UserBase.cs
// 
// 创建标识：温朋朋 2018-05-28 10:00
// 
// 修改标识：温朋朋2018-05-28 10:00
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using April.Uow.Repositories;
using Domain.Core.Permissions.Managers;
using Domain.Core.Permissions.Members;
using Domain.Core.Permissions.Roles;
using Microsoft.AspNet.Identity;

namespace Domain.Core.Permissions.Users
{
    public class UserBase:ChildAccount.ChildAccount, IUser<long>
    {        
        /// <summary>
        ///     角色名称
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        ///     是否锁定
        /// </summary>
        public virtual bool IsLockoutEnaled { get; set; }

        /// <summary>
        ///     访问失败次数
        /// </summary>
        public virtual int? AccessFailedCount { get; set; }

        /// <summary>
        ///     安全标记
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        ///     加密密码
        /// </summary>
        public virtual string PasswordHash { get; set; }

        /// <summary>
        ///     是否邮箱已验证
        /// </summary>
        public virtual bool IsEmailComfirmed { get; set; }

        /// <summary>
        ///     邮箱
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        ///     电话号码
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        ///     是否电话已验证
        /// </summary>
        public virtual bool IsPhoneNumberComfirmed { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        public virtual string RealName { get; set; }

        /// <summary>
        ///     是否关联专家
        /// </summary>
        public virtual bool IsRelationExpert { get; set; }
        /// <summary>
        ///     创建时间
        /// </summary>
        public virtual string CreationTime { get; set; } = DateTime.Now.ToLocalTime().ToString(CultureInfo.InvariantCulture);


        #region 导航属性

        /// <summary>
        ///     Roles外键属性
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        ///     Managers外键属性
        /// </summary>
        public virtual ICollection<Manager> Managers { get; set; }

        /// <summary>
        ///     Members外键属性
        /// </summary>
        public virtual ICollection<Member> Members { get; set; }

        /// <summary>
        ///     UserClaims外键属性
        /// </summary>
        public virtual ICollection<UserClaim> UserClaims { get; set; }

        #endregion 导航属性
       
    }
}