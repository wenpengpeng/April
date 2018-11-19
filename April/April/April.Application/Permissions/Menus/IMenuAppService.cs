// 文件名：IMenuAppService.cs
// 
// 创建标识：温朋朋 2018-06-21 15:19
// 
// 修改标识：温朋朋2018-06-21 15:19
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using April.Application.Commons;
using April.Application.Permissions.Menus.Dtos;
using April.Web.Services;

namespace April.Application.Permissions.Menus
{
    public interface IMenuAppService:IApplicationService
    {
        /// <summary>
        /// 获取动态路由
        /// </summary>
        /// <returns></returns>
        Task<List<MenuListDto>> GetDynamicRouteAsync();

        /// <summary>
        ///  获取左侧导航栏
        /// </summary>
        Task<SideBarDto> GetSideBarListAsync(long[] array);

        /// <summary>
        /// 获取菜单树形导航栏
        /// </summary>
        /// <returns></returns>
        Task<List<MenuTreeDto>> GetMenuTreeAsync();

        /// <summary>
        /// 获取菜单树形导航栏
        /// </summary>
        /// <returns></returns>
        Task<List<MenuJsTreeAuthorizeDto>> GetMenuJsTreeAuthorizeAsync(NullableIdDto<int> input);

        /// <summary>
        /// 获取菜单树形导航栏
        /// </summary>
        /// <returns></returns>
        Task<List<MenuTreeAuthorizeDto>> GetMenuTreeAuthorizeAsync(NullableIdDto<int> input);

        /// <summary>
        /// 获取菜单树形导航栏
        /// </summary>
        /// <returns></returns>
        Task<List<MenuSelectDto>> GetMenuSelectAsync();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateMenuAsync(MenuEditDto input);

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateMenuAsync(MenuEditDto input);

        /// <summary>
        /// 锁定或解锁菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task LockMenuAsync(NullableIdDto<int> input);

        /// <summary>
        /// 获取菜单(编辑)
        /// </summary>
        /// <returns></returns>
        Task<MenuEditDto> GetMenuForEditAsync(NullableIdDto<int> input);
    }
}