// 文件名：AprilSessionExtensions.cs
// 
// 创建标识：温朋朋 2018-05-25 16:38
// 
// 修改标识：温朋朋2018-05-25 16:38
// 
// ------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Security.Claims;
using April.Core.Session;
using Domain.Core.Enums.Users;

namespace Domain.Core.AprilSessions
{
    public class AprilSessionExtensions:ClaimsAprilSession,IAprilSessionExtensions
    {
        public AprilSessionExtensions(IPrincipalAccessor principalAccessor) 
            : base(principalAccessor)
        {
        }
        /// <summary>
        ///     用户角色
        /// </summary>
        public string UserRoles => GetClaimValue(ClaimTypes.Role);
        /// <summary>
        ///     用户权限
        /// </summary>
        public string UserPermissions => GetClaimValue(ClaimTypeExtensions.Permission);
        /// <summary>
        ///     主账号ID
        /// </summary>

        public long BelongUserId => !string.IsNullOrWhiteSpace(GetClaimValue(ClaimTypeExtensions.BelongUserId))
            ? Convert.ToInt64(GetClaimValue(ClaimTypeExtensions.BelongUserId))
            : 0;
        /// <summary>
        ///     账户类型
        /// </summary>
        public AccountTypeEnum AccountType => (AccountTypeEnum) Enum.Parse(typeof(AccountTypeEnum),GetClaimValue(ClaimTypeExtensions.AccountType));

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName => GetClaimValue(ClaimTypes.Name);

        /// <summary>
        ///     真实名称
        /// </summary>
        public string RealName => GetClaimValue(ClaimTypeExtensions.RealName);
        /// <summary>
        ///     会员ID
        /// </summary>
        public long MemberId => !string.IsNullOrWhiteSpace(GetClaimValue(ClaimTypeExtensions.MemberId))
            ? Convert.ToInt64(GetClaimValue(ClaimTypeExtensions.MemberId))
            : 0;

        /// <summary>
        ///     手机号码
        /// </summary>
        public string MobilePhone => GetClaimValue(ClaimTypes.MobilePhone);

        /// <summary>
        ///     会员编码
        /// </summary>
        public string MemberCode => GetClaimValue(ClaimTypeExtensions.MemberCode);

        /// <summary>
        ///     公司名称
        /// </summary>
        public string CompanyName => GetClaimValue(ClaimTypeExtensions.CompanyName);

        /// <summary>
        ///     是否自营
        /// </summary>
        public bool IsSelfSupport => Convert.ToBoolean(GetClaimValue(ClaimTypeExtensions.IsSelfSupport));
        /// <summary>
        ///     会员类型
        /// </summary>
        public MemberTypeEnum MemberType => (MemberTypeEnum)
            Enum.Parse(typeof(MemberTypeEnum), GetClaimValue(ClaimTypeExtensions.MemberType));
        /// <summary>
        ///     从Claims中获取值
        /// </summary>
        /// <param name="claimType"></param>
        /// <returns></returns>
        private string GetClaimValue(string claimType)
        {
            var claimPrincipal = PrincipalAccessor.Principal;
            var claim = claimPrincipal?.Claims.FirstOrDefault(c=>c.Type==claimType);
            return string.IsNullOrEmpty(claim?.Value) ? "" : claim.Value;
        }
    }
}