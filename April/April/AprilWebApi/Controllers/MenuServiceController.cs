using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using April.Application.Commons;
using April.Application.Permissions.Menus;
using April.Application.Permissions.Menus.Dtos;
using April.Common.Exceptions;

namespace AprilWebApi.Controllers
{
    public class MenuServiceController : AprilWebApiBaseController
    {
        private readonly IMenuAppService _menuAppService;

        public MenuServiceController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        /// <summary>
        ///     获取动态路由
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<MenuListDto>> GetDynamicRouteAsync()
        {            
            return await _menuAppService.GetDynamicRouteAsync();
        }

        /// <summary>
        ///     获取左侧导航栏
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<SideBarDto> GetSideBarListAsync(long[] array)
        {
            return await _menuAppService.GetSideBarListAsync(array);
        }

        /// <summary>
        ///     获取菜单树形结构
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<MenuTreeDto>> GetMenuTreeAsync()
        {            
            return await _menuAppService.GetMenuTreeAsync();
        }

        /// <summary>
        ///     获取菜单树形结构授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<MenuJsTreeAuthorizeDto>> GetMenuJsTreeAuthorizeAsync(NullableIdDto<int> input)
        {
            return await _menuAppService.GetMenuJsTreeAuthorizeAsync(input);
        }

        /// <summary>
        ///     获取菜单树形结构授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<MenuTreeAuthorizeDto>> GetMenuTreeAuthorizeAsync(NullableIdDto<int> input)
        {
            return await _menuAppService.GetMenuTreeAuthorizeAsync(input);
        }

        /// <summary>
        ///     获取select格式Menu
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<MenuSelectDto>> GetMenuSelectAsync()
        {
            return await _menuAppService.GetMenuSelectAsync();
        }

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateMenuAsync(MenuEditDto input)
        {
            await _menuAppService.CreateMenuAsync(input);
        }

        /// <summary>
        ///     编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateMenuAsync(MenuEditDto input)
        {
            await _menuAppService.UpdateMenuAsync(input);
        }

        /// <summary>
        ///     锁定或解锁菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task LockMenuAsync(NullableIdDto<int> input)
        {
            await _menuAppService.LockMenuAsync(input);
        }

        /// <summary>
        ///     获取菜单(编辑)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MenuEditDto> GetMenuForEditAsync(NullableIdDto<int> input)
        {
            return await _menuAppService.GetMenuForEditAsync(input);
        }
    }
}
