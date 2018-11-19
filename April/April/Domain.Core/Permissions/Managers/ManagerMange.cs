// 文件名：ManagerMange.cs
// 
// 创建标识：温朋朋 2018-06-20 13:59
// 
// 修改标识：温朋朋2018-06-20 13:59
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using April.Common.Predicates;
using April.Web.Services;
using Domain.Core.Pages;
using Domain.Core.Permissions.Users;
using Domain.Core.Repositories;

namespace Domain.Core.Permissions.Managers
{
    /// <summary>
    ///     后台用户管理
    /// </summary>
    public class ManagerMange:IDomainService
    {
        private readonly IAprilRepository<Manager, long> _managerRepository;
        private readonly IAprilRepository<UserBase, long> _userBaseRepository;
        private readonly IAprilRepository<UserClaim, long> _userClaimRepository;

        /// <summary>
        ///     构造方法
        /// </summary>
        public ManagerMange(
            IAprilRepository<Manager, long> managerRepository, IAprilRepository<UserBase, long> userBaseRepository, IAprilRepository<UserClaim, long> userClaimRepository)
        {
            _managerRepository = managerRepository;
            _userBaseRepository = userBaseRepository;
            _userClaimRepository = userClaimRepository;
        }
        /// <summary>
        ///     新增后台用户返回Id
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public async Task<long> InsertManagerAndGetIdAsync(Manager manager)
        {
            return await _managerRepository.InsertAndGetIdAsync(manager);
        }
        /// <summary>
        ///   锁定，解锁操作
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public async Task LockManagerAsync(long managerId)
        {
            //获取管理员信息
            var manager = await GetManagerByIdAsync(managerId, new List<string> { "UserBase" });

            manager.UserBase.IsLockoutEnaled = !manager.UserBase.IsLockoutEnaled;

            await _managerRepository.UpdateAsync(manager);
        }

        /// <summary>
        ///     修改后台用户
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public async Task UpdateManagerAsync(Manager manager)
        {
            await _managerRepository.UpdateAsync(manager);
        }

        /// <summary>
        ///   删除
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>       
        public async Task DeleteManagerAsync(long managerId)
        {
            //获取管理员表数据删除
            var entity = await GetManagerByIdAsync(managerId, new List<string>());

            await _managerRepository.DeleteAsync(entity);

            //获取用户基础表数据删除
            var user = await _userBaseRepository.GetAll()
                .Where(x => x.Id == entity.UserId)
                .Include("Roles")
                .Include("UserClaims")
                .FirstOrDefaultAsync();

            //删除用户的申明表集合
            if (user.UserClaims != null && user.UserClaims.Any())
            {
                var deleteIds = user.UserClaims.Select(x => x.Id);
                await _userClaimRepository.DeleteAsync(x => deleteIds.Contains(x.Id));
            }

            await _userBaseRepository.DeleteAsync(user);
        }

        /// <summary>
        ///     根据后台用户Id获取用户实体
        /// </summary>
        /// <returns></returns>
        public async Task<Manager> GetManagerByIdAsync(long managerId, List<string> includeNames)
        {
            var predicate = new PredicateGroup<Manager>();
            predicate.AddPredicate(true, s => s.Id == managerId);

            var managerList = await _managerRepository.QueryAsync(
                predicate.Predicates, includeNames);

            return managerList.FirstOrDefault();
        }

        /// <summary>
        ///     分页获取管理员
        /// </summary>
        /// <param name="predicates"></param>
        /// <param name="input"></param>
        /// <param name="includeNames"></param>
        /// <returns></returns>
        public async Task<Tuple<List<Manager>, int>> GetPagedMemberAsync(List<AprilPredicate<Manager>> predicates,
            PageQueryEntity input, List<string> includeNames)
        {
            var tuple = await _managerRepository.QueryAsync(predicates, input, includeNames);
            return tuple;
        }

        /// <summary>
        /// 根据UserBaseId获取用户数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserBase> GetUserBaseInfo(long userId)
        {
            //获取用户基础表数据
            var user = await _userBaseRepository.GetAll()
                .Where(x => x.Id == userId).FirstOrDefaultAsync();
            return user;
        }

        /// <summary>
        /// 修改UserBase信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdataUserBase(UserBase input)
        {
            await _userBaseRepository.UpdateAsync(input);
        }
    }
}