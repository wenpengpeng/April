// 文件名：JsonNetResult.cs
// 
// 创建标识：温朋朋 2018-05-30 17:02
// 
// 修改标识：温朋朋2018-05-30 17:02
// 
// ------------------------------------------------------------------------------

using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace April.Common.Json
{
    /// <summary>
    ///     mvc中返回jsonResult时用JsonNetResult替代
    /// </summary>
    public class JsonNetResult:JsonResult
    {
        public JsonSerializerSettings Settings { get; private set; }
        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore, //忽略循环引用，如果设置为Error，则遇到循环引用的时候报错
                DateFormatString = "yyyy-MM-dd HH:mm:ss", //日期格式化，mvc默认的让人看不懂
                ContractResolver =
                    new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() //json中属性开头字母小写的驼峰命名
            };
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if(context==null)
                throw new ArgumentNullException("context为空");
            //不允许Get方式
            if(JsonRequestBehavior==JsonRequestBehavior.DenyGet
                &&string.Equals(context.HttpContext.Request.HttpMethod,"GET",StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");
            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;//为ContentType设置默认值application/json
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data == null)
                return;
            var scriptSerializer= JsonSerializer.Create(Settings);
            scriptSerializer.Serialize(response.Output,Data);
        }
    }
}