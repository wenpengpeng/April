using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using April.Application.Commons;
using April.Application.Permissions.Roles;
using April.Application.Permissions.Roles.Dtos;

namespace AprilWebApi.Controllers
{
    public class RoleServiceController : AprilWebApiBaseController
    {
        private readonly IRoleAppService _roleAppService;

        public RoleServiceController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
        }
        /// <summary>
        ///     通过id获取角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RoleEditDto> GetRoleByIdAsync(NullableIdDto<long> input)
        {
            return await _roleAppService.GetRoleByIdAsync(input);
        }
        /// <summary>
        ///     分页获取角色数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PagedResultDto<RoleListDto>> GetPagedRoleAsync(GetRolesInput input)
        {
            return await _roleAppService.GetPagedRoleAsync(input);
        }

        /// <summary>
        ///     获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<RoleListDto>> GetRoleListAsync()
        {
            return await _roleAppService.GetRoleListAsync();
        }

        /// <summary>
        ///     锁定角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task LockRoleAsync(NullableIdDto<long> input)
        {
             await _roleAppService.LockRoleAsync(input);
        }

        /// <summary>
        ///     删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task DeleteRoleAsync(NullableIdDto<long> input)
        {
            await _roleAppService.DeleteRoleAsync(input);
        }

        /// <summary>
        ///     获取角色授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GetRoleAuthorizationDto> GetRoleAuthorizationAsync(NullableIdDto<int> input)
        {
            return await _roleAppService.GetRoleAuthorizationAsync(input);
        }

        /// <summary>
        ///     新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateRoleAsync(RoleEditDto input)
        {
            await _roleAppService.CreateRoleAsync(input);
        }

        /// <summary>
        ///     修改角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateRoleAsync(RoleEditDto input)
        {
            await _roleAppService.UpdateRoleAsync(input);
        }

        /// <summary>
        ///   设置角色菜单  
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SetRoleMenusAsync(RoleSetMenuDto input)
        {
            await _roleAppService.SetRoleMenusAsync(input);
        }
    }
}
