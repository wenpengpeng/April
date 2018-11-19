// 文件名：ManagerAppService.cs
// 
// 创建标识：温朋朋 2018-06-21 17:15
// 
// 修改标识：温朋朋2018-06-21 17:15
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using April.Application.Authorizations;
using April.Application.Authorizations.Entity;
using April.Application.Commons;
using April.Application.Permissions.Managers.Dtos;
using April.Application.Permissions.Roles.Dtos;
using April.Common.AutoMap;
using April.Common.Exceptions;
using April.Common.Predicates;
using Domain.Core.AprilSessions;
using Domain.Core.Enums.Users;
using Domain.Core.Permissions.Managers;
using Domain.Core.Permissions.Roles;
using Domain.Core.Permissions.Users;
using Microsoft.AspNet.Identity;

namespace April.Application.Permissions.Managers
{
    [AppAuthorize(Code = "Manager", Name = "运营管理员")]
    public class ManagerAppService : AprilAppServiceBase, IManagerAppService
    {
        #region IOC
        private readonly ManagerMange _managerManage;
        private readonly RoleManage _roleManage;
        private readonly UserBaseManage _userBaseManage;

        /// <summary>
        ///     构造函数
        /// </summary>
        public ManagerAppService(ManagerMange managerManage, RoleManage roleManage, UserBaseManage userBaseManage)
        {
            _managerManage = managerManage;
            _roleManage = roleManage;
            _userBaseManage = userBaseManage;
        }
        #endregion

        /// <summary>
        ///     新增后台管理用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission, Description = "新增运营", Action = BaseAction.Add)]
        public async Task InsertManagerAsync(CreateOrUpdateManagerInputDto input)
        {
            if (string.IsNullOrEmpty(input.UserBase.PasswordHash))
                throw new UserFriendlyException("密码不能为为空！");

            //自动转换一下用户基础表
            var userBase = input.UserBase.MapTo<UserBase>();

            userBase.PasswordHash = new PasswordHasher().HashPassword(userBase.PasswordHash);
            userBase.SecurityStamp = Guid.NewGuid().ToString();
            userBase.IsEmailComfirmed = true;
            userBase.IsLockoutEnaled = false;
            userBase.IsPhoneNumberComfirmed = true;
            userBase.AccessFailedCount = 0;

            //用户角色
            var roles = await _roleManage.GetRoleListByIdsAsync(input.RoleIds);
            userBase.Roles = roles;

            //获取当前帐号类型
            var currentAccountType = (AccountTypeEnum)Convert.ToInt32(ClaimTypeExtensions.GetClaimValue(ClaimTypeExtensions.AccountType));

            //需要赋予的帐号类型
            var accountType = AccountTypeEnum.子帐号;
            if (currentAccountType == AccountTypeEnum.超级管理员)
            {
                accountType = AccountTypeEnum.主帐号;
            }

            //用户申明
            userBase.UserClaims = new List<UserClaim>{
                new UserClaim{
                    ClaimType = "AccountType",
                    ClaimValue = Convert.ToInt32(accountType).ToString()
                }
            };

            //实例化管理员
            userBase.Managers = new List<Manager>
            {
                new Manager()
            };
           

            var userId = await _userBaseManage.CreateUserBaseAndGetIdAsync(userBase);

            //如果管理员创建主帐号则将所属Id设置成当前自己的Id
            userBase.BelongUserId = currentAccountType == AccountTypeEnum.超级管理员 ? userId : AprilSession.BelongUserId;
            await _userBaseManage.UpdateUserBaseAsync(userBase);
        }
        /// <summary>
        ///     编辑或新增后台运营者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission, Description = "新增/编辑运营", Action = BaseAction.Add)]
        public async Task CreateOrUpdateManager(CreateOrUpdateManagerInputDto input)
        {
            if (input.Id > 0)
                await UpdateManagerAsync(input);
            else
                await InsertManagerAsync(input);
        }
        /// <summary>
        ///     根据后台用户Id查询（Manager表id）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission, Description = "查看运营", Action = BaseAction.View)]
        public async Task<GetManagerListDto> GetManagerByIdAsync(NullableIdDto<long> input)
        {
            GetManagerListDto entity;

            //获取用户当前权限下面的角色列表数据
            var roleList = await _roleManage.GetRoleAllAsync();

            if (input.Id.HasValue)
            {
                var query = await _managerManage.GetManagerByIdAsync(input.Id.Value, new List<string> { "UserBase.Roles" });

                if (query == null)
                    throw new UserFriendlyException($"操作失败，标识符[{input.Id}不存在或已被管理员删除]");

                entity = query.MapTo<GetManagerListDto>();

                entity.RoleList = roleList.MapTo<List<GetAllIncludeUserRole>>();

                entity.RoleList.ForEach(x =>
                {
                    x.IsChecked = entity.UserBase.Roles.Any(y => y.Id == x.Id);
                });
            }
            else
            {
                entity = new GetManagerListDto
                {
                    RoleList = roleList.MapTo<List<GetAllIncludeUserRole>>()
                };
            }

            return entity;
        }
        /// <summary>
        ///     锁定解锁管理员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission, Description = "锁定/解锁运营", Action = BaseAction.Lock)]
        public async Task LockManagerAsync(NullableIdDto<long> input)
        {
            if (!input.Id.HasValue)
                throw new UserFriendlyException("Id不能为空");
            await _managerManage.LockManagerAsync(input.Id.Value);
        }
        /// <summary>
        ///     修改后台管理用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission, Description = "修改运营", Action = BaseAction.Update)]
        public async Task UpdateManagerAsync(CreateOrUpdateManagerInputDto input)
        {
            //获取管理员信息
            var manager = await _managerManage.GetManagerByIdAsync(input.Id, new List<string> { "UserBase.Roles", "UserBase.UserClaims" });

            //不能修改登录用户名，密码
            input.UserBase.PasswordHash = manager.UserBase.PasswordHash;
            input.UserBase.UserName = manager.UserBase.UserName;

            //不是超级管理员不能进行主帐号修改操作
            if (manager.UserBase.UserClaims.Any(x => x.ClaimValue == AccountTypeEnum.主帐号.ToString()) && AprilSession.AccountType != AccountTypeEnum.超级管理员)
                throw new UserFriendlyException("运营主帐号不能进行修改操作");

            //自动转换一下用户基础表
            input.UserBase.MapTo(manager.UserBase);

            //用户角色
            var roles = await _roleManage.GetRoleListByIdsAsync(input.RoleIds);

            manager.UserBase.Roles?.Clear();

            manager.UserBase.Roles = roles;

            await _managerManage.UpdateManagerAsync(manager);
        }
        /// <summary>
        ///     删除后台管理用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteManagerAsync(NullableIdDto<long> input)
        {
            if (!input.Id.HasValue)
                throw new UserFriendlyException("Id不能为空");
            await _managerManage.DeleteManagerAsync(input.Id.Value);
        }
        /// <summary>
        ///     分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AppMethodAuthorize(AppAuthorize = ApplicationAuthorizeEnum.OperationPermission, Description = "运营列表", Action = BaseAction.Show)]
        public async Task<PagedResultDto<GetManagerListDto>> GetPagedManagerAsync(GetManagerPagerInput input)
        {
            //查询不是当前用户Id的子帐号数据
            Expression<Func<Manager, bool>> where = w =>
                w.UserBase.Id != AprilSession.UserId.Value;

            var predicate = new PredicateGroup<Manager>();

            predicate.AddPredicate(true, where);
            predicate.AddPredicate(AprilSession.AccountType != AccountTypeEnum.超级管理员, w => w.UserBase.BelongUserId.Value == AprilSession.BelongUserId);

            predicate.AddPredicate(!string.IsNullOrEmpty(input.UserName), s => s.UserBase.UserName.Contains(input.UserName));
            predicate.AddPredicate(!string.IsNullOrEmpty(input.RealName), s => s.UserBase.RealName.Contains(input.RealName));
            predicate.AddPredicate(!string.IsNullOrEmpty(input.PhoneNumber), s => s.UserBase.PhoneNumber.Contains(input.PhoneNumber));

            var tuple = await _managerManage.GetPagedMemberAsync(predicate.Predicates,
                input, new List<string> { "UserBase" });

            var roleListDtos = tuple.Item1.MapTo<List<GetManagerListDto>>();

            return new PagedResultDto<GetManagerListDto>(
                tuple.Item2,
                roleListDtos
            );
        }
    }
}