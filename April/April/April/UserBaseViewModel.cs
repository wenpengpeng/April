// 文件名：UserBaseViewModel.cs
// 
// 创建标识：温朋朋 2018-06-21 10:53
// 
// 修改标识：温朋朋2018-06-21 10:53
// 
// ------------------------------------------------------------------------------

using April.Common.AutoMap;
using Domain.Core.Permissions.Users;

namespace April
{
    [AutoMap(typeof(UserBase))]
    public class UserBaseViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}