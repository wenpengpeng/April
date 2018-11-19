using Castle.DynamicProxy;

namespace April.Application.Authorizations
{
    /// <summary>
    ///     不需登陆就可调用的方法
    /// </summary>
    public class NoLoginOperationPermissionInterceptor: AuthorizeInterceptorBase
    {
        public override void Authorize(IInvocation invocation)
        {
            //什么也不做
        }

        public NoLoginOperationPermissionInterceptor(AppMethodAuthorizeAttribute appMethodAuthorizeAttribute) 
            : base(appMethodAuthorizeAttribute)
        {
        }
    }
}