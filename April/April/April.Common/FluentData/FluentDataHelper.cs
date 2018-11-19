// 文件名：FluentDataHelper.cs
// 
// 创建标识：温朋朋 2018-06-20 10:21
// 
// 修改标识：温朋朋2018-06-20 10:21
// 
// ------------------------------------------------------------------------------

using System.Configuration;
using FluentData;

namespace April.Common.FluentData
{
    /// <summary>
    ///   FluentDataHelper  
    /// </summary>
    public class FluentDataHelper
    {
        private static string _connectionString = "";
        /// <summary>
        ///     设置连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        public static void SetConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                _connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
        /// <summary>
        ///     创建IDbContext实例
        /// </summary>
        /// <returns></returns>
        public static IDbContext CreateInstance()
        {
            SetConnectionString(null);//设置连接字符串

            IDbContext db = new DbContext();
            db.ConnectionString(_connectionString,new SqlServerProvider());
            db.CommandTimeout(1000*60);
            db.IgnoreIfAutoMapFails(true);
            db.IsolationLevel(IsolationLevel.ReadCommitted);

            return db;
        }
    }
}