// 文件名：IAprilSessionExtensions.cs
// 
// 创建标识：温朋朋 2018-05-25 16:25
// 
// 修改标识：温朋朋2018-05-25 16:25
// 
// ------------------------------------------------------------------------------

using April.Core.Session;
using Domain.Core.Enums.Users;

namespace Domain.Core.AprilSessions
{
    public interface IAprilSessionExtensions:IAprilSession
    {
        /// <summary>
        ///     用户角色
        /// </summary>
        string UserRoles { get; }
        /// <summary>
        ///     用户权限
        /// </summary>
        string UserPermissions { get; }
        /// <summary>
        ///     主账号Id
        /// </summary>
        long BelongUserId { get; }
        /// <summary>
        ///     账户类型
        /// </summary>
        AccountTypeEnum AccountType { get; }
        /// <summary>
        ///     用户名
        /// </summary>
        string UserName { get; }
        /// <summary>
        ///     真实名字
        /// </summary>
        string RealName { get; }
        /// <summary>
        ///     会员Id
        /// </summary>
        long MemberId { get; }
        /// <summary>
        ///     手机号码
        /// </summary>
        string MobilePhone { get; }
        /// <summary>
        ///     会员编号
        /// </summary>
        string MemberCode { get; }
        /// <summary>
        ///     公司名称
        /// </summary>
        string CompanyName { get; }
        /// <summary>
        ///     当前账号是否自营
        /// </summary>
        bool IsSelfSupport { get; }
        /// <summary>
        ///     会员类型
        /// </summary>
        MemberTypeEnum MemberType { get; }

    }
}