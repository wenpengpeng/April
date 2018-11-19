using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using April.Common.Json;
using April.Models.Account;
using Domain.Core.AprilSessions;
using Domain.Core.Permissions.Members;
using Domain.Core.Permissions.Users;
using Microsoft.AspNet.Identity;

namespace April.Controllers
{
    public class AccountController : AprilWebControllerBase
    {
        /// <summary>
        ///     用户管理
        /// </summary>
        private readonly UserBaseManage _userManage;
        private readonly MemberManage _memberManage;

        public AccountController(UserBaseManage userManage, MemberManage memberManage)
        {
            _userManage = userManage;
            _memberManage = memberManage;
        }

        [HttpGet]
        public ActionResult Login(string returnUrl="/Home/Index")
        {
            ViewBag.ReturnUrl = returnUrl;
            if (AprilSession.UserId > 0)
                Response.Redirect(returnUrl);
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "Home/Index")
        {
            var result = new AjaxResult();
            try
            {
                var user =await _userManage.GetUserBaseByIncludeAsync(loginModel.UsernameOrPhoneNumber);
                if (user == null)
                {
                    result.Successed = false;
                    result.Message = "账号不存在";
                    return Json(result);
                }
                UserBase belongUser;
                if (user.BelongUserId > 0)
                    belongUser = await _userManage.GetUserBaseByIdAsync(user.BelongUserId.Value);
                else
                    belongUser = user;
                //校验密码
                var passwordResult = new PasswordHasher().VerifyHashedPassword(user.PasswordHash,loginModel.Password);
                if (passwordResult != PasswordVerificationResult.Success)
                {                  
                    result.Message = "账户名与密码不匹配，请重新输入。";
                    result.Successed = false;                   
                    return Json(result);
                }
                //角色信息
                var userRoles = string.Join(",",user.Roles.Where(u=>u.IsValid).Select(x=>x.Id).ToArray());

                //用户账号类型信息
                var accountTypeObj = user.UserClaims.OrderByDescending(u => u.Id).FirstOrDefault(x=>x.ClaimType== "AccountType");

                //会员信息
                var member= await _memberManage.GetMemberByUserBaseIdAsync(belongUser.Id);
                //是否自营
                var isSelfSupport = "false";
                //会员Id
                var memberId = "0";
                //会员代码
                var memberCode = "";
                //公司名称
                var companyName = "";
                //用户类型
                var memberType = "-1";
                if (member != null)
                {
                    memberId = member.Id.ToString();
                    memberCode = member.MemberCode ?? "";
                    companyName = member.CompanyName ?? "";
                    memberType = member.UserType?.ToString() ?? "-1";

                    isSelfSupport = member.IsSelfSupport != null ? member.IsSelfSupport.ToString() : "false";
                }

                //构建Claims声明
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName), //用户名
                    new Claim(ClaimTypes.Role, userRoles), //用户角色集合
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), //用户Id
                    new Claim(ClaimTypeExtensions.RealName, user.RealName), //用户名称，无则为登录名
                    new Claim(ClaimTypeExtensions.AccountType,
                        accountTypeObj == null ? "0" : accountTypeObj.ClaimValue), //用户帐号类型
                    new Claim(ClaimTypeExtensions.BelongUserId,
                        user.BelongUserId == null ? "0" : user.BelongUserId.ToString()), //所属主帐号Id
                    new Claim(ClaimTypeExtensions.MemberId, memberId), //会员Id
                    new Claim(ClaimTypeExtensions.MemberType, memberType), //会员类型
                    new Claim(ClaimTypeExtensions.MemberCode, memberCode), //会员编码
                    new Claim(ClaimTypeExtensions.CompanyName, companyName), //公司名称
                    new Claim(ClaimTypeExtensions.IsSelfSupport, isSelfSupport), //是否自营
                    new Claim(ClaimTypes.MobilePhone, belongUser.PhoneNumber ?? "0"), //手机号
                    new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "ASP.NET Identity")
                };

                //构建身份申明（类似：登机牌，电影票等）
                var claimsIdentity = new ClaimsIdentity(claims,DefaultAuthenticationTypes.ApplicationCookie);
                //通过Owin Context获取认证管理
                var owinCtx = HttpContext.GetOwinContext();
                var authenticationManager = owinCtx.Authentication;

                //先退出登出（稳妥的做法）
                authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                //再进行登入（将登机牌给保安验证）（本质其实是构建cookie等）
                authenticationManager.SignIn(new Microsoft.Owin.Security.AuthenticationProperties{IsPersistent=false},claimsIdentity);
            }
            catch (Exception e)
            {
                result.Successed = false;
                result.Message = e.Message;
                return Json(result);
            }
            return Json(result);
        }
        /// <summary>
        ///     退出登录
        /// </summary>
        /// <returns></returns>
        public  ActionResult LogOut()
        {
            var owinCtx = HttpContext.GetOwinContext();
            var authenticationManager = owinCtx.Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);//登出
            return Redirect("/Account/Login");//跳转到登录页面
        }
    }
}