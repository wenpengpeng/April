// 文件名：JsonnetAuditSerializer.cs
// 
// 创建标识：温朋朋 2018-05-18 17:10
// 
// 修改标识：温朋朋2018-05-18 17:10
// 
// ------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace April.Web.Auditing
{
    public class JsonnetAuditSerializer:IAuditSerializer
    {
        private readonly IAuditingConfiguration _configuration;

        public JsonnetAuditSerializer(IAuditingConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        ///     那些IgnoredTypes和加了DisableAuditingAttribute的参数不进行序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize(object obj)
        {
            var options = new JsonSerializerSettings
            {
                ContractResolver = new AuditingContractResolver(_configuration.IgnoredTypes)
            };

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}