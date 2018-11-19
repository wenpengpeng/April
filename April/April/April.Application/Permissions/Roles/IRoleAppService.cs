// 文件名：IRoleAppService.cs
// 
// 创建标识：温朋朋 2018-06-21 14:20
// 
// 修改标识：温朋朋2018-06-21 14:20
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using April.Application.Commons;
using April.Application.Permissions.Roles.Dtos;
using April.Web.Services;

namespace April.Application.Permissions.Roles
{
    public interface IRoleAppService:IApplicationService
    {
        /// <summary>
        ///     查询id获取角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RoleEditDto> GetRoleByIdAsync(NullableIdDto<long> input);

        /// <summary>
        ///     分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<RoleListDto>> GetPagedRoleAsync(GetRolesInput input);

        /// <summary>
        ///   获取角色列表
        /// </summary>
        /// <returns></returns>
        Task<List<RoleListDto>> GetRoleListAsync();

        /// <summary>
        ///     锁定角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task LockRoleAsync(NullableIdDto<long> input);

        /// <summary>
        ///     删除角色
        /// </summary>
        /// <returns></returns>
        Task DeleteRoleAsync(NullableIdDto<long> input);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetRoleAuthorizationDto> GetRoleAuthorizationAsync(NullableIdDto<int> input);

        /// <summary>
        ///     新增角色
        /// </summary>
        /// <returns></returns>
        Task CreateRoleAsync(RoleEditDto input);

        /// <summary>
        ///     修改角色
        /// </summary>
        /// <returns></returns>
        Task UpdateRoleAsync(RoleEditDto input);
        /// <summary>
        ///     设置角色菜单
        /// </summary>
        /// <returns></returns>
        Task SetRoleMenusAsync(RoleSetMenuDto input);
    }
}