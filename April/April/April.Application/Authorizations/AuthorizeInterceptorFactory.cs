namespace April.Application.Authorizations
{
    /// <summary>
    ///     简单工厂
    /// </summary>
    public class AuthorizeInterceptorFactory
    {
        public static AuthorizeInterceptorBase CreateAuthorizeInterceptor(AppMethodAuthorizeAttribute appMethodAuthorizeAttribute)
        {
            switch (appMethodAuthorizeAttribute.AppAuthorize)
            {
                case ApplicationAuthorizeEnum.Login:
                    return new LoginInterceptor(appMethodAuthorizeAttribute);
                case ApplicationAuthorizeEnum.NoLoginOperationPermission:
                    return new NoLoginOperationPermissionInterceptor(appMethodAuthorizeAttribute);
                case ApplicationAuthorizeEnum.OperationPermission:
                    return new OperationPermissionInerceptor(appMethodAuthorizeAttribute);
                default:
                    return null;
            }
        }
    }
}