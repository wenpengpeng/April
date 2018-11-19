// 文件名：DefaultPermissionCreator.cs
// 
// 创建标识：温朋朋 2018-05-30 11:16
// 
// 修改标识：温朋朋2018-05-30 11:16
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain.Core.Enums.Users;
using Domain.Core.Permissions.Menus;
using Domain.Core.Permissions.Roles;
using Domain.Core.Permissions.Users;
using Domain.EntityFramework.EntityFramework;

namespace Domain.EntityFramework.Migrations.Seed
{
    public class DefaultPermissionCreator
    {
        private readonly AprilWebDbContext _context;

        public DefaultPermissionCreator(AprilWebDbContext context)
        {
            _context = context;
        }
        /// <summary>
        ///     创建初始用户权限
        /// </summary>
        public void Create()
        {
            AddMenu();
            AddAdminRole();
            AddOperatorRole();
            AddBuyerRole();
            AddSupplierRole();
            AddMemberRole();
            AddAdminUser();
        }
        #region 添加初始菜单

        private void AddMenu()
        {
            var menu = _context.Menus.FirstOrDefault(m=>m.Code=="KongZhiTai1");
            if (menu != null)
                return;
            #region 控制台菜单
            menu = new Menu
            {
                Icon = "fa fa-bank",
                DisplayName = "控制台",
                IsMenu = true,
                IsExpand = false,
                IsPublic = false,
                IsInterface = true,
                Remark = "控制台",
                IsValid = true,
                Code = "KongZhiTai1",
                Category = MenuCategoryEnum.运营中心,
                Layer = "1,",
                HasLevel = true,
                Sort = 1,
                CreationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Childrens = new List<Menu>
                {
                    new Menu
                    {
                        Icon = "fa fa-recycle",
                        DisplayName = "系统管理",
                        IsMenu = true,
                        IsExpand = false,
                        IsPublic = false,
                        IsInterface = true,
                        Remark = "系统管理",
                        IsValid = true,
                        Code = "XiTongGuanLi2",
                        Category = MenuCategoryEnum.运营中心,
                        Layer = "1,2",
                        HasLevel = true,
                        Sort = 1,
                        CreationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        Childrens=new List<Menu>
                        {
                            new Menu
                            {
                                Icon = "fa fa-recycle",
                                DisplayName = "角色管理",
                                RequestUrl = "/App/Main/views/role/index.html",
                                IsMenu = true,
                                IsExpand = true,
                                IsPublic = false,
                                IsInterface = false,
                                Remark = "系统管理",
                                IsValid = true,
                                Code = "JiaoSeGuanLi4",
                                Category = MenuCategoryEnum.运营中心,
                                Layer = "1,2,4",
                                HasLevel = false,
                                Sort = 1,
                                CreationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture)
                            },
                            new Menu
                            {
                                Icon = "fa fa-recycle",
                                DisplayName = "菜单管理",
                                RequestUrl = "/App/Main/views/menu/index.html",
                                IsMenu = true,
                                IsExpand = true,
                                IsPublic = false,
                                IsInterface = false,
                                Remark = "菜单管理",
                                IsValid = true,
                                Code = "CaiDanGuanLi5",
                                Category = MenuCategoryEnum.运营中心,
                                Layer = "1,2,5",
                                HasLevel = false,
                                Sort = 2,
                                CreationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture)
                            }
                        }
                    },
                    new Menu
                    {
                        Icon="fa fa-recycle",
                        DisplayName="工作台",
                        IsMenu=true,
                        IsExpand=false,
                        IsPublic=false,
                        IsInterface=false,
                        Remark="工作台",
                        IsValid=true,
                        Code="GongZuoTai3",
                        Category=MenuCategoryEnum.运营中心,
                        Layer="1,3",
                        HasLevel=true,
                        Sort=2,
                        CreationTime=DateTime.Now.ToString(CultureInfo.InvariantCulture),
                        Childrens=new List<Menu>
                        {
                            new Menu
                            {
                                Icon="fa fa-recycle",
                                DisplayName="维护",
                                RequestUrl="/App/Main/views/system/maintenance.html",
                                IsMenu=true,
                                IsExpand=true,
                                IsPublic=false,
                                IsInterface=false,
                                Remark="维护",
                                IsValid=true,
                                Code="WeiHu6",
                                Category=MenuCategoryEnum.运营中心,
                                Layer="1,3,6",
                                HasLevel=false,
                                Sort=1,
                                CreationTime=DateTime.Now.ToString(CultureInfo.InvariantCulture)
                            }
                        }
                    }
                }
            }; 
            #endregion

            _context.Menus.Add(menu);
            _context.SaveChanges();
        }
        #endregion

        #region 添加Admin角色

        private void AddAdminRole()
        {
            var adminRole = _context.Roles.FirstOrDefault(a=>a.Code=="admin1");
            if (adminRole != null)
                return;
            var menu = _context.Menus.Where(m => m.Layer.Contains("1,")).ToList();
            //新增超级管理员
            adminRole = new Role
            {
                Name = "超级管理员",
                Code = "admin1",
                IsValid = true,
                DefaultRoleType = DefaultRoleTypeEnum.超级管理员,
                IsSystem = true,
                CreationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                BelongUserId = 1,
                Menus = menu
            };
            _context.Roles.Add(adminRole);
            _context.SaveChanges();
        }
        #endregion

        #region 添加操作角色

        private void AddOperatorRole()
        {
            var operatorRole = _context.Roles.FirstOrDefault(u => u.Code == "operator2");
            if (operatorRole != null)
                return;
            operatorRole = new Role
            {
                Name = "运营者",
                Code = "operator2",
                IsValid = true,
                DefaultRoleType = DefaultRoleTypeEnum.运营者,
                IsSystem = true,
                CreationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                BelongUserId = 1
            };
            _context.Roles.Add(operatorRole);
            _context.SaveChanges();
        }
        #endregion

        #region 添加采购商角色

        private void AddBuyerRole()
        {
            var buyerRole = _context.Roles.FirstOrDefault(u => u.Code == "buyer3");
            if (buyerRole != null)
                return;
            buyerRole = new Role
            {
                Name = "采购商",
                Code = "buyer3",
                IsValid = true,
                DefaultRoleType = DefaultRoleTypeEnum.采购商,
                IsSystem = true,
                CreationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                BelongUserId = 1
            };

            _context.Roles.Add(buyerRole);
            _context.SaveChanges();
        }
        #endregion

        #region 添加供应商角色

        private void AddSupplierRole()
        {
            var supplierRole = _context.Roles.FirstOrDefault(u => u.Code == "supplier4");
            if (supplierRole != null)
                return;
            supplierRole = new Role
            {
                Name = "供应商",
                Code = "supplier4",
                IsValid = true,
                DefaultRoleType = DefaultRoleTypeEnum.供应商,
                IsSystem = true,
                CreationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                BelongUserId = 1
            };

            _context.Roles.Add(supplierRole);
            _context.SaveChanges();
        }

        #endregion

        #region 添加会员角色
        private void AddMemberRole()
        {
            var memberRole = _context.Roles.FirstOrDefault(u => u.Code == "member5");
            if (memberRole != null)
                return;
            memberRole = new Role
            {
                Name = "会员",
                Code = "member5",
                IsValid = true,
                DefaultRoleType = DefaultRoleTypeEnum.会员,
                IsSystem = true,
                CreationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                BelongUserId = 1
            };

            _context.Roles.Add(memberRole);
            _context.SaveChanges();
        }
        #endregion

        #region 添加超级管理员

        private void AddAdminUser()
        {
            var adminUser = _context.UserBases.FirstOrDefault(u=>u.UserName== "administrator");
            if (adminUser != null)
                return;
            var adminRole = _context.Roles.FirstOrDefault(r=>r.Code=="admin1");
            adminUser = new UserBase
            {
                UserName = "administrator",
                IsLockoutEnaled = false,
                SecurityStamp = "",
                PasswordHash = "ALKmFYD2UDGMr6kNJJcewr1sWmxHVud1YWkxCJOONbGA/pWZIp7VYssj3yrUv2XrXA==",
                IsEmailComfirmed = true,
                Email = "administrator@default.com",
                IsPhoneNumberComfirmed = true,
                RealName = "超级管理员",
                CreationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                BelongUserId = 1,
                Roles = new List<Role> {adminRole},
                UserClaims = new List<UserClaim>
                {
                    new UserClaim
                    {
                        ClaimType = "AccountType",
                        ClaimValue = "1",
                        UserId = 1,
                        CreationTime = DateTime.Now.ToString(CultureInfo.InvariantCulture)
                    }
                }
            };
            _context.UserBases.Add(adminUser);
            _context.SaveChanges();
        }
        #endregion
    }
}