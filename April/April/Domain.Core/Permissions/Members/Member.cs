// 文件名：Member.cs
// 
// 创建标识：温朋朋 2018-05-28 10:39
// 
// 修改标识：温朋朋2018-05-28 10:39
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using April.Uow.Repositories;
using Domain.Core.Enums.Users;
using Domain.Core.Permissions.Users;

namespace Domain.Core.Permissions.Members
{
    public class Member:IEntity<long>
    {

        #region 属性
        /// <summary>
        ///     ID
        /// </summary>
        public virtual long Id { get; set; }
        /// <summary>
        ///     用户基础表
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        ///     会员编码
        /// </summary>
        public virtual string MemberCode { get; set; }

        /// <summary>
        ///     公司名字
        /// </summary>
        public virtual string CompanyName { get; set; }

        /// <summary>
        ///     公司电话
        /// </summary>
        public virtual string CompayTel { get; set; }
        
        /// <summary>
        ///     采购商审核状态
        /// </summary>
        public virtual BuyerAuditEnum BuyerAudit { get; set; }

        /// <summary>
        ///     是否自营
        /// </summary>
        public virtual bool? IsSelfSupport { get; set; }

        /// <summary>
        ///     供应商审核状态
        /// </summary>
        public virtual SupplierAuditEnum SupplierAudit { get; set; }

        /// <summary>
        ///     用户类型方便用户在后台中查询
        ///     具体描述请看：<see cref="UserType" /> 枚举
        /// </summary>
        public virtual MemberTypeEnum? UserType { get; set; }

        /// <summary>
        ///     开标密码
        /// </summary>
        public virtual string BidOpeningPassword { get; set; }

        /// <summary>
        /// 交易密码
        /// </summary>
        public virtual string TransactionPassword { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public virtual string Remark { get; set; }
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