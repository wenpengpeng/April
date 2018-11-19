// 文件名：AprilAuditInfoProvider.cs
// 
// 创建标识：温朋朋 2018-05-18 15:44
// 
// 修改标识：温朋朋2018-05-18 15:44
// 
// ------------------------------------------------------------------------------

using Castle.Core.Internal;

namespace April.Web.Auditing
{
    public class AprilAuditInfoProvider:IAuditInfoProvider
    {
        private readonly IClientInfoProvider _clientInfoProvider;

        public AprilAuditInfoProvider(IClientInfoProvider clientInfoProvider)
        {
            _clientInfoProvider = clientInfoProvider;           
        }
        public void Fill(AuditInfo auditInfo)
        {
            if (auditInfo.ClientIpAddress.IsNullOrEmpty())
            {
                auditInfo.ClientIpAddress = _clientInfoProvider.ClientIpAddress;
            }

            if (auditInfo.BrowserInfo.IsNullOrEmpty())
            {
                auditInfo.BrowserInfo = _clientInfoProvider.BrowserInfo;
            }

            if (auditInfo.ClientName.IsNullOrEmpty())
            {
                auditInfo.ClientName = _clientInfoProvider.ComputerName;
            }
        }
    }
}