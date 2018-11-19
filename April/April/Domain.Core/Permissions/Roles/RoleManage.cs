// 文件名：RoleManage.cs
// 
// 创建标识：温朋朋 2018-06-19 16:15
// 
// 修改标识：温朋朋2018-06-19 16:15
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using April.Common.Exceptions;
using April.Common.Predicates;
using April.Core.Ioc;
using April.Web.Services;
using Domain.Common.Helpers;
using Domain.Core.AprilSessions;
using Domain.Core.Pages;
using Domain.Core.Permissions.Authorizes;
using Domain.Core.Permissions.Menus;
using Domain.Core.Repositories;

namespace Domain.Core.Permissions.Roles
{
    /// <summary>
    ///     角色管理
    /// </summary>
    public class RoleManage:IDomainService
    {
        private readonly IAprilRepository<Role, long> _roleRepository;
        private readonly MenuManage _menuManage;
        private readonly MenuAppAuthorizeManage _menuAppAuthorizeManage;

        /// <summary>
        ///     扩展Session
        /// </summary>
        public IAprilSessionExtensions AprilSession => IocManager.Instance.Resolve<IAprilSessionExtensions>();//不建议在领域层使用AprilSession，需要使用就要手动Resolve一下
        public RoleManage(MenuAppAuthorizeManage menuAppAuthorizeManage, MenuManage menuManage, IAprilRepository<Role, long> roleRepository)
        {
            _menuAppAuthorizeManage = menuAppAuthorizeManage;
            _menuManage = menuManage;
            _roleRepository = roleRepository;
        }

        /// <summary>
        ///     根据Id获取角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <param name="includeNames">includeNames</param>
        /// <returns></returns>
        public async Task<Role> GetRoleByIdAsync(long id, List<string> includeNames = null)
        {
            var predicate = new PredicateGroup<Role>();
            predicate.AddPredicate(true, s => s.Id == id);

            var info = await _roleRepository.QueryEntityAsync(predicate.Predicates, includeNames);
            if (info == null)
                throw new UserFriendlyException("未找到角色");

            return info;
        }
        /// <summary>
        ///     根据names获取角色集合
        /// </summary>
        /// <param name="names">角色名称集合</param>
        /// <returns></returns>
        public async Task<List<Role>> GetRoleListByNamesAsync(List<string> names)
        {
            var predicate = new PredicateGroup<Role>();
            predicate.AddPredicate(true, s => names.Contains(s.Name));

            return await _roleRepository.QueryAsync(predicate.Predicates);
        }
        /// <summary>
        ///     根据Ids获取角色集合
        /// </summary>
        /// <param name="ids">角色编号集合</param>
        /// <returns></returns>
        public async Task<List<Role>> GetRoleListByIdsAsync(List<long> ids)
        {
            var predicate = new PredicateGroup<Role>();
            predicate.AddPredicate(true, s => ids.Contains(s.Id));
            predicate.AddPredicate(true, s => s.IsValid);

            return await _roleRepository.QueryAsync(predicate.Predicates);
        }
        /// <summary>
        ///     根据DefaultRoleType获取默认角色集合
        /// </summary>
        /// <param name="defaultRoleTypes"></param>
        /// <returns></returns>
        public async Task<List<Role>> GetRoleListByDefaultRoleTypeAsync(List<int> defaultRoleTypes)
        {
            Expression<Func<Role, bool>> where =
                w => w.DefaultRoleType != null && defaultRoleTypes.Contains((int)w.DefaultRoleType);

            var predicate = new PredicateGroup<Role>();
            predicate.AddPredicate(true, where);

            return await _roleRepository.QueryAsync(predicate.Predicates);
        }
        /// <summary>
        ///     根据角色Id获取拥有菜单Id集合
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public async Task<List<long>> GetRoleMenuByArrayAsync(long[] array)
        {
            var query = _roleRepository.GetAll()
                .Where(x => array.Contains(x.Id));
            var list = await query.AsNoTracking().Include("Menus")
                .SelectMany(x => x.Menus.Select(y => y.Id)).ToListAsync();
            return list;
        }
        /// <summary>
        ///     获取所有角色集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<Role>> GetRoleAllAsync()
        {
            var predicate = new PredicateGroup<Role>();
            predicate.AddPredicate(true, w => w.IsValid);

            return await _roleRepository.QueryAsync(predicate.Predicates);
        }
        /// <summary>
        ///     分页获取角色
        /// </summary>
        /// <param name="predicates"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<Tuple<List<Role>, int>> GetPagedRoleAsync(List<AprilPredicate<Role>> predicates,
            PageQueryEntity input)
        {
            var tuple = await _roleRepository.QueryAsync(predicates, input);
            return tuple;
        }
        /// <summary>
        ///     根据code获取角色
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<Role> GetRoleByCodeAsync(string code)
        {
            var predicate = new PredicateGroup<Role>();
            predicate.AddPredicate(true, s => s.Code == code);
            var role = await _roleRepository.QueryEntityAsync(predicate.Predicates);
            return role;
        }
        /// <summary>
        ///     新增角色
        /// </summary>
        /// <param name="info">角色实体</param>
        /// <returns></returns>
        public async Task CreateRoleAsync(Role info)
        {
            //保存角色
            var key = await _roleRepository.InsertAndGetIdAsync(info);
            //设置编码
            info.Code = info.Name.ChineseConverterToSpell() + "#" + key;
            info.BelongUserId = AprilSession.BelongUserId;
            //更新角色
            await UpdateRoleAsync(info);
        }

        /// <summary>
        ///     更新角色
        /// </summary>
        /// <param name="info">角色实体</param>
        /// <returns></returns>
        public async Task UpdateRoleAsync(Role info)
        {
            if (info.BelongUserId != AprilSession.BelongUserId)
                throw new UserFriendlyException("您无权修改此角色");
            await _roleRepository.UpdateAsync(info);
        }

        /// <summary>
        ///     锁定或启用角色
        /// </summary>
        /// <param name="id">角色编号</param>
        public async Task LockRoleAsync(long id)
        {
            var info = await GetRoleByIdAsync(id);
            info.IsValid = !info.IsValid;
            await UpdateRoleAsync(info);
        }

        /// <summary>
        ///     删除角色
        /// </summary>
        /// <param name="id">角色编号</param>
        /// <returns></returns>
        public async Task Delete(long id)
        {
            var incloudNames = new List<string> { "Menus", "UserBases" };
            var info = await GetRoleByIdAsync(id, incloudNames);
            if (info.IsSystem)
                throw new UserFriendlyException("该角色为系统角色，不允许删除");
            if (info.UserBases.Any())
                throw new UserFriendlyException("该角色已关联用户，不允许删除");
            if (info.Menus.Any())
                throw new UserFriendlyException("该角色已关联菜单，不允许删除");
            await _roleRepository.DeleteAsync(id);
        }

        /// <summary>
        ///     设置角色菜单
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="menuIds">菜单编号集合</param>
        /// <param name="menuAppAuthorizeIds"></param>
        /// <returns></returns>
        public async Task SetRoleMenusAsync(long roleId, List<long> menuIds, List<long> menuAppAuthorizeIds)
        {
            var incloudNames = new List<string> { "Menus", "MenuAppAuthorizes" };
            var info = await GetRoleByIdAsync(roleId, incloudNames);

            info.Menus.Clear();
            info.MenuAppAuthorizes.Clear();//先删后增

            var menus = await _menuManage.GetMenuListByIdsAsync(menuIds);
            var menuAppAuthorizes =
                await _menuAppAuthorizeManage.GetMenuAppAuthorizeListByIdsAsync(menuAppAuthorizeIds);

            foreach (var each in menus)
                info.Menus.Add(each);

            foreach (var each in menuAppAuthorizes)
                info.MenuAppAuthorizes.Add(each);
            //更新
            await UpdateRoleAsync(info);
        }
    }
}