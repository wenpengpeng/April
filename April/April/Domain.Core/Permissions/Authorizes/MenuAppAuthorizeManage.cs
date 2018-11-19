// 文件名：MenuAppAuthorizeManage.cs
// 
// 创建标识：温朋朋 2018-06-20 9:32
// 
// 修改标识：温朋朋2018-06-20 9:32
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using April.Common.FluentData;
using April.Common.Predicates;
using April.Uow.UnitOfWorks;
using April.Web.Services;
using Domain.Core.Permissions.Authorizes.Dtos;
using Domain.Core.Repositories;

namespace Domain.Core.Permissions.Authorizes
{
    /// <summary>
    ///     菜单授权管理
    /// </summary>
    public class MenuAppAuthorizeManage:IDomainService
    {
        private readonly IAprilRepository<MenuAppAuthorize, long> _repository;
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="unitOfWorkManager"></param>
        public MenuAppAuthorizeManage(
            IAprilRepository<MenuAppAuthorize, long> repository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        /// <summary>
        /// 查询判断菜单授权
        /// </summary>
        /// <param name="menuCode"></param>
        /// <param name="appCode"></param>
        /// <param name="operationCode"></param>
        /// <returns></returns>
        public async Task<MenuAppAuthorize> JudgeMenuAppAuthorizeAsync(string menuCode, string appCode, string operationCode)
        {
            var predicate = new PredicateGroup<MenuAppAuthorize>();
            predicate.AddPredicate(true, s => s.AppCode == appCode
                                              && s.MenuCode == menuCode
                                              && s.OperationCode == operationCode);

            return await _repository.QueryEntityAsync(predicate.Predicates);
        }
        /// <summary>
        /// 根据Ids获取菜单授权集合
        /// </summary>
        /// <param name="ids">菜单编号集合</param>
        /// <returns></returns>
        public async Task<List<MenuAppAuthorize>> GetMenuAppAuthorizeListByIdsAsync(List<long> ids)
        {
            var predicate = new PredicateGroup<MenuAppAuthorize>();
            predicate.AddPredicate(true, s => ids.Contains(s.Id));
            return await _repository.QueryAsync(predicate.Predicates);
        }
        /// <summary>
        ///     根据用户Id构建Script
        /// </summary>
        /// <returns></returns>
        public string GetScript(long userId)
        {            
            var sql = $@"SELECT MenuCode as MenuCode,AppCode as AppCode,OperationCode as OperationCode
                        FROM MenuAppAuthorize A
                    INNER JOIN RoleMenuAppAuthorize B
                        ON A.Id = B.MenuAppAuthorizeId
                    INNER JOIN UserRole C ON C.RoleId = B.RoleId
                        WHERE C.UserId = {userId}";

            using (var db = FluentDataHelper.CreateInstance())
            {
                var list = db.Sql(sql)
                    .QueryMany<AppMenuAuth>();

                var sb = new StringBuilder();
                sb.AppendLine("(function() {");

                sb.AppendLine("    abp.appMenuAuth = {};");
                sb.AppendLine("    abp.appMenuAuth.allAppMenuAuths = [");

                foreach (var item in list)
                {
                    sb.AppendLine("           {");
                    sb.AppendLine("            menuCode: '" + item.MenuCode + "',");
                    sb.AppendLine("            appCode: '" + item.AppCode + "',");
                    sb.AppendLine("            operationCode: '" + item.OperationCode + "',");
                    sb.AppendLine("},");
                }

                sb.AppendLine("    ];");

                sb.AppendLine("})();");

                return sb.ToString();
            }
        }
        /// <summary>
        ///     根据菜单Id获取菜单授权列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public async Task<List<MenuAppAuthorize>> GetMenuAppAuthorizeList(long menuId)
        {
            Expression<Func<MenuAppAuthorize, bool>> where =
                w => w.MenuId == menuId;
            var predicate = new PredicateGroup<MenuAppAuthorize>();
            predicate.AddPredicate(true, where);

            return await _repository.QueryAsync(predicate.Predicates);
        }
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="list"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>   
        public async Task BatchInsertMenuAppAuthorizeAsync(List<MenuAppAuthorize> list, long menuId)
        {
            var keyArray = new List<long>();
            using (var unitOfWork = _unitOfWorkManager.Begin()) //启用工作单元
            {
                var first = list.FirstOrDefault();
                foreach (var item in list)
                {
                    Expression<Func<MenuAppAuthorize, bool>> where =
                        w => w.MenuId == item.MenuId
                             && w.MenuCode == item.MenuCode
                             && w.AppCode == item.AppCode
                             && w.OperationCode == item.OperationCode;

                    var predicate = new PredicateGroup<MenuAppAuthorize>();
                    predicate.AddPredicate(true, where);

                    var entity = await _repository.QueryEntityAsync(predicate.Predicates);
                    if (entity != null)
                    {
                        keyArray.Add(entity.Id);
                    }
                    else
                    {
                        var key = await _repository.InsertAndGetIdAsync(item);
                        keyArray.Add(key);
                    }
                }
                if (first != null)
                {
                    await _repository.DeleteAsync(x => !(keyArray.Contains(x.Id))
                                                       && x.MenuId == first.MenuId);
                }
                else
                {
                    await _repository.DeleteAsync(x => x.MenuId == menuId);
                }

                await unitOfWork.CompleteAsync();
            }
        }
    }
}