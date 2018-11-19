using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace April.Application.Authorizations
{
    /// <summary>
    ///     Service方法授权AOP
    /// </summary>
    public class ApplicationServiceIntercept: IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (invocation.GetConcreteMethodInvocationTarget()
                .CustomAttributes
                .Any(x => x.AttributeType ==
                          typeof(AppMethodAuthorizeAttribute)))
            {
                var appMethodAuthorizeAttribute = invocation
                        .GetConcreteMethodInvocationTarget()
                        .GetCustomAttribute(typeof(AppMethodAuthorizeAttribute))
                    as AppMethodAuthorizeAttribute;

                AuthorizeInterceptorFactory.CreateAuthorizeInterceptor(appMethodAuthorizeAttribute)
                    .Authorize(invocation);
            }
            invocation.Proceed();
        }
    }
}