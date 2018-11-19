// 文件名：AprilMvcAuditFilter.cs
// 
// 创建标识：温朋朋 2018-06-05 16:35
// 
// 修改标识：温朋朋2018-06-05 16:35
// 
// ------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Web.Mvc;
using April.Web.Extensions;

namespace April.Web.Auditing
{
    public class AprilMvcAuditFilter : IActionFilter
    {
        private readonly IAuditingHelper _auditingHelper;

        public AprilMvcAuditFilter(IAuditingHelper auditingHelper)
        {
            _auditingHelper = auditingHelper;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!ShouldSaveAudit(filterContext))
            {
                AprilAuditFilterData.Set(filterContext.HttpContext, null);//如果不需要审计则保存一个null
                return;
            }

            var auditInfo = _auditingHelper.CreateAuditInfo(filterContext.ActionDescriptor.ControllerDescriptor.ControllerType,
                filterContext.ActionDescriptor.GetMethodInfoOrNull(),
                filterContext.ActionParameters);

            var stopwatch = Stopwatch.StartNew();

            AprilAuditFilterData.Set(filterContext.HttpContext,new AprilAuditFilterData(stopwatch,auditInfo));//将AprilAuditFilterData保存到httpContext.item中
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var auditData = AprilAuditFilterData.GetOrNull(filterContext.HttpContext);//从httpContext中获取AprilAuditFilterData

            if (auditData == null)//如果为空则说明不需要审计
                return;

            auditData.Stopwatch.Stop();

            //更新auditInfo实体
            auditData.AuditInfo.ExecutionDuration = Convert.ToInt32(auditData.Stopwatch.Elapsed.TotalMilliseconds);
            auditData.AuditInfo.Exception = filterContext.Exception;

            _auditingHelper.Save(auditData.AuditInfo);//保存

        }
        /// <summary>
        ///     是否需要审计
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool ShouldSaveAudit(ActionExecutingContext filterContext)
        {
            var currentMethodInfo = filterContext.ActionDescriptor.GetMethodInfoOrNull();

            if (currentMethodInfo == null)
                return false;

            return _auditingHelper.ShouldSaveAudit(currentMethodInfo, true);//默认为true
        }
    }
}