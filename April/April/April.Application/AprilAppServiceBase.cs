// 文件名：AprilAppServiceBase.cs
// 
// 创建标识：温朋朋 2018-06-21 14:09
// 
// 修改标识：温朋朋2018-06-21 14:09
// 
// ------------------------------------------------------------------------------

using April.Application.Authorizations;
using April.Core.Ioc;
using April.Web.Services;
using Autofac.Extras.DynamicProxy;
using Domain.Core.AprilSessions;

namespace April.Application
{
    public class AprilAppServiceBase:IApplicationService
    {        
        public AprilAppServiceBase()
        {
            
        }
        public IAprilSessionExtensions AprilSession => IocManager.Instance.Resolve<IAprilSessionExtensions>();
    }
}