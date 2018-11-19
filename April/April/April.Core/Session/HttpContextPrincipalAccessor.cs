// 文件名：HttpContextPrincipalAccessor.cs
// 
// 创建标识：温朋朋 2018-05-25 17:42
// 
// 修改标识：温朋朋2018-05-25 17:42
// 
// ------------------------------------------------------------------------------
using System.Security.Claims;
using System.Web;

namespace April.Core.Session
{
    public class HttpContextPrincipalAccessor:AprilPrincipalAccessor
    {
        public override ClaimsPrincipal Principal => HttpContext.Current?.User as ClaimsPrincipal ?? base.Principal;
    }
}