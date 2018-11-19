// 文件名：AprilMvcFilterAttribute.cs
// 
// 创建标识：温朋朋 2018-05-21 17:43
// 
// 修改标识：温朋朋2018-05-21 17:43
// 
// ------------------------------------------------------------------------------

using System.Web.Mvc;
using April.Uow.UnitOfWorks;

namespace April.EntityFramework
{
    public class AprilMvcFilterAttribute:FilterAttribute,IActionFilter
    {
        public IUnitOfWorkCompleteHandle UnitOfWorkCompleteHandle { get; set; }
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AprilMvcFilterAttribute(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }
        /// <summary>
        ///     方法执行前开启工作单元
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UnitOfWorkCompleteHandle = _unitOfWorkManager.Begin();
        }
        /// <summary>
        ///     方法执行后提交工作单元
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            UnitOfWorkCompleteHandle.Complete();
        }
    }
}