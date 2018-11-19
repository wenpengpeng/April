// 文件名：UserBaseManage.cs
// 
// 创建标识：温朋朋 2018-05-31 10:54
// 
// 修改标识：温朋朋2018-05-31 10:54
// 
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using April.Common.Predicates;
using April.Uow.Repositories;
using April.Web.Services;
using Domain.Core.Permissions.Members;
using Domain.Core.Repositories;

namespace Domain.Core.Permissions.Users
{
    public class UserBaseManage:IDomainService
    {
        #region Ioc
        private readonly IAprilRepository<UserBase, long> _userBaseRepository;
        private readonly IAprilRepository<UserClaim, long> _userClaimRepository;
        private readonly IAprilRepository<Member, long> _membeRepository;

        public UserBaseManage(IAprilRepository<UserBase, long> userBaseRepository,
            IAprilRepository<UserClaim, long> userClaimRepository, IAprilRepository<Member, long> membeRepository)
        {
            _userBaseRepository = userBaseRepository;
            _userClaimRepository = userClaimRepository;
            _membeRepository = membeRepository;
        } 
        #endregion

        /// <summary>
        ///     根据电话号码查询User
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public UserBase GetUserByPhone(string phone)
        {
            return _userBaseRepository.FirstOrDefault(s=>s.PhoneNumber==phone);
        }
        /// <summary>
        ///     新增用户返回Id
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<long> CreateUserAndGetIdAsync(UserBase user)
        {
            return await _userBaseRepository.InsertAndGetIdAsync(user);
        }
        /// <summary>
        ///     新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task CreateUserAsync(UserBase user)
        {
            await _userBaseRepository.InsertAsync(user);
        }
        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateUserBaseAsync(UserBase input)
        {
            await _userBaseRepository.UpdateAsync(input);
        }
        /// <summary>
        ///     获取用户
        ///     <remarks>如果没有查询到用户信息返回null</remarks>
        /// </summary>
        /// <returns></returns>
        public async Task<UserBase> GetUserBaseByIdAsync(long userId, List<string> includeNames = null)
        {
            var predicate = new PredicateGroup<UserBase>();
            predicate.AddPredicate(true, x => x.Id == userId && !x.IsLockoutEnaled);

            var userBases = await _userBaseRepository.QueryAsync(
                predicate.Predicates,
                includeNames);

            return userBases.FirstOrDefault();
        }
        /// <summary>
        ///     获取用户
        ///     <remarks>如果没有查询到用户信息返回null</remarks>
        /// </summary>
        /// <returns></returns>
        public async Task<UserBase> GetUserBaseByUserNameAsync(string userName, List<string> includeNames = null)
        {
            var predicate = new PredicateGroup<UserBase>();
            predicate.AddPredicate(true, x => x.UserName == userName && !x.IsLockoutEnaled);

            var userBases = await _userBaseRepository.QueryAsync(
                predicate.Predicates,
                includeNames);

            return userBases.FirstOrDefault();
        }
        /// <summary>
        ///     根据用户名获取
        /// </summary>
        /// <param name="userNameOrPhoneNumber"></param>
        /// <returns></returns>
        public async Task<UserBase> GetUserBaseByIncludeAsync(string userNameOrPhoneNumber)
        {
            var predicate = new PredicateGroup<UserBase>();
            predicate.AddPredicate(true, x => x.UserName == userNameOrPhoneNumber);
            var user = await _userBaseRepository.QueryAsync(predicate.Predicates,
                new List<string> {"Roles", "UserClaims"});

            return user.FirstOrDefault();
        }
        /// <summary>
        ///     获取用户角色Id集合
        /// </summary>
        /// <param name="userBaseId"></param>
        /// <returns></returns>
        public async Task<long[]> GetUserRoleListAsync(long userBaseId)
        {
            return await _userBaseRepository.GetAll()
                .Where(x => x.Id == userBaseId).AsNoTracking()
                .Include("Roles")
                .SelectMany(x =>
                    x.Roles.Select(y => y.Id)).ToArrayAsync();
        }
        /// <summary>
        ///     新增用户返回Id
        /// </summary>
        /// <param name="user">用户基础实体</param>
        /// <returns></returns>      
        public async Task<long> CreateUserBaseAndGetIdAsync(UserBase user)
        {
            return await _userBaseRepository.InsertAndGetIdAsync(user);
        }
    }
}