// 文件名：ClaimTypeExtensions.cs
// 
// 创建标识：温朋朋 2018-05-25 16:51
// 
// 修改标识：温朋朋2018-05-25 16:51
// 
// ------------------------------------------------------------------------------

using System.Linq;
using April.Core.Session;

namespace Domain.Core.AprilSessions
{
    public static class ClaimTypeExtensions
    {
        /// <summary>
        ///     用户权限
        /// </summary>
        public const string Permission = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/permission";

        /// <summary>
        ///     子帐号代码
        /// </summary>
        public const string LayerCode = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/layerCode";

        /// <summary>
        ///     主帐号Id
        /// </summary>
        public const string BelongUserId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/belongUserId";

        /// <summary>
        ///     帐号类型
        /// </summary>
        public const string AccountType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/accountType";

        /// <summary>
        ///     帐号类型
        /// </summary>
        public const string RealName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/realName";

        /// <summary>
        ///     会员Id类型
        /// </summary>
        public const string MemberId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/memberId";

        /// <summary>
        ///     会员编码
        /// </summary>
        public const string MemberCode = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/memberCode";

        /// <summary>
        ///     公司名称
        /// </summary>
        public const string CompanyName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/CompanyName";

        /// <summary>
        ///     是否是自营
        /// </summary>
        public const string IsSelfSupport = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/IsSelfSupport";

        /// <summary>
        ///     用户类型
        /// </summary>
        public const string MemberType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/MemberType";
        /// <summary>
        ///     获取申明值
        /// </summary>
        /// <returns></returns>
        public static string GetClaimValue(string claimType)
        {
            var claimsPrincipal = AprilPrincipalAccessor.Instance.Principal;

            var claim = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == claimType);

            return string.IsNullOrEmpty(claim?.Value) ? null : claim.Value;
        }
    }
}