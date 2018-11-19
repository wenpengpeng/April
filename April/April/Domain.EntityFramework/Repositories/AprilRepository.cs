// 文件名：AprilRepository.cs
// 
// 创建标识：温朋朋 2018-05-31 15:30
// 
// 修改标识：温朋朋2018-05-31 15:30
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using April.Common.Extensions;
using April.Common.Predicates;
using April.EntityFramework;
using April.EntityFramework.Repositories;
using April.Uow.Repositories;
using Domain.Core.Pages;
using Domain.Core.Repositories;
using Domain.EntityFramework.EntityFramework;

namespace Domain.EntityFramework.Repositories
{
    public class AprilRepository<TEntity,TPrimary>:EfRepositoryBase<AprilWebDbContext,TEntity,TPrimary>,
        IAprilRepository<TEntity,TPrimary> where TEntity:class,IEntity<TPrimary>
    {
        public AprilRepository(IDbContextProvider<AprilWebDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }
        /// <summary>
        ///     查询所有（结果集为list）
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryAsync()
        {
            var query = GetAll();
            return await query.ToListAsync();
        }
        /// <summary>
        ///     查询所有并包含子结果集
        /// </summary>
        /// <param name="includeNames"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryAsync(List<string> includeNames)
        {
            var query = GetAll();
            if (includeNames != null)
                query = includeNames.Aggregate(query, (current, item) => current.Include(item));
            return await query.ToListAsync();
        }
        /// <summary>
        ///     查询满足过滤条件的结果集
        /// </summary>
        /// <param name="predicateList"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryAsync(List<AprilPredicate<TEntity>> predicateList)
        {
            var query = GetAll();
            query= query.WhereIf(predicateList);
            return await query.ToListAsync();
        }
        /// <summary>
        ///    查询满足过滤条件的结果集并排序 
        /// </summary>
        /// <param name="predicateList"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryAsync(List<AprilPredicate<TEntity>> predicateList, string orderby)
        {
            var query = GetAll();
            query= query.WhereIf(predicateList);
            return await query.OrderBy(orderby).ToListAsync();
        }
        /// <summary>
        ///     查询所有（IQueryable）
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Query()
        {
            return GetAll();
        }
        /// <summary>
        ///     查询所有包含子结果集（IQueryable）
        /// </summary>
        /// <param name="includeNames"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(List<string> includeNames)
        {
            var query = GetAll();
            if (includeNames != null)
                query = includeNames.Aggregate(query,(current,item)=>current.Include(item));
            return query;
        }
        /// <summary>
        ///     根据条件查询
        /// </summary>
        /// <param name="predicateList"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(List<AprilPredicate<TEntity>> predicateList)
        {
            var query = GetAll();
            query = query.WhereIf(predicateList);
            return query;
        }
        /// <summary>
        ///     根据条件查询并包含子结果集
        /// </summary>
        /// <param name="predicateListm"></param>
        /// <param name="includeNames"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(List<AprilPredicate<TEntity>> predicateListm, List<string> includeNames)
        {
            var query = GetAll();
            query = query.WhereIf(predicateListm);
            includeNames?.Aggregate(query,(current,item)=>current.Include(item));
            return query;
        }
        /// <summary>
        ///     根据条件查询并排序
        /// </summary>
        /// <param name="predicateList"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(List<AprilPredicate<TEntity>> predicateList, string orderby)
        {
            var query = GetAll();
            query = query.WhereIf(predicateList);
            return query.OrderBy(orderby);
        }
        /// <summary>
        ///     获取分页数据
        /// </summary>
        /// <param name="predicateList"></param>
        /// <param name="pageQueryEntity"></param>
        /// <returns></returns>
        public async Task<Tuple<List<TEntity>, int>> QueryAsync(List<AprilPredicate<TEntity>> predicateList, PageQueryEntity pageQueryEntity)
        {
            var query = GetAll();
            query = query.WhereIf(predicateList);
            var totalCount = query.Count();
            var data = await query
                .OrderBy(pageQueryEntity.Sorting)
                .PageBy(pageQueryEntity.SkipCount, pageQueryEntity.MaxResultCount)
                .ToListAsync();
            return new Tuple<List<TEntity>, int>(data,totalCount);
        }
        /// <summary>
        ///     根据条件插叙包含子结果集
        /// </summary>
        /// <param name="predicateList"></param>
        /// <param name="includeNames"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryAsync(List<AprilPredicate<TEntity>> predicateList, List<string> includeNames)
        {
            var query = GetAll();
            query = query.WhereIf(predicateList);
            includeNames?.Aggregate(query,(current,item)=>current.Include(item));
            return await query.ToListAsync();
        }
        /// <summary>
        ///     根据条件插叙包含子结果集并排序
        /// </summary>
        /// <param name="predicateList"></param>
        /// <param name="orderby"></param>
        /// <param name="includeNames"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryAsync(List<AprilPredicate<TEntity>> predicateList, string orderby, List<string> includeNames)
        {
            var query = GetAll();
            query = query.WhereIf(predicateList);
            includeNames?.Aggregate(query,(current,item)=>current.Include(item));
            return await query.OrderBy(orderby).ToListAsync();
        }
        /// <summary>
        ///     获取分页包含子结果集
        /// </summary>
        /// <param name="predicateList"></param>
        /// <param name="pageQueryEntity"></param>
        /// <param name="includeNames"></param>
        /// <returns></returns>
        public async Task<Tuple<List<TEntity>, int>> QueryAsync(List<AprilPredicate<TEntity>> predicateList, PageQueryEntity pageQueryEntity, List<string> includeNames)
        {
            var query = GetAll();
            includeNames?.Aggregate(query,(current,item)=>current.Include(item));
            var totalCount = query.Count();
            var data= await query
                .OrderBy(pageQueryEntity.Sorting)
                .PageBy(pageQueryEntity.SkipCount, pageQueryEntity.MaxResultCount)
                .ToListAsync();
            return new Tuple<List<TEntity>, int>(data, totalCount);
        }
        /// <summary>
        ///     查询单个实体
        /// </summary>
        /// <param name="predicateList"></param>
        /// <returns></returns>
        public async Task<TEntity> QueryEntityAsync(List<AprilPredicate<TEntity>> predicateList)
        {
            var query = GetAll();
            query = query.WhereIf(predicateList);

            return await query.FirstOrDefaultAsync();
        }
        /// <summary>
        ///     查询单个实体包含子结果集
        /// </summary>
        /// <param name="predicateList"></param>
        /// <param name="includeNames"></param>
        /// <returns></returns>
        public async Task<TEntity> QueryEntityAsync(List<AprilPredicate<TEntity>> predicateList, List<string> includeNames)
        {
            var query = GetAll();
            query = query.WhereIf(predicateList);
            includeNames?.Aggregate(query,(current,item)=>current.Include(item));
            return await query.FirstOrDefaultAsync();
        }      
    }
}