// 文件名：MenuManage.cs
// 
// 创建标识：温朋朋 2018-06-19 16:18
// 
// 修改标识：温朋朋2018-06-19 16:19
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using April.Common.Predicates;
using April.Web.Services;
using Domain.Core.AprilSessions;
using Domain.Core.Enums.Users;
using Domain.Core.Permissions.Menus.Dtos;
using Domain.Core.Permissions.Roles;
using Domain.Core.Permissions.Users;
using Domain.Core.Repositories;

namespace Domain.Core.Permissions.Menus
{
    public class MenuManage:IDomainService
    {
        private readonly IAprilRepository<Menu, long> _menuRepository;
        private readonly IAprilRepository<Role, long> _roleRepository;
        private readonly UserBaseManage _userManage;
        
        public MenuManage(UserBaseManage userManage, IAprilRepository<Role, long> roleRepository, IAprilRepository<Menu, long> menuRepository)
        {
            _userManage = userManage;
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;
        }
        /// <summary>
        ///     获取菜单列表
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<List<Menu>> GetMenuListAsync(Expression<Func<Menu,bool>> where)
        {
            var predicate = new PredicateGroup<Menu>();
            predicate.AddPredicate(where!=null,where);

            var list = await _menuRepository.QueryAsync(predicate.Predicates," Sort Asc");

            var menus = list.Where(m => m.ParentId == null).ToList();

            return GetTreeMenuList(menus);
        }
        /// <summary>
        ///     获取菜单列表
        /// </summary>
        /// <param name="predicates"></param>
        /// <returns></returns>
        public async Task<List<Menu>> GetMenuListAsync(List<AprilPredicate<Menu>> predicates)
        {
            return await _menuRepository.QueryAsync(predicates," Sort Asc");
        }

        public async Task<List<Menu>> GetMenuListAuthorizeAsync(Expression<Func<Menu, bool>> where, long belongUserId)
        {
            var accountType =
                (AccountTypeEnum) Convert.ToInt32(ClaimTypeExtensions.GetClaimValue(ClaimTypeExtensions.AccountType));

            var predicate = new PredicateGroup<Menu>();
            predicate.AddPredicate(where != null, where);

            if (accountType != AccountTypeEnum.超级管理员)
            {
                var roles = await _userManage.GetUserRoleListAsync(belongUserId);

                var menus = await _roleRepository.GetAll()
                    .Where(x => roles.Contains(x.Id))
                    .AsNoTracking().Include("Menus")
                    .SelectMany(x => x.Menus.Where(m => m.IsPublic).Select(y => y.Id)).ToListAsync();

                Expression<Func<Menu,bool>> arrayWhere= w => menus.Contains(w.Id);
                predicate.AddPredicate(true,arrayWhere);
            }

            var list = await _menuRepository.QueryAsync(predicate.Predicates,new List<string>{"MenuAppAuthorizes"});
            var menuList = list.Where(x => x.ParentId == null).ToList();

            var isAll = accountType == AccountTypeEnum.超级管理员;

            return GetTreeMenuList(menuList,isAll);

        }
        /// <summary>
        ///     获取授权菜单带子集的列表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="belongUserId"></param>
        /// <param name="menuAppAuthorizeIds"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public async Task<List<MenuJsTreeAuthorizeEntityDto>> GetMenuChildrenListAuthorizeAsync(
            Expression<Func<Menu, bool>> where, long belongUserId, List<long> menuAppAuthorizeIds, List<long>menuIds)
        {
            var accountType = (AccountTypeEnum)Convert.ToInt32(
                ClaimTypeExtensions.GetClaimValue(
                    ClaimTypeExtensions.AccountType));

            var predicate = new PredicateGroup<Menu>();
            predicate.AddPredicate(where != null, where);

            if (accountType != AccountTypeEnum.超级管理员)
            {
                var array = await _userManage
                    .GetUserRoleListAsync(belongUserId);
                var menuArray = await _roleRepository.GetAll()
                    .Where(x => array.Contains(x.Id))
                    .AsNoTracking().Include("Menus")
                    .SelectMany(x => x.Menus.Where(w => w.IsPublic).Select(y => y.Id)).ToListAsync();

                Expression<Func<Menu, bool>> arrayWhere =
                    w => menuArray.Contains(w.Id);

                predicate.AddPredicate(true, arrayWhere);
            }

            var infoList = await _menuRepository.QueryAsync(
                predicate.Predicates
                , new List<string> { "MenuAppAuthorizes" });

            var menus = infoList.Where(x => x.ParentId == null).ToList();

            var isAll = accountType == AccountTypeEnum.超级管理员;

            var menuTreeListDto = new List<MenuJsTreeAuthorizeEntityDto>();

            //递归返回菜单树
            GetTreeChildrenMenuList(menuTreeListDto, menus, menuAppAuthorizeIds, menuIds, isAll);

            return menuTreeListDto;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">菜单编号</param>
        /// <returns></returns>
        public async Task<Menu> GetAsync(long id)
        {
            var entity = await _menuRepository.GetAsync(id);

            return entity;
        }

        /// <summary>
        /// 根据Ids获取菜单集合
        /// </summary>
        /// <param name="ids">菜单编号集合</param>
        /// <returns></returns>
        public async Task<List<Menu>> GetMenuListByIdsAsync(List<long> ids)
        {
            var predicate = new PredicateGroup<Menu>();
            predicate.AddPredicate(true, s => ids.Contains(s.Id));

            return await _menuRepository.QueryAsync(predicate.Predicates);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">菜单实体</param>
        /// <returns></returns>
        public async Task<long> InsertAsync(Menu entity)
        {
            var entityId = await _menuRepository.InsertAndGetIdAsync(entity);
            return entityId;
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="id">菜单编号</param>
        /// <returns></returns>
        public async Task RemoveAsync(long id)
        {
            var entity = await _menuRepository.GetAsync(id);
            entity.IsValid = false;

            await _menuRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity">菜单实体</param>
        /// <returns></returns>
        public async Task EditAsync(Menu entity)
        {
            await _menuRepository.UpdateAsync(entity);
        }

        #region Private
        /// <summary>
        ///     获取所有公开的菜单集合
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        private static List<Menu> GetTreeMenuList(IEnumerable<Menu> menus, bool isAll = true)
        {
            var menuList = new List<Menu>();

            foreach (var item in menus)
            {
                if (!isAll && !item.IsPublic)
                    continue;
                menuList.Add(item);
                if (item.Childrens != null && item.Childrens.Count > 0)
                    menuList.AddRange(GetTreeMenuList(item.Childrens, isAll));
            }
            return menuList;
        }

        /// <summary>
        ///     递归返回菜单树
        /// </summary>
        /// <param name="menuTrees"></param>
        /// <param name="menus"></param>
        /// <param name="menuAppAuthorizeIds"></param>
        /// <param name="menuIds"></param>
        /// <param name="isAll"></param>
        private static void GetTreeChildrenMenuList(List<MenuJsTreeAuthorizeEntityDto> menuTrees,
            IEnumerable<Menu> menus, List<long> menuAppAuthorizeIds, List<long> menuIds, bool isAll = true)
        {
            foreach (var info in menus)
            {
                if (!isAll && !info.IsPublic)
                {
                    continue;
                }

                //处理菜单是否被选中的情况
                bool menuIsSelected;
                if (info.MenuAppAuthorizes != null && info.MenuAppAuthorizes.Any() && info.Childrens != null && info.Childrens.Any())
                {
                    menuIsSelected = info.Childrens.All(x => menuIds != null && menuIds.Contains(x.Id))
                                     && info.MenuAppAuthorizes.All(x => menuAppAuthorizeIds != null && menuAppAuthorizeIds.Contains(x.Id));
                }
                else if (info.Childrens != null && info.Childrens.Any())
                {
                    menuIsSelected = info.Childrens.All(x => menuIds != null && menuIds.Contains(x.Id));

                }
                else if (info.MenuAppAuthorizes != null && info.MenuAppAuthorizes.Any())
                {
                    menuIsSelected = info.MenuAppAuthorizes.All(x => menuAppAuthorizeIds != null && menuAppAuthorizeIds.Contains(x.Id));
                }
                else
                {
                    menuIsSelected = menuIds != null && menuIds.Contains(info.Id);
                }
                //添加菜单
                var menuTree = new MenuJsTreeAuthorizeEntityDto
                {
                    Code = info.Code,
                    Id = "menu_" + info.Id,
                    Icon = string.IsNullOrWhiteSpace(info.Icon) ? "fa fa-tags" : info.Icon,
                    Text = info.DisplayName,
                    Type = "menu",
                    State = new State
                    {
                        Selected = menuIsSelected
                    }
                };
                //菜单对应的授权接口列表
                if (info.MenuAppAuthorizes != null)
                {
                    menuTree.Children.AddRange(info.MenuAppAuthorizes.Select(menuAppAuthorize =>
                    {
                        #region 图标处理

                        var icon = "fa fa-cogs";
                        if (menuAppAuthorize.OperationDescription.IndexOf("新增", StringComparison.OrdinalIgnoreCase) >= 0
                            && menuAppAuthorize.OperationDescription.IndexOf("修改", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            icon = "fa fa-pencil-square";
                        }
                        else if (menuAppAuthorize.OperationDescription.IndexOf("新增", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            icon = "fa fa-plus";
                        }
                        else if (menuAppAuthorize.OperationDescription.IndexOf("修改", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            icon = "fa fa-edit";
                        }
                        else if (menuAppAuthorize.OperationDescription.IndexOf("查看", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            icon = "fa fa-eye";
                        }
                        else if (menuAppAuthorize.OperationDescription.IndexOf("删除", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            icon = "fa fa-trash-o";
                        }
                        else if (menuAppAuthorize.OperationDescription.IndexOf("审核", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            icon = "fa fa-user-secret";
                        }
                        else if (menuAppAuthorize.OperationDescription.IndexOf("统计", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            icon = "fa fa-line-chart";
                        }
                        else if (menuAppAuthorize.OperationDescription.IndexOf("获取", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            icon = "fa fa-retweet";
                        }
                        else if (menuAppAuthorize.OperationDescription.IndexOf("查询", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            icon = "fa fa-search";
                        }
                        else if (menuAppAuthorize.OperationDescription.IndexOf("锁定", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            icon = "fa fa-lock";
                        }

                        #endregion

                        return new MenuJsTreeAuthorizeEntityDto
                        {
                            Code = menuAppAuthorize.Id + menuAppAuthorize.AppCode,
                            Id = "menuAppAuthorize_" + menuAppAuthorize.Id,
                            Icon = icon,
                            Text = menuAppAuthorize.OperationDescription,
                            Type = "opration",
                            State = new State
                            {
                                Selected =
                                    menuAppAuthorizeIds != null && menuAppAuthorizeIds.Contains(menuAppAuthorize.Id)
                            }
                        };
                    }).ToList());
                }
                menuTrees.Add(menuTree);
                if (info.Childrens != null && info.Childrens.Count > 0)
                {
                    GetTreeChildrenMenuList(menuTree.Children, info.Childrens, menuAppAuthorizeIds, menuIds, isAll);
                }
            }
        }
        #endregion
    }
}