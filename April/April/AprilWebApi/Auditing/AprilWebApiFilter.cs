// 文件名：AprilWebApiFilter.cs
// 
// 创建标识：温朋朋 2018-06-06 15:54
// 
// 修改标识：温朋朋2018-06-06 15:54
// 
// ------------------------------------------------------------------------------

using April.Web.Auditing;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AprilWebApi.Auditing
{
    public class AprilWebApiFilter:IActionFilter
    {
        private readonly IAuditingHelper _auditingHelper;

        public AprilWebApiFilter(IAuditingHelper auditingHelper)
        {
            _auditingHelper = auditingHelper;
        }

        public bool AllowMultiple => false;
        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var actionDescriptor = actionContext.ActionDescriptor as ReflectedHttpActionDescriptor;
            if (actionDescriptor == null || !_auditingHelper.ShouldSaveAudit(actionDescriptor?.MethodInfo, true))//此时说明不需要审计
                return await continuation();

            var auditInfo = _auditingHelper.CreateAuditInfo(
                actionContext.ActionDescriptor.ControllerDescriptor.ControllerType,
                actionDescriptor.MethodInfo,
                actionContext.ActionArguments);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                return await continuation();
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;//若发生异常则保存异常
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                await _auditingHelper.SaveAsync(auditInfo);//插入审计信息到数据库
            }
        }
        /// <summary>
        ///     是否需要审计
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool ShouldSaveAudit(HttpActionContext context)
        {            
            var actionDescriptor = context.ActionDescriptor as ReflectedHttpActionDescriptor;
            return _auditingHelper.ShouldSaveAudit(actionDescriptor?.MethodInfo,true);
        }
    }
}