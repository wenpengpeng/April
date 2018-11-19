// 文件名：AuditInfo.cs
// 
// 创建标识：温朋朋 2018-05-18 13:39
// 
// 修改标识：温朋朋2018-05-18 13:39
// 
// ------------------------------------------------------------------------------

using System;

namespace April.Web.Auditing
{
    public class AuditInfo
    {
        /// <summary>
        ///     TenantId
        /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        ///     UserId
        /// </summary>
        public long? UserId { get; set; }
        /// <summary>
        /// ImpersonatorTenantId.
        /// </summary>
        public int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// Service (class/interface) name.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Executed method name.
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Calling parameters.
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// Start time of the method execution.
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// Total duration of the method call.
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// IP address of the client.
        /// </summary>
        public string ClientIpAddress { get; set; }

        /// <summary>
        /// Name (generally computer name) of the client.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Browser information if this method is called in a web request.
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// Optional custom data that can be filled and used.
        /// </summary>
        public string CustomData { get; set; }

        /// <summary>
        /// Exception object, if an exception occurred during execution of the method.
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        ///     重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var loggedUserId = UserId.HasValue ? "user" + UserId.Value : "an anonymous";
            var exceptionOrSuccessMessage = Exception != null ? "excetion" + Exception.Message : "succed";
            return $"AUDIT LOG: {ServiceName}.{MethodName} is executed by {loggedUserId} in {ExecutionDuration} ms from {ClientIpAddress} IP address with {exceptionOrSuccessMessage}.";
        }
    }
}