using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using April.Application.Commons;
using April.Application.Permissions.Authorizes;
using April.Application.Permissions.Authorizes.Dtos;

namespace AprilWebApi.Controllers
{
    public class MenuAppAuthorizeServiceController : AprilWebApiBaseController
    {
        private readonly IMenuAppAuthorizeAppService _menuAppAuthorizeAppService;

        public MenuAppAuthorizeServiceController(IMenuAppAuthorizeAppService menuAppAuthorizeAppService)
        {
            _menuAppAuthorizeAppService = menuAppAuthorizeAppService;
        }

        /// <summary>
        ///     获取授权列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<AppAuthorizeDto>> GetAppAuthorizeListAsync(NullableIdDto<int> input)
        {
            return await _menuAppAuthorizeAppService.GetAppAuthorizeListAsync(input);
        }

        /// <summary>
        ///     接口授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SaveMenuAppAuthorizeAsync(AuditDto input)
        {
            await _menuAppAuthorizeAppService.SaveMenuAppAuthorizeAsync(input);
        }
    }
}
