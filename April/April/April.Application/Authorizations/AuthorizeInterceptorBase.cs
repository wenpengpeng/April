using Castle.DynamicProxy;

namespace April.Application.Authorizations
{
    /// <summary>
    ///     接口授权基类
    /// </summary>
    public abstract class AuthorizeInterceptorBase
    {
        public AppMethodAuthorizeAttribute AppMethodAuthorizeAttribute { get; set; }
        protected AuthorizeInterceptorBase(AppMethodAuthorizeAttribute appMethodAuthorizeAttribute)
        {
            AppMethodAuthorizeAttribute = appMethodAuthorizeAttribute;
        }
        public abstract void Authorize(IInvocation invocation);
    }
}