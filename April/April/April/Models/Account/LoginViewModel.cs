// 文件名：LoginViewModel.cs
// 
// 创建标识：温朋朋 2018-05-30 15:59
// 
// 修改标识：温朋朋2018-05-30 15:59
// 
// ------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using April.Web.Auditing;

namespace April.Models.Account
{
    public class LoginViewModel
    {
        /// <summary>
        ///   用户名或电话号码
        /// </summary>
        [Required]
        public string UsernameOrPhoneNumber { get; set; }

        /// <summary>
        ///  密码
        /// </summary>
        [Required]
        [DisableAuditing]
        public string Password { get; set; }
    }
}