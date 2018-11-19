// 文件名：MenuAppAuthorizeAppService.cs
// 
// 创建标识：温朋朋 2018-06-21 15:45
// 
// 修改标识：温朋朋2018-06-21 15:45
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using April.Application.Authorizations;
using April.Application.Authorizations.Entity;
using April.Application.Commons;
using April.Application.Permissions.Authorizes.Dtos;
using April.Common.AutoMap;
using April.Common.Exceptions;
using Domain.Core.Permissions.Authorizes;

namespace April.Application.Permissions.Authorizes
{
    [AppAuthorize(Code = "RoleAuthorize", Name = "角色授权服务")]
    public class MenuAppAuthorizeAppService : AprilAppServiceBase, IMenuAppAuthorizeAppService
    {
        /// <summary>
        ///     MenuManage
        /// </summary>
        private readonly MenuAppAuthorizeManage _menuAppAuthorizeManage;

        public MenuAppAuthorizeAppService(MenuAppAuthorizeManage menuAppAuthorizeManage)
        {
            _menuAppAuthorizeManage = menuAppAuthorizeManage;
        }
        /// <summary>
        ///     获取授权列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "查看接口授权", Action = BaseAction.View)]
        public async Task<List<AppAuthorizeDto>> GetAppAuthorizeListAsync(NullableIdDto<int> input)
        {
            if (!input.Id.HasValue)
                throw new UserFriendlyException("Id不能为空");
            var list = LoadAction();
            var data = list.MapTo<List<AppAuthorizeDto>>();
            var menuAppAuthorizeList = await _menuAppAuthorizeManage
                .GetMenuAppAuthorizeList(input.Id.Value);
            foreach (var item in menuAppAuthorizeList)
            {
                var appAuthorizeDto = data.FirstOrDefault(x => x.Code == item.AppCode);
                if (appAuthorizeDto == null) continue;
                foreach (var appMethodAuthorize in appAuthorizeDto.AppMethodAuthorizes)
                {
                    if (appMethodAuthorize.Code == item.OperationCode)
                    {
                        appMethodAuthorize.IsCheck = true;
                    }
                }
            }
            return data;
        }
        /// <summary>
        ///     接口授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission
            , Description = "接口授权", Action = BaseAction.Audit)]
        public async Task SaveMenuAppAuthorizeAsync(AuditDto input)
        {
            var list = input.EditMenuAppAuthorizes.MapTo<List<MenuAppAuthorize>>();

            await _menuAppAuthorizeManage.BatchInsertMenuAppAuthorizeAsync(list, input.MenuId);
        }
        /// <summary>
        ///     构造出程序中所有的AppAuthorize
        /// </summary>
        /// <returns></returns>
        private static List<AppAuthorize> LoadAction()
        {
            var data = new List<AppAuthorize>();
            var assembly = Assembly.Load("April.Application");//加载Application程序集
            var list = assembly.ExportedTypes
                .Where(x => x.BaseType == typeof(AprilAppServiceBase));//找到程序集中所有服务

            foreach (var item in list)
            {
                var appAuthorize = item.GetCustomAttribute(typeof(AppAuthorizeAttribute)) as AppAuthorizeAttribute;
                if (appAuthorize == null) continue;//没有标注了AppAuthorizeAttribute的服务类跳过
                var entity = new AppAuthorize
                {
                    Code = appAuthorize.Code,
                    Name = appAuthorize.Name
                };

                var methods = item.GetMethods()
                    .Where(x => x.GetCustomAttribute(
                                    typeof(AppMethodAuthorizeAttribute)) != null);
                foreach (var method in methods)
                {
                    var appMethodAuthorizeAttribute = method
                            .GetCustomAttribute(typeof(AppMethodAuthorizeAttribute))
                        as AppMethodAuthorizeAttribute;

                    if (appMethodAuthorizeAttribute == null ||
                        appMethodAuthorizeAttribute.AppAuthorize !=
                        ApplicationAuthorizeEnum.OperationPermission)
                        continue;
                    if (entity.AppMethodAuthorizes
                            .Count(x => x.Code == appMethodAuthorizeAttribute.Action) > 0)
                        continue;

                    var appMethodAuthorize =
                        new AppMethodAuthorize
                        {
                            Description = appMethodAuthorizeAttribute.Description,
                            Code = appMethodAuthorizeAttribute.Action
                        };

                    entity.AppMethodAuthorizes.Add(appMethodAuthorize);
                }

                data.Add(entity);
            }
            return data;
        }
    }
}