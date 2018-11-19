// 文件名：JsonNetActionFilter.cs
// 
// 创建标识：温朋朋 2018-05-30 17:14
// 
// 修改标识：温朋朋2018-05-30 17:14
// 
// ------------------------------------------------------------------------------

using System.Web.Mvc;

namespace April.Common.Json
{
    public class JsonNetActionFilter:IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //通过Aop将JsonResult替换为JsonNetResult
            if (filterContext.Result is JsonResult && !(filterContext.Result is JsonNetResult))
            {
                var jsonResult = (JsonResult) filterContext.Result;
                var jsonNetResult = new JsonNetResult
                {
                    ContentEncoding = jsonResult.ContentEncoding,
                    ContentType = jsonResult.ContentType,
                    Data = jsonResult.Data,
                    JsonRequestBehavior = jsonResult.JsonRequestBehavior,
                    MaxJsonLength = jsonResult.MaxJsonLength,
                    RecursionLimit = jsonResult.RecursionLimit
                };
                filterContext.Result = jsonNetResult;//将结果替换为jsonNetResult
            }
        }
    }
}