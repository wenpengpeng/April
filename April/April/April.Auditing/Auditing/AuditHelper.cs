// 文件名：AuditHelper.cs
// 
// 创建标识：温朋朋 2018-05-18 15:55
// 
// 修改标识：温朋朋2018-05-18 15:55
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using April.Core.Session;
using April.Uow.UnitOfWorks;
using Castle.Core.Internal;
using Castle.Core.Logging;

namespace April.Web.Auditing
{
    public class AuditHelper:IAuditingHelper
    {
        public ILogger Logger { get; set; }
        public IAprilSession AprilSession { get; set; }
        private readonly IAuditingStore _auditingStore;
        private readonly IAuditInfoProvider _auditInfoProvider;
        private readonly IAuditingConfiguration _configuration;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IAuditSerializer _auditSerializer;

        public AuditHelper(IAuditInfoProvider auditInfoProvider, IAuditingConfiguration configuration, IUnitOfWorkManager unitOfWorkManager, IAuditSerializer auditSerializer, IAuditingStore auditingStore)
        {
            _auditInfoProvider = auditInfoProvider;
            _configuration = configuration;
            _unitOfWorkManager = unitOfWorkManager;
            _auditSerializer = auditSerializer;
            _auditingStore = auditingStore;                     
            Logger = NullLogger.Instance;
        }
        /// <summary>
        ///     是否需要审计
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public bool ShouldSaveAudit(MethodInfo methodInfo, bool defaultValue = false)
        {
            if (!_configuration.IsEnabled)
                return false;
            if (!_configuration.IsEnabledForAnonymousUsers && (AprilSession?.UserId == null))
                return false;
            if (methodInfo == null)
                return false;
            if (!methodInfo.IsPublic)
                return false;
            if (methodInfo.IsDefined(typeof(AuditedAttribute), true))
                return true;
            if (methodInfo.IsDefined(typeof(DisableAuditingAttribute), true))
                return false;


            var classType = methodInfo.DeclaringType;//获取类的类型
            if (classType != null)
            {
                if (classType.GetTypeInfo().IsDefined(typeof(AuditedAttribute), true))//类上是否声明了AuditedAttribute
                    return true;
                if (classType.GetTypeInfo().IsDefined(typeof(DisableAuditingAttribute), true))//类上是否声明了DisableAuditingAttribute
                    return false;
                if (_configuration.Selectors.Any(s => s.Predicate(classType)))//判断配置中是否需要为该类添加审计
                    return true;
            }
            return defaultValue;
        }
        /// <summary>
        ///     创建AuditInfo
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method">被审计的方法信息</param>
        /// <param name="arguments">参数数组</param>
        /// <returns></returns>
        public AuditInfo CreateAuditInfo(Type type, MethodInfo method, object[] arguments)
        {
            return CreateAuditInfo(type,method, CreateArgumentsDictionary(method, arguments));
        }
        /// <summary>
        ///     创建AuditInfo
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method">被审计的方法信息</param>
        /// <param name="arguments">参数字典</param>
        /// <returns></returns>
        public AuditInfo CreateAuditInfo(Type type, MethodInfo method, IDictionary<string, object> arguments)
        {
            var auditInfo = new AuditInfo
            {
                TenantId = null,
                UserId = AprilSession?.UserId,
                ImpersonatorTenantId = null,
                ServiceName = type != null ? type.FullName : "",
                MethodName = method.Name,
                Parameters = ConvertArgumentsToJson(arguments),
                ExecutionTime = DateTime.Now
            };

            try
            {
                _auditInfoProvider.Fill(auditInfo);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(),ex);
            }
            return auditInfo;
        }

        public void Save(AuditInfo auditInfo)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                _auditingStore.Save(auditInfo);
                uow.Complete();
            }
        }

        public async Task SaveAsync(AuditInfo auditInfo)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                await _auditingStore.SaveAsync(auditInfo);
                await uow.CompleteAsync();
            }
        }
        /// <summary>
        ///     参数转换为json字符串
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private string ConvertArgumentsToJson(IDictionary<string, object> arguments)
        {
            try
            {
                if (arguments.IsNullOrEmpty())
                    return "{}";

                var dictionary = new Dictionary<string, object>();
                foreach (var argument in arguments)
                {
                    if (argument.Value != null &&
                        _configuration.IgnoredTypes.Any(t => t.IsInstanceOfType(argument.Value))) //如果IgnoreTypes中包含该参数类型则置为null                  
                        dictionary[argument.Key] = null;                    
                    else
                        dictionary[argument.Key] = argument.Value;
                }
                return _auditSerializer.Serialize(dictionary);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
                return "{}";
            }
        }
        /// <summary>
        ///     把参数数组转换为参数字典（参数Name作为key）
        /// </summary>
        /// <param name="method"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private static Dictionary<string, object> CreateArgumentsDictionary(MethodInfo method, object[] arguments)
        {
            var parameters = method.GetParameters();
            var dictionary = new Dictionary<string, object>();

            for (var i = 0; i < parameters.Length; i++)
            {
                dictionary[parameters[i].Name] = arguments[i];
            }

            return dictionary;
        }
    }
}