// 文件名：NullAprilSession.cs
// 
// 创建标识：温朋朋 2018-05-18 15:23
// 
// 修改标识：温朋朋2018-05-18 15:23
// 
// ------------------------------------------------------------------------------
namespace April.Core.Session
{
    public class NullAprilSession:AprilBaseSession
    {
        public static NullAprilSession Instance { get; } = new NullAprilSession();
        public override long? UserId =>null;
    }
}