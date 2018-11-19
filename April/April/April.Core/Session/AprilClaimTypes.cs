// 文件名：AprilClaimTypes.cs
// 
// 创建标识：温朋朋 2018-05-18 15:09
// 
// 修改标识：温朋朋2018-05-18 15:09
// 
// ------------------------------------------------------------------------------

using System.Security.Claims;

namespace April.Core.Session
{
    public class AprilClaimTypes
    {
        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.Name"/>
        /// </summary>
        public static string UserName { get; set; } = ClaimTypes.Name;

        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.NameIdentifier"/>
        /// </summary>
        public static string UserId { get; set; } = ClaimTypes.NameIdentifier;

        /// <summary>
        /// UserId.
        /// Default: <see cref="ClaimTypes.Role"/>
        /// </summary>
        public static string Role { get; set; } = ClaimTypes.Role;
    }
}