// 文件名：UnitOfWorkInterceptor.cs
// 
// 创建标识：温朋朋 2018-05-15 10:12
// 
// 修改标识：温朋朋2018-05-15 10:12
// 
// ------------------------------------------------------------------------------

using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace April.Uow.UnitOfWorks
{
    public class UnitOfWorkInterceptor:IInterceptor
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UnitOfWorkInterceptor(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }
        /// <summary>
        ///     拦截
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            //默认都开启工作单元，所以不需UnitOfWorkAttribute那些


            if (invocation.Method.ReturnType == typeof(Task) ||
                invocation.Method.ReturnType.GetTypeInfo().IsGenericType &&
                invocation.Method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                PerformUowAsync(invocation);
            }
            else
            {
                PerformUow(invocation);
            }
        }
        /// <summary>
        ///     同步
        /// </summary>
        /// <param name="invocation"></param>
        private void PerformUow(IInvocation invocation)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                invocation.Proceed();
                uow.Complete();
            }
        }
        /// <summary>
        ///     异步
        /// </summary>
        /// <param name="invocation"></param>
        private void PerformUowAsync(IInvocation invocation)
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                invocation.Proceed();
                uow.CompleteAsync();
            }
        }
    }
}