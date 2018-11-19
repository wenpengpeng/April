using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using April.Common.AutoMap;
using April.Common.Predicates;
using April.Models.Layouts;
using Domain.Core.Permissions.Menus;
using Domain.Core.Permissions.Roles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace April.Controllers
{
    public class PersonalCenterController : AprilWebControllerBase
    {
        /// <summary>
        ///     MenuManage
        /// </summary>
        private readonly MenuManage _menuManage;

        /// <summary>
        ///     RoleManage
        /// </summary>
        private readonly RoleManage _roleManage;

        public PersonalCenterController(MenuManage menuManage, RoleManage roleManage)
        {
            _menuManage = menuManage;
            _roleManage = roleManage;
        }

        // GET: PersonalCenter
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var userId = AprilSession.UserId;
            var strArray = AprilSession.UserRoles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var array = Array.ConvertAll(strArray, long.Parse);
            var data = await LoadData(array);
            TempData["SideBar"]= JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            return View("~/App/Main/views/layout/layout.cshtml");
        }
        /// <summary>
        ///     私有方法
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private async Task<List<TopMenusViewModel>> LoadData(long[] array)
        {
            var menuid = await _roleManage.GetRoleMenuByArrayAsync(array);
            Expression<Func<Menu, bool>> where =
                w => w.IsMenu && menuid.Contains(w.Id) && w.IsValid;

            var predicate = new PredicateGroup<Menu>();
            predicate.AddPredicate(true, where);

            var menus = await _menuManage
                .GetMenuListAsync(predicate.Predicates);

            var topMenus = menus.ToList().Where(x => x.ParentId == null);
            var topMenusViewModel = topMenus.MapTo<List<TopMenusViewModel>>();

            foreach (var item in topMenusViewModel)
            {
                item.ChildMenus.AddRange(BuildSideBar(item.Id, item.Code, menus));
            }
            return topMenusViewModel;
        }
        /// <summary>
        ///     构建菜单栏
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="topCode"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        private static List<ChildMenusViewModel> BuildSideBar(long parentId, string topCode, IEnumerable<Menu> menus)
        {
            var menuList = new List<ChildMenusViewModel>();
            var enumerable = menus as Menu[] ?? menus.ToArray();
            var childMenus = enumerable.Where(x => x.ParentId == parentId).OrderBy(x => x.Sort);
            //构架Html
            foreach (var menu in childMenus)
            {
                var childMenusViewModel = menu.MapTo<ChildMenusViewModel>();
                childMenusViewModel.TopCode = topCode;
                if (menu.HasLevel && menu.Childrens != null)
                {
                    childMenusViewModel.ChildMenus
                        .AddRange(BuildSideBar(childMenusViewModel.Id, topCode, enumerable));
                }
                menuList.Add(childMenusViewModel);
            }
            return menuList;
        }
        /// <summary>
        ///     工具栏
        /// </summary>
        /// <returns></returns>
        public ActionResult ToolBar()
        {
            ViewBag.SideBar = TempData["SideBar"];
            return View("~/App/Main/views/layout/toolbar.cshtml");
        }
    }
}