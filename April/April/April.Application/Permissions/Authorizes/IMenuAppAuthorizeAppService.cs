// 文件名：IMenuAppAuthorizeAppService.cs
// 
// 创建标识：温朋朋 2018-06-21 15:30
// 
// 修改标识：温朋朋2018-06-21 15:30
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using April.Application.Commons;
using April.Application.Permissions.Authorizes.Dtos;
using April.Web.Services;

namespace April.Application.Permissions.Authorizes
{
    public interface IMenuAppAuthorizeAppService: IApplicationService
    {
        /// <summary>
        /// GetAppAuthorizeListAsync
        /// </summary>
        /// <returns></returns>
        Task<List<AppAuthorizeDto>> GetAppAuthorizeListAsync(NullableIdDto<int> input);

        /// <summary>
        /// SaveMenuAppAuthorize
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SaveMenuAppAuthorizeAsync(AuditDto input);
    }
}