// 文件名：User.cs
// 
// 创建标识：温朋朋 2018-05-25 16:28
// 
// 修改标识：温朋朋2018-05-25 16:28
// 
// ------------------------------------------------------------------------------
namespace Domain.Core.Enums.Users
{
    /// <summary>
    ///     账户类型
    /// </summary>
    public enum AccountTypeEnum
    {
        其他 = 0,

        超级管理员 = 1,

        主帐号 = 2,

        子帐号 = 3,

        会员 = 4
    }
    /// <summary>
    ///     会员类型
    /// </summary>
    public enum MemberTypeEnum
    {
        全部 = 0,
        采购商供应商 = 1,
        采购商 = 2,
        供应商 = 3,
        其他 = -1
    }
    /// <summary>
    ///     默认角色类别
    /// </summary>
    public enum DefaultRoleTypeEnum
    {
        超级管理员 = 2,
        运营者 = 3,
        采购商 = 6,
        供应商 = 4,
        会员 = 7,
    }
    /// <summary>
    ///     菜单目标
    /// </summary>
    public enum MenuTargetEnum
    {
        新开页面 = 1,

        自身打开 = 2
    }
    /// <summary>
    ///     菜单的分类
    /// </summary>
    public enum MenuCategoryEnum
    {
        买家中心 = 1,

        卖家中心 = 2,

        运营中心 = 3,

        会员中心 = 4,

        超级管理员 = 5
    }
    /// <summary>
    ///     采购商审核状态
    /// </summary>
    public enum BuyerAuditEnum
    {
        未申请 = -1,

        待审核 = 1,

        审核未通过 = 2,

        审核通过 = 3
    }
    /// <summary>
    ///     供应商审核状态
    /// </summary>
    public enum SupplierAuditEnum
    {
        未申请 = -1,

        待审核 = 1,

        审核未通过 = 2,

        审核通过 = 3
    }
}