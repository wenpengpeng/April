// 文件名：AprilWebApiUowFilter.cs
// 
// 创建标识：温朋朋 2018-06-06 16:51
// 
// 修改标识：温朋朋2018-06-06 16:51
// 
// ------------------------------------------------------------------------------

using April.Uow.UnitOfWorks;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AprilWebApi.Uow
{
    public class AprilWebApiUowFilter:IActionFilter
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AprilWebApiUowFilter(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        public bool AllowMultiple => false;
        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var actionDescriptor = actionContext.ActionDescriptor as ReflectedHttpActionDescriptor;
            if (actionDescriptor == null)
                return await continuation();

            //开启工作单元
            using (var uow = _unitOfWorkManager.Begin())
            {
                var result = await continuation();
                await uow.CompleteAsync();
                return result;
            }
        }
    }
}