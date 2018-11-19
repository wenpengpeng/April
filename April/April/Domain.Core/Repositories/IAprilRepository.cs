// 文件名：IAprilRepository.cs
// 
// 创建标识：温朋朋 2018-05-31 15:11
// 
// 修改标识：温朋朋2018-05-31 15:11
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using April.Common.Predicates;
using April.Uow.Repositories;
using Domain.Core.Pages;

namespace Domain.Core.Repositories
{
    public interface IAprilRepository<TEntity,TPrimary>: IBaseRepository<TEntity,TPrimary>
        where TEntity:class,IEntity<TPrimary>

    {
        Task<List<TEntity>> QueryAsync();

        Task<List<TEntity>> QueryAsync(List<string> includeNames);

        Task<List<TEntity>> QueryAsync(List<AprilPredicate<TEntity>> predicateList);

        Task<List<TEntity>> QueryAsync(List<AprilPredicate<TEntity>> predicateList, string orderby);

        IQueryable<TEntity> Query();

        IQueryable<TEntity> Query(List<string> includeNames);

        IQueryable<TEntity> Query(List<AprilPredicate<TEntity>> predicateList);

        IQueryable<TEntity> Query(List<AprilPredicate<TEntity>> predicateListm, List<string> includeNames);

        IQueryable<TEntity> Query(List<AprilPredicate<TEntity>> predicateList, string orderby);

        Task<Tuple<List<TEntity>, int>> QueryAsync(List<AprilPredicate<TEntity>> predicateList,
            PageQueryEntity pageQueryEntity);

        Task<List<TEntity>> QueryAsync(List<AprilPredicate<TEntity>> predicateList,
            List<string> includeNames);

        Task<List<TEntity>> QueryAsync(List<AprilPredicate<TEntity>> predicateList, string orderby,
            List<string> includeNames);

        Task<Tuple<List<TEntity>, int>> QueryAsync(List<AprilPredicate<TEntity>> predicateList,
            PageQueryEntity pageQueryEntity, List<string> includeNames);

        Task<TEntity> QueryEntityAsync(List<AprilPredicate<TEntity>> predicateList);

        Task<TEntity> QueryEntityAsync(List<AprilPredicate<TEntity>> predicateList,
            List<string> includeNames);       
    }
}