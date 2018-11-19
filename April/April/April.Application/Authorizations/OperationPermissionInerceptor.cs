using System;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using April.Common.Exceptions;
using April.Common.FluentData;
using Castle.DynamicProxy;
using Domain.Core.AprilSessions;
using Domain.Core.Enums.Users;

namespace April.Application.Authorizations
{
    /// <summary>
    ///     每个具体的操作方法授权
    /// </summary>
    public class OperationPermissionInerceptor:AuthorizeInterceptorBase
    {
        public OperationPermissionInerceptor(AppMethodAuthorizeAttribute appMethodAuthorizeAttribute) 
            : base(appMethodAuthorizeAttribute)
        {
        }
        /// <summary>
        ///     判定当前登陆账号的角色是否有该操作方法的授权
        /// </summary>
        /// <param name="invocation"></param>
        public override void Authorize(IInvocation invocation)
        {
            var isAuthorized = HttpContext.Current.User.Identity.IsAuthenticated;
            if(!isAuthorized)
                throw new UserFriendlyException("您未登陆，不能执行该操作");
            //如果是超级管理员则跳过不验证
            var accountType =(AccountTypeEnum)Convert.ToInt32(ClaimTypeExtensions.GetClaimValue(ClaimTypeExtensions.AccountType));
            if(accountType== AccountTypeEnum.超级管理员)
                return;
            //从请求头中获取当前菜单的menuCode
            var menuCode = HttpContext.Current.Request.Headers["menuCode"];
            //如果为home则为工作台，只需登陆权限即可
            if (menuCode == "home")
                return;

            //如果是其他的menuCode则判定当前用户的角色是否有该菜单下的调用操作方法的权限

            //如果角色为空则直接不允许调用
            var roles = ClaimTypeExtensions.GetClaimValue(ClaimTypes.Role);//登陆时构造好了
            if(string.IsNullOrWhiteSpace(roles))
                throw new UserFriendlyException("当前用户未拥有任何角色");
            //如果service类上没打上AppAuthorizeAttribute特性则不验证直接退出
            var appAuthorizeAttribute = invocation.TargetType.GetCustomAttribute
                (typeof(AppAuthorizeAttribute)) as AppAuthorizeAttribute;
            if (appAuthorizeAttribute == null)
                return;
            var sql = $@"select  count(1)
                            from MenuAppAuthorizeRoles
                            where Role_Id in ({roles})  AND MenuAppAuthorize_Id in(
                            select Id from MenuAppAuthorizes where
                            MenuCode = '{menuCode}' and AppCode =
                            '{appAuthorizeAttribute.Code}'
                            and OperationCode = '{AppMethodAuthorizeAttribute.Action}')";

            try
            {
                using (var db = FluentDataHelper.CreateInstance())
                {
                    var data = db.Sql(sql)
                        .QuerySingle<int>();
                    if (data <= 0)
                        throw new UserFriendlyException
                            ($"用户缺少{AppMethodAuthorizeAttribute.Description}权限！");
                }
            }
            catch (Exception e)
            {
                throw new UserFriendlyException
                    (e.Message);
            }
        }
    }
}