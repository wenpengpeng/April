// 文件名：AprilWebApiBaseController.cs
// 
// 创建标识：温朋朋 2018-06-06 17:34
// 
// 修改标识：温朋朋2018-06-06 17:34
// 
// ------------------------------------------------------------------------------

using Domain.Core.AprilSessions;
using System.Web.Http;

namespace AprilWebApi
{
    /// <summary>
    ///     apiBaseController
    /// </summary>
    public class AprilWebApiBaseController:ApiController
    {
        /// <summary>
        ///     AprilSession
        /// </summary>
        public IAprilSessionExtensions AprilSession { get; set; }
        /// <summary>
        ///     公共构造函数
        /// </summary>
        public AprilWebApiBaseController()
        {
            
        }
    }
}