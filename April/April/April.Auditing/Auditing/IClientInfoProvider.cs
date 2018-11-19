// 文件名：IClientInfoProvider.cs
// 
// 创建标识：温朋朋 2018-05-18 15:48
// 
// 修改标识：温朋朋2018-05-18 15:48
// 
// ------------------------------------------------------------------------------
namespace April.Web.Auditing
{
    public interface IClientInfoProvider
    {
        /// <summary>
        ///     浏览器信息
        /// </summary>
        string BrowserInfo { get; }
        /// <summary>
        ///     客户端Ip地址
        /// </summary>
        string ClientIpAddress { get; }
        /// <summary>
        ///     电脑名称
        /// </summary>
        string ComputerName { get; }
    }
}