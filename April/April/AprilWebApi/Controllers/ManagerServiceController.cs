using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using April.Application.Commons;
using April.Application.Permissions.Managers;
using April.Application.Permissions.Managers.Dtos;

namespace AprilWebApi.Controllers
{
    public class ManagerServiceController : AprilWebApiBaseController
    {
        private readonly IManagerAppService _managerAppService;

        public ManagerServiceController(IManagerAppService managerAppService)
        {
            _managerAppService = managerAppService;
        }

        /// <summary>
        ///     新增后台管理用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task InsertManagerAsync(CreateOrUpdateManagerInputDto input)
        {
            await _managerAppService.InsertManagerAsync(input);
        }

        /// <summary>
        ///     编辑或新增后台运营者
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateOrUpdateManager(CreateOrUpdateManagerInputDto input)
        {
            await _managerAppService.CreateOrUpdateManager(input);
        }

        /// <summary>
        ///     根据后台用户Id查询（Manager表id）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GetManagerListDto> GetManagerByIdAsync(NullableIdDto<long> input)
        {
            return await _managerAppService.GetManagerByIdAsync(input);
        }

        /// <summary>
        ///     锁定解锁管理员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task LockManagerAsync(NullableIdDto<long> input)
        {
            await _managerAppService.LockManagerAsync(input);
        }

        /// <summary>
        ///     修改后台管理用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateManagerAsync(CreateOrUpdateManagerInputDto input)
        {
            await _managerAppService.UpdateManagerAsync(input);
        }

        /// <summary>
        ///     删除后台管理用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task DeleteManagerAsync(NullableIdDto<long> input)
        {
            await _managerAppService.DeleteManagerAsync(input);
        }

        /// <summary>
        ///     分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PagedResultDto<GetManagerListDto>> GetPagedManagerAsync(GetManagerPagerInput input)
        {
            return await _managerAppService.GetPagedManagerAsync(input);
        }
    }
}
