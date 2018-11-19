// 文件名：AuditingInterceptor.cs
// 
// 创建标识：温朋朋 2018-05-18 17:36
// 
// 修改标识：温朋朋2018-05-18 17:36
// 
// ------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace April.Web.Auditing
{
    /// <summary>
    ///     审计拦截器
    /// </summary>
    public class AuditingInterceptor:IInterceptor
    {
        private readonly IAuditingHelper _auditingHelper;

        public AuditingInterceptor(IAuditingHelper auditingHelper)
        {
            _auditingHelper = auditingHelper;
        }

        public void Intercept(IInvocation invocation)
        {
            if (!_auditingHelper.ShouldSaveAudit(invocation.MethodInvocationTarget))
            {
                invocation.Proceed();
                return;
            }
            var auditInfo = _auditingHelper.CreateAuditInfo(invocation.TargetType,invocation.MethodInvocationTarget,invocation.Arguments);

            if (invocation.Method.ReturnType == typeof(Task) ||
                (invocation.Method.ReturnType.GetTypeInfo().IsGenericType && invocation.Method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>)))
            {
                PerformAsyncAuditing(invocation, auditInfo);
            }
            else
            {
                PerformSyncAuditing(invocation, auditInfo);
            }
        }
        private void PerformSyncAuditing(IInvocation invocation, AuditInfo auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                auditInfo.Exception = ex;
                throw;
            }
            finally
            {
                stopwatch.Stop();
                auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);
                _auditingHelper.Save(auditInfo);
            }
        }

        private void PerformAsyncAuditing(IInvocation invocation, AuditInfo auditInfo)
        {
            var stopwatch = Stopwatch.StartNew();

            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = AwaitTaskWithFinally(
                    (Task)invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, stopwatch, exception)
                );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = CallAwaitTaskWithFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    exception => SaveAuditInfo(auditInfo, stopwatch, exception)
                );
            }
        }

        private void SaveAuditInfo(AuditInfo auditInfo, Stopwatch stopwatch, Exception exception)
        {
            stopwatch.Stop();
            auditInfo.Exception = exception;
            auditInfo.ExecutionDuration = Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds);

            _auditingHelper.Save(auditInfo);
        }
        private static async Task AwaitTaskWithFinally(Task actualReturnValue, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                await actualReturnValue;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }
        public static object CallAwaitTaskWithFinallyAndGetResult(Type taskReturnType, object actualReturnValue, Action<Exception> finalAction)
        {
            var methodInfo = typeof(AuditingInterceptor)
                .GetMethod("AwaitTaskWithFinallyAndGetResult", BindingFlags.Static);
            if (methodInfo != null)
                return methodInfo
                    .MakeGenericMethod(taskReturnType)
                    .Invoke(null, new object[] {actualReturnValue, finalAction});
            return null;
        }
    }
}