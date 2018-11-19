// 文件名：MenuAppService.cs
// 
// 创建标识：温朋朋 2018-06-21 16:23
// 
// 修改标识：温朋朋2018-06-21 16:23
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using April.Application.Authorizations;
using April.Application.Authorizations.Entity;
using April.Application.Commons;
using April.Application.Permissions.Authorizes.Dtos;
using April.Application.Permissions.Menus.Dtos;
using April.Common.AutoMap;
using April.Common.Exceptions;
using April.Common.Predicates;
using Domain.Core.Permissions.Menus;
using Domain.Core.Permissions.Roles;

namespace April.Application.Permissions.Menus
{
    [AppAuthorize(Code = "Menu", Name = "菜单服务")]
    public class MenuAppService : AprilAppServiceBase, IMenuAppService
    {
        #region IOC
        /// <summary>
        /// MenuManage
        /// </summary>
        private readonly MenuManage _menuManage;

        /// <summary>
        /// RoleManage
        /// </summary>
        private readonly RoleManage _roleManage;

        public MenuAppService(MenuManage menuManage, RoleManage roleManage)
        {
            _menuManage = menuManage;
            _roleManage = roleManage;
        }
        #endregion

        /// <summary>
        ///     获取动态路由
        /// </summary>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.Login)]
        public async Task<List<MenuListDto>> GetDynamicRouteAsync()
        {            
            var strArray = AprilSession.UserRoles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var array= Array.ConvertAll(strArray, long.Parse);

            var menuid = await _roleManage.GetRoleMenuByArrayAsync(array);

            Expression<Func<Menu, bool>> where =
                w => w.IsMenu && menuid.Contains(w.Id);
            

            var predicate = new PredicateGroup<Menu>();
            predicate.AddPredicate(true, where);

            var menus = await _menuManage
                .GetMenuListAsync(predicate.Predicates);

            var menuListDtos = menus
                .MapTo<List<MenuListDto>>();

            return menuListDtos;
        }
        /// <summary>
        ///     获取左侧导航栏
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.Login)]
        public async Task<SideBarDto> GetSideBarListAsync(long[] array)
        {
            var menuid = await _roleManage.GetRoleMenuByArrayAsync(array);

            Expression<Func<Menu, bool>> where =
                w => w.IsMenu && menuid.Contains(w.Id) && w.IsValid;

            var predicate = new PredicateGroup<Menu>();
            predicate.AddPredicate(true, where);

            var menus = await _menuManage
                .GetMenuListAsync(predicate.Predicates);

            var topMenus = menus.Where(x => x.ParentId == null);

            var enumerable = topMenus as Menu[] ?? topMenus.ToArray();

            var sideBar = new SideBarDto { TopHtml = BuilTopHtml(enumerable) };

            foreach (var menu in enumerable)
            {
                var html = new StringBuilder();

                sideBar.ChildrenHtml += BuilSideBarHtml(menu.Childrens, html);
            }

            return sideBar;
        }
        /// <summary>
        ///     获取菜单树形结构
        /// </summary>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "菜单列表", Action = BaseAction.Show)]
        public async Task<List<MenuTreeDto>> GetMenuTreeAsync()
        {
            Expression<Func<Menu, bool>> where =
                w => w.IsMenu && w.IsValid;

            var menus = await _menuManage.GetMenuListAsync(where);

            var menuTreeDto = menus
                .MapTo<List<MenuTreeDto>>();

            return menuTreeDto;
        }
        /// <summary>
        ///     获取菜单树形结构授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "菜单列表", Action = BaseAction.Show)]
        public async Task<List<MenuJsTreeAuthorizeDto>> GetMenuJsTreeAuthorizeAsync(NullableIdDto<int> input)
        {
            if (!input.Id.HasValue)
            {
                throw new UserFriendlyException("Key不能为空！");
            }

            var entity = await _roleManage.GetRoleByIdAsync(
                input.Id.Value,
                new List<string> { "Menus", "MenuAppAuthorizes" });

            Expression<Func<Menu, bool>> where =
                w => w.IsMenu && w.IsValid;

            var menuIdList = entity.Menus?
                .Select(x => x.Id).ToList();

            var menuAppAuthorizeIdList = entity.MenuAppAuthorizes?
                .Select(x => x.Id).ToList();

            var menus = await _menuManage.GetMenuChildrenListAuthorizeAsync(
                where, AprilSession.BelongUserId, menuAppAuthorizeIdList, menuIdList);

            return menus.MapTo<List<MenuJsTreeAuthorizeDto>>();
        }
        /// <summary>
        ///     获取菜单树形结构授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "菜单列表", Action = BaseAction.Show)]
        public async Task<List<MenuTreeAuthorizeDto>> GetMenuTreeAuthorizeAsync(NullableIdDto<int> input)
        {
            if (!input.Id.HasValue)
            {
                throw new UserFriendlyException("Key不能为空！");
            }

            var entity = await _roleManage.GetRoleByIdAsync(
                input.Id.Value,
                new List<string> { "Menus", "MenuAppAuthorizes" });

            Expression<Func<Menu, bool>> where =
                w => w.IsMenu && w.IsValid;

            var menuIdList = entity.Menus?
                .Select(x => x.Id).ToList();
            var menuAppAuthorizeIdList = entity.MenuAppAuthorizes?
                .Select(x => x.Id).ToList();

            var menus = await _menuManage.GetMenuListAuthorizeAsync(
                where, AprilSession.BelongUserId);

            var menuTreeDto = menus.Select(x => new MenuTreeAuthorizeDto
                {
                    DisplayName = x.DisplayName,
                    Id = x.Id,
                    ParentId = x.ParentId,
                    IsCheck = menuIdList != null && menuIdList.Contains(x.Id),
                    MenuAppAuthorizes = x.MenuAppAuthorizes == null ? new List<MenuAppAuthorizeDto>() : x.MenuAppAuthorizes?.Select(y => new MenuAppAuthorizeDto
                    {
                        Id = y.Id,
                        OperationDescription = y.OperationDescription ?? "",
                        IsCheck = menuAppAuthorizeIdList != null && menuAppAuthorizeIdList.Contains(y.Id)
                    }).ToList()
                }
            ).ToList();

            return menuTreeDto;
        }
        /// <summary>
        ///     获取select格式Menu
        /// </summary>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "菜单列表", Action = BaseAction.Show)]
        public async Task<List<MenuSelectDto>> GetMenuSelectAsync()
        {
            Expression<Func<Menu, bool>> where =
                w => w.IsMenu && w.IsValid;

            var menus = await _menuManage.GetMenuListAsync(where);

            var data = menus.Select(x =>
                    new MenuSelectDto
                    {
                        DisplayName = BusinessHelper.StringOfChar(x.Layer.Split(',').Length, "　") + "├" + x.DisplayName,
                        Id = x.Id
                    })
                .ToList();

            return data;
        }
        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "新增菜单", Action = BaseAction.Add)]
        public async Task CreateMenuAsync(MenuEditDto input)
        {
            var entity = input.MapTo<Menu>();

            entity.IsMenu = true;
            entity.HasLevel = false;
            entity.Code = Guid.NewGuid().ToString();
            var key = await _menuManage.InsertAsync(entity);

            entity.Layer = key + ",";

            if (entity.ParentId != null)
            {
                var parent = await _menuManage.GetAsync(entity.ParentId.Value);
                parent.HasLevel = true;
                await _menuManage.EditAsync(parent);

                entity.Layer = parent.Layer + key + ",";
            }
            await _menuManage.EditAsync(entity);
        }
        /// <summary>
        ///     编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "编辑菜单", Action = BaseAction.Update)]
        public async Task UpdateMenuAsync(MenuEditDto input)
        {
            if (!input.Id.HasValue)
            {
                throw new UserFriendlyException("Key不能为空！");
            }

            var entity = await _menuManage.GetAsync(input.Id.Value);
            input.MapTo(entity);

            await _menuManage.EditAsync(entity);
        }
        /// <summary>
        ///     锁定或解锁菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "锁定/解锁", Action = BaseAction.Lock)]
        public async Task LockMenuAsync(NullableIdDto<int> input)
        {
            if (!input.Id.HasValue)
            {
                throw new UserFriendlyException("Key不能为空！");
            }

            var entity = await _menuManage.GetAsync(input.Id.Value);
            entity.IsValid = !entity.IsValid;

            await _menuManage.EditAsync(entity);
        }
        /// <summary>
        ///     获取菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "查看菜单", Action = BaseAction.View)]
        public async Task<MenuEditDto> GetMenuForEditAsync(NullableIdDto<int> input)
        {
            MenuEditDto edit;

            if (input.Id.HasValue)
            {
                var entity = await _menuManage.GetAsync(input.Id.Value);
                edit = entity.MapTo<MenuEditDto>();
            }
            else
            {
                edit = new MenuEditDto();
            }

            return edit;
        }

        #region 私有

        /// <summary>
        /// 构建菜单栏
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        private static string BuilTopHtml(IEnumerable<Menu> menus)
        {
            var html = new StringBuilder();
            var enumerable = menus as Menu[] ?? menus.ToArray();
            var firstOrDefault = enumerable.FirstOrDefault();

            if (firstOrDefault == null) return html.ToString();
            html.AppendLine("<ul class=\"nav navbar-nav topSideBar\">");

            //构建Html
            foreach (var menu in enumerable)
            {
                html.AppendLine("<li>");
                html.AppendLine($"<a href=\"javascript:;\">{menu.DisplayName}</a >");
                html.AppendLine("</li>");
            }

            html.AppendLine("</ul>");

            return html.ToString();
        }

        /// <summary>
        /// 构建菜单栏
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        private static string BuilSideBarHtml(IEnumerable<Menu> menus, StringBuilder html)
        {
            if (menus == null)
            {
                html.AppendLine("<ul class=\"sidebar-menu hide\" data-widget=\"tree\"></ul>");
                return html.ToString();
            }

            var enumerable = menus as Menu[] ?? menus.ToArray();
            var firstOrDefault = enumerable.FirstOrDefault();

            if (firstOrDefault == null) return html.ToString();

            html.AppendLine(firstOrDefault.Parent.ParentId != null
                ? "<ul  class=\"treeview-menu\">"
                : "<ul class=\"sidebar-menu hide\" data-widget=\"tree\">");

            //构建Html
            foreach (var menu in enumerable)
            {
                if (!menu.HasLevel)
                {
                    html.AppendLine("<li  ui-sref-active=\"active\">");
                    html.AppendLine($"<a href=\"#\" ui-sref=\"{menu.Code}\">" +
                                    $"<i class=\"{menu.Icon}\" ></i>" +
                                    $"<span>{menu.DisplayName}</span>" +
                                    "</a >");
                }
                else
                {
                    html.AppendLine("<li class=\"treeview\">");
                    html.AppendLine("<a href=\"#\">" +
                                    $"<i class=\"{menu.Icon }\"></i>" +
                                    $"<span>{menu.DisplayName}</span>" +
                                    "<span class=\"pull-right-container\">" +
                                    "<i class=\"fa fa-angle-left pull-right\"></i>" +
                                    "</span>" +
                                    "</a >");
                    if (menu.Childrens != null)
                    {
                        var childHtml = new StringBuilder();

                        html.AppendLine(BuilSideBarHtml(menu.Childrens, childHtml));
                    }
                }
                html.AppendLine("</li>");
            }

            html.AppendLine("</ul>");

            return html.ToString();
        }

        #endregion 私有
    }
}