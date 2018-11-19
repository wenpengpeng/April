// 文件名：ActionDescriptorExtensions.cs
// 
// 创建标识：温朋朋 2018-06-05 16:40
// 
// 修改标识：温朋朋2018-06-05 16:40
// 
// ------------------------------------------------------------------------------

using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace April.Web.Extensions
{
    public static class ActionDescriptorExtensions
    {
        /// <summary>
        ///     获取方法的methodInfo
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public static MethodInfo GetMethodInfoOrNull(this ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ReflectedActionDescriptor)
                return ((ReflectedActionDescriptor) actionDescriptor).MethodInfo;

            if(actionDescriptor is ReflectedAsyncActionDescriptor)
                return ((ReflectedAsyncActionDescriptor)actionDescriptor).MethodInfo;

            if(actionDescriptor is TaskAsyncActionDescriptor)
                return ((TaskAsyncActionDescriptor)actionDescriptor).MethodInfo;

            return null;
        }
    }
}