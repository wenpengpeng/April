// 文件名：WebAuditInfoProvider.cs
// 
// 创建标识：温朋朋 2018-05-23 14:44
// 
// 修改标识：温朋朋2018-05-23 14:44
// 
// ------------------------------------------------------------------------------

using System;
using System.Net;
using System.Net.Sockets;
using System.Web;
using Castle.Core.Logging;

namespace April.Web.Auditing
{
    /// <summary>
    ///     asp.net项目使用
    /// </summary>
    public class WebAuditInfoProvider:IClientInfoProvider
    {  
        public ILogger Logger { get; set; }

        public WebAuditInfoProvider()
        {
            Logger = NullLogger.Instance;
        }

        public string BrowserInfo => GetBrowserInfo();
        public string ClientIpAddress => GetClientIpAddress();
        public string ComputerName => GetComputerName();
        /// <summary>
        ///     从httpContext中获取浏览器信息
        /// </summary>
        /// <returns></returns>
        protected virtual string GetBrowserInfo()
        {
            var httpContext = HttpContext.Current;
            if (httpContext?.Request.Browser == null)
                return null;
            return $"{httpContext.Request.Browser.Browser}/{httpContext.Request.Browser.Version}/{httpContext.Request.Browser.Platform}";
        }
        /// <summary>
        ///     从httpContext中获取IP信息
        /// </summary>
        /// <returns></returns>
        protected virtual string GetClientIpAddress()
        {
            var httpContext = HttpContext.Current;
            if (httpContext?.Request.ServerVariables == null)
                return null;
            var clientIp = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                           httpContext.Request.ServerVariables["REMOTE_ADDR"];
            try
            {
                foreach (var hostAddress in Dns.GetHostAddresses(clientIp))
                {
                    if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                        return hostAddress.ToString();
                }
                foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return hostAddress.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Debug(e.ToString());
            }
            return clientIp;
        }
        /// <summary>
        ///     从httpContext中获取电脑信息
        /// </summary>
        /// <returns></returns>
        protected virtual string GetComputerName()
        {
            var httpContext = HttpContext.Current;
            if (httpContext == null || !httpContext.Request.IsLocal)
            {
                return null;
            }

            try
            {
                var clientIp = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                               httpContext.Request.ServerVariables["REMOTE_ADDR"];
                return Dns.GetHostEntry(IPAddress.Parse(clientIp)).HostName;
            }
            catch
            {
                return null;
            }
        }
    }
}