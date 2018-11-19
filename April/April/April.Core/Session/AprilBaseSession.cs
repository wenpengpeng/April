// 文件名：AprilBaseSession.cs
// 
// 创建标识：温朋朋 2018-05-18 14:55
// 
// 修改标识：温朋朋2018-05-18 14:55
// 
// ------------------------------------------------------------------------------
namespace April.Core.Session
{
    public abstract class AprilBaseSession:IAprilSession
    {
        public abstract long? UserId { get; }
    }
}