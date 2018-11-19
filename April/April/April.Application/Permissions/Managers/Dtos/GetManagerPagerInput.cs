// 文件名：GetManagerPagerInput.cs
// 
// 创建标识：温朋朋 2018-06-21 17:08
// 
// 修改标识：温朋朋2018-06-21 17:08
// 
// ------------------------------------------------------------------------------

using Domain.Core.Pages;

namespace April.Application.Permissions.Managers.Dtos
{
    public class GetManagerPagerInput : PageQueryEntity
    {
        /// <summary>
        /// 模糊查询参数ss
        /// </summary>
        public string FilterText { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}