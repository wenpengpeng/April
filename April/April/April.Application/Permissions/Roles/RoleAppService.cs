// 文件名：RoleAppService.cs
// 
// 创建标识：温朋朋 2018-06-21 14:46
// 
// 修改标识：温朋朋2018-06-21 14:46
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using April.Application.Authorizations;
using April.Application.Authorizations.Entity;
using April.Application.Commons;
using April.Application.Permissions.Roles.Dtos;
using April.Common.AutoMap;
using April.Common.Exceptions;
using April.Common.Predicates;
using Domain.Core.Permissions.Roles;

namespace April.Application.Permissions.Roles
{
    /// <summary>
    ///     角色服务
    /// </summary>
    [AppAuthorize(Code = "Role", Name = "角色服务")]
    public class RoleAppService:AprilAppServiceBase,IRoleAppService
    {
        private readonly RoleManage _roleManage;

        public RoleAppService(RoleManage roleManage)
        {
            _roleManage = roleManage;
        }
        /// <summary>
        ///     查询id获取角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "查看角色", Action = BaseAction.View)]
        public async Task<RoleEditDto> GetRoleByIdAsync(NullableIdDto<long> input)
        {
            RoleEditDto edit;

            if (input.Id.HasValue)
            {
                var entity = await _roleManage.GetRoleByIdAsync(input.Id.Value);
                edit = entity.MapTo<RoleEditDto>();
            }
            else
            {
                edit = new RoleEditDto();
            }

            return edit;
        }
        /// <summary>
        ///     分页获取角色数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "角色列表", Action = BaseAction.Show)]
        public async Task<PagedResultDto<RoleListDto>> GetPagedRoleAsync(GetRolesInput input)
        {
            Expression<Func<Role, bool>> whereif = w => w.Name == input.FilterText;

            var predicate = new PredicateGroup<Role>();
            predicate.AddPredicate(!string.IsNullOrEmpty(input.FilterText), whereif);

            var tuple = await _roleManage.GetPagedRoleAsync(predicate.Predicates, input);

            var roleListDtos = tuple.Item1.MapTo<List<RoleListDto>>();

            return new PagedResultDto<RoleListDto>(
                tuple.Item2,
                roleListDtos
            );
        }
        /// <summary>
        ///     获取角色列表
        /// </summary>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "角色列表", Action = BaseAction.Show)]
        public async Task<List<RoleListDto>> GetRoleListAsync()
        {
            var roleListDto = await _roleManage.GetRoleAllAsync();

            return roleListDto.MapTo<List<RoleListDto>>();
        }
        /// <summary>
        ///     锁定角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "锁定/解锁", Action = BaseAction.Lock)]
        public async Task LockRoleAsync(NullableIdDto<long> input)
        {
            if (!input.Id.HasValue)
            {
                throw new UserFriendlyException("Key不能为空！");
            }

            await _roleManage.LockRoleAsync(input.Id.Value);
        }
        /// <summary>
        ///     删除角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "删除角色", Action = BaseAction.Delete)]
        public async Task DeleteRoleAsync(NullableIdDto<long> input)
        {
            if (!input.Id.HasValue)
            {
                throw new UserFriendlyException("Key不能为空！");
            }

            var entity = await _roleManage.GetRoleByIdAsync(input.Id.Value);
            entity.IsValid = !entity.IsValid;

            await _roleManage.UpdateRoleAsync(entity);
            await _roleManage.Delete(input.Id.Value);
        }
        /// <summary>
        ///     获取角色授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.Login)]
        public async Task<GetRoleAuthorizationDto> GetRoleAuthorizationAsync(NullableIdDto<int> input)
        {
            if (!input.Id.HasValue)
            {
                throw new UserFriendlyException("Key不能为空！");
            }

            var entity = await _roleManage.GetRoleByIdAsync(
                input.Id.Value,
                new List<string> { "Menus" });

            return new GetRoleAuthorizationDto
            {
                RoleId = entity.Id,
                Name = entity.Name,
                MenuIdList = entity.Menus?.Select(x => x.Id).ToList()
            };
        }
        /// <summary>
        ///     新增角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "新增角色", Action = BaseAction.Add)]
        public async Task CreateRoleAsync(RoleEditDto input)
        {
            var info = input.MapTo<Role>();
            await _roleManage.CreateRoleAsync(info);
        }
        /// <summary>
        ///     修改角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "修改角色", Action = BaseAction.Update)]
        public async Task UpdateRoleAsync(RoleEditDto input)
        {
            var info = await _roleManage.GetRoleByIdAsync(input.Id);
            input.MapTo(info);
            await _roleManage.UpdateRoleAsync(info);
        }
        /// <summary>
        ///   设置角色菜单  
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "角色授权", Action = BaseAction.Audit)]
        public async Task SetRoleMenusAsync(RoleSetMenuDto input)
        {
            await _roleManage.SetRoleMenusAsync(input.RoleId, input.MenuIds, input.MenuAppAuthorizeIds);
        }
    }
}