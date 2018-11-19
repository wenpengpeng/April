// 文件名：IAprilSession.cs
// 
// 创建标识：温朋朋 2018-05-18 14:39
// 
// 修改标识：温朋朋2018-05-18 14:39
// 
// ------------------------------------------------------------------------------
namespace April.Core.Session
{
    public interface IAprilSession
    {
        /// <summary>
        ///     获取当前用户的UserId，没有用户为null
        /// </summary>
        long? UserId { get; }
    }
}