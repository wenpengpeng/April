// 文件名：EditManagerUserBaseDto.cs
// 
// 创建标识：温朋朋 2018-06-21 17:09
// 
// 修改标识：温朋朋2018-06-21 17:09
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using April.Application.Permissions.Roles.Dtos;
using April.Common.AutoMap;
using Domain.Core.Permissions.Users;

namespace April.Application.Permissions.Managers.Dtos
{
    /// <summary>
    /// 用户基础信息Dto
    /// </summary>
    public class ManagerUserBaseDto 
    {
        #region 属性
        /// <summary>
        ///     Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLockoutEnaled { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [RegularExpression(@"(13\d|14[57]|15[^4,\D]|17[678]|18\d)\d{8}|170[059]\d{7}", ErrorMessage = "手机号码只能输入11位纯数字")]
        [Required(ErrorMessage = "电话号码不能为空")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string RealName { get; set; }

        #endregion 属性
    }

    /// <summary>
    ///   更新用户基础表Dto
    /// </summary>
    [AutoMap(typeof(UserBase))]
    public class EditManagerUserBaseDto : ManagerUserBaseDto
    {
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(20, MinimumLength = 8, ErrorMessage = "密码限定位数8-16位")]
        public string PasswordHash { get; set; }

        /// <summary>
        ///   确认密码
        /// </summary>
        [Compare("PasswordHash", ErrorMessage = "密码必须一致")]
        public string RePasswordHash { get; set; }

        /// <summary>
        ///   用户名
        /// </summary>
        [Required(ErrorMessage = "用户登录帐号不能为空")]
        [RegularExpression(@"^[0-9a-zA-Z]*$", ErrorMessage = "用户登录帐号只能数字和字母组合")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "用户登录帐号位数限定8-16位")]
        public string UserName { get; set; }
    }

    /// <summary>
    ///    获取用户基础表DTO
    /// </summary>

    [AutoMap(typeof(UserBase))]
    public class GetUserBaseDto : ManagerUserBaseDto
    {
        /// <summary>
        ///   用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///   所属角色Id
        /// </summary>
        public List<RoleListDto> Roles { get; set; }
    }
}