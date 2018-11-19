// 文件名：StartUp.cs
// 
// 创建标识：温朋朋 2018-06-01 11:20
// 
// 修改标识：温朋朋2018-06-01 11:20
// 
// ------------------------------------------------------------------------------

using April;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly:OwinStartup(typeof(StartUp))]
namespace April
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType= DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath=new PathString("/Account/Login"),
                LogoutPath=new PathString("/Account/Logout")
            });            
        }
    }
}