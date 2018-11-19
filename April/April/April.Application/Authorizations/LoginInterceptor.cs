using System.Web;
using April.Common.Exceptions;
using Castle.DynamicProxy;

namespace April.Application.Authorizations
{
    /// <summary>
    ///     方法需要登陆权限才能调用
    /// </summary>
    public class LoginInterceptor: AuthorizeInterceptorBase
    {
        public LoginInterceptor(AppMethodAuthorizeAttribute appMethodAuthorizeAttribute) 
            : base(appMethodAuthorizeAttribute)
        {
        }

        public override void Authorize(IInvocation invocation)
        {
            var isAuthorized = HttpContext.Current.User.Identity.IsAuthenticated;
            if (!isAuthorized)
            {
                throw new UserFriendlyException("您需要登陆后才能执行该操作");
            }
        }
    }
}