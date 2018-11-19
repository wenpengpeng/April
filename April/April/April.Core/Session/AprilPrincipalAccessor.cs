// 文件名：AprilPrincipalAccessor.cs
// 
// 创建标识：温朋朋 2018-05-18 15:19
// 
// 修改标识：温朋朋2018-05-18 15:19
// 
// ------------------------------------------------------------------------------

using System.Security.Claims;
using System.Threading;

namespace April.Core.Session
{
    public class AprilPrincipalAccessor:IPrincipalAccessor
    {
        public virtual ClaimsPrincipal Principal => Thread.CurrentPrincipal as ClaimsPrincipal;
        public static AprilPrincipalAccessor Instance => new AprilPrincipalAccessor();
    }
}