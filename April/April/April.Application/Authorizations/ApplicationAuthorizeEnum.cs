// 文件名：ApplicationAuthorizeEnum.cs
// 
// 创建标识：温朋朋 2018-06-21 15:58
// 
// 修改标识：温朋朋2018-06-21 15:58
// 
// ------------------------------------------------------------------------------
namespace April.Application.Authorizations
{
    /// <summary>
    ///     授权级别
    /// </summary>
    public enum ApplicationAuthorizeEnum
    {
        /// <summary>
        /// 登录授权
        /// </summary>
        Login = 1,

        /// <summary>
        /// 操作授权(登录)
        /// </summary>
        OperationPermission = 2,

        /// <summary>
        /// 操作授权(非登录操作授权)
        /// </summary>
        NoLoginOperationPermission = 3
    }
}