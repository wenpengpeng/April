// 文件名：NullClientInfoProvider.cs
// 
// 创建标识：温朋朋 2018-05-18 15:51
// 
// 修改标识：温朋朋2018-05-18 15:51
// 
// ------------------------------------------------------------------------------

namespace April.Web.Auditing
{
    public class NullClientInfoProvider:IClientInfoProvider
    {
        public static NullClientInfoProvider Instance { get; } = new NullClientInfoProvider();
        public string BrowserInfo => null;
        public string ClientIpAddress => null;
        public string ComputerName => null;
    }
}