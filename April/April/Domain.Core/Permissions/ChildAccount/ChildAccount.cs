// 文件名：ChildAccount.cs
// 
// 创建标识：温朋朋 2018-05-30 13:41
// 
// 修改标识：温朋朋2018-05-30 13:41
// 
// ------------------------------------------------------------------------------

using April.Uow.Repositories;

namespace Domain.Core.Permissions.ChildAccount
{
    public class ChildAccount:IEntity<long>,IChildAccount
    {
        public long Id { get; set; }
        public long? BelongUserId { get; set; }
    }
}