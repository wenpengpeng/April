// 文件名：ResultWrapHandler.cs
// 
// 创建标识：温朋朋 2018-06-06 16:58
// 
// 修改标识：温朋朋2018-06-06 16:58
// 
// ------------------------------------------------------------------------------

using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using April.Common.Json;

namespace AprilWebApi.Handler
{
    /// <summary>
    ///     包裹api返回的结果
    /// </summary>
    public class ResultWrapHandler:DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request,cancellationToken);

            WrapResult(request,result);
            return result;
        }
        /// <summary>
        ///     包裹结果
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        protected virtual void WrapResult(HttpRequestMessage request, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode) //如果http响应不成功则直接退出
                return;

            object resultObject;
            if (!response.TryGetContentValue(out resultObject) || resultObject == null)//没有获取到结果值或者获取到的结果值为null则赋一个
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new ObjectContent<AjaxResult>(new AjaxResult(), new JsonMediaTypeFormatter());
                return;
            }

            //有结果值则直接包裹起来
            response.Content = new ObjectContent<AjaxResult>(new AjaxResult{Successed=true,Result=resultObject},new JsonMediaTypeFormatter());
        }
    }
}