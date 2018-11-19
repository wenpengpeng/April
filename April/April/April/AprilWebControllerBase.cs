// 文件名：AprilWebControllerBase.cs
// 
// 创建标识：温朋朋 2018-05-30 14:24
// 
// 修改标识：温朋朋2018-05-30 14:24
// 
// ------------------------------------------------------------------------------

using Domain.Core.AprilSessions;
using System.Web.Mvc;

namespace April
{
    public class AprilWebControllerBase:Controller
    {
        public AprilWebControllerBase()
        {
            
        }
        public IAprilSessionExtensions AprilSession { get; set; }
    }
}