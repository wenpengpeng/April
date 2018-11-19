// 文件名：IPrincipalAccessor.cs
// 
// 创建标识：温朋朋 2018-05-18 14:50
// 
// 修改标识：温朋朋2018-05-18 14:50
// 
// ------------------------------------------------------------------------------

using System.Security.Claims;

namespace April.Core.Session
{
    public interface IPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
    }
}