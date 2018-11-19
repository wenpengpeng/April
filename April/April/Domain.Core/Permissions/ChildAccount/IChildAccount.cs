// 文件名：IChildAccount.cs
// 
// 创建标识：温朋朋 2018-05-30 13:41
// 
// 修改标识：温朋朋2018-05-30 13:41
// 
// ------------------------------------------------------------------------------
namespace Domain.Core.Permissions.ChildAccount
{
    public interface IChildAccount
    {
        long? BelongUserId { get; set; }
    }
}