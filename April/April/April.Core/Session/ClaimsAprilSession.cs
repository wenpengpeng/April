// 文件名：ClaimsAprilSession.cs
// 
// 创建标识：温朋朋 2018-05-18 14:58
// 
// 修改标识：温朋朋2018-05-18 14:58
// 
// ------------------------------------------------------------------------------

using System.Linq;

namespace April.Core.Session
{
    public class ClaimsAprilSession:AprilBaseSession
    {
        protected IPrincipalAccessor PrincipalAccessor { get; }
        public ClaimsAprilSession(IPrincipalAccessor principalAccessor)
        {
            PrincipalAccessor = principalAccessor;
        }

        public override long? UserId
        {
            get
            {
                var userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c=>c.Type==AprilClaimTypes.UserId);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }
                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }
                return userId;
            }
        }
        
    }
}