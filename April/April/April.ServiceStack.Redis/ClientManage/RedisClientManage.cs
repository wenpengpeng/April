// 文件名：AprilDbContext.cs
// 
// 创建标识：温朋朋 2018-08-11 10:35
// 
// 修改标识：温朋朋2018-08-11 10:35
// 
// ------------------------------------------------------------------------------

using System.Configuration;
using ServiceStack.Redis;

namespace April.ServiceStack.Redis.ClientManage
{
    /// <summary>
    ///     Redis缓存管理类
    /// </summary>
    public class RedisClientManage
    {
        //从配置文件中获取IP和端口号，每一个形如password@ip:port用；隔开,如果为空则赋值一个默认值
        private static readonly string RedisConnect = ConfigurationManager.AppSettings["RedisConnect"]??"127.0.0.1:6379";

        private static readonly PooledRedisClientManager PooledRedisClientManager;

        static RedisClientManage()
        {
            PooledRedisClientManager=new PooledRedisClientManager(RedisConnect.Split(';'));
        }
        /// <summary>
        ///     获取Redis客户端
        /// </summary>
        /// <returns></returns>
        public static IRedisClient GetRedisClient()
        {
            return PooledRedisClientManager.GetClient();
        }
    }
}