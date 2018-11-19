// 文件名：IManagerAppService.cs
// 
// 创建标识：温朋朋 2018-06-21 17:06
// 
// 修改标识：温朋朋2018-06-21 17:06
// 
// ------------------------------------------------------------------------------

using System.Threading.Tasks;
using April.Application.Commons;
using April.Application.Permissions.Managers.Dtos;
using April.Web.Services;

namespace April.Application.Permissions.Managers
{
    public interface IManagerAppService: IApplicationService
    {
        /// <summary>
        /// 新增后台管理用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task InsertManagerAsync(CreateOrUpdateManagerInputDto input);

        /// <summary>
        ///   新增或者编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdateManager(CreateOrUpdateManagerInputDto input);

        /// <summary>
        /// 根据后台用户Id查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetManagerListDto> GetManagerByIdAsync(NullableIdDto<long> input);

        /// <summary>
        ///   解锁，锁定运营管理员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task LockManagerAsync(NullableIdDto<long> input);

        /// <summary>
        /// 修改后台管理用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateManagerAsync(CreateOrUpdateManagerInputDto input);

        /// <summary>
        /// 删除后台管理用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteManagerAsync(NullableIdDto<long> input);

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<GetManagerListDto>> GetPagedManagerAsync(GetManagerPagerInput input);
    }
}