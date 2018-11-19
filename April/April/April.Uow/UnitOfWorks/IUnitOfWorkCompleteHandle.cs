// 文件名：IUnitOfWorkCompleteHandle.cs
// 
// 创建标识：温朋朋 2018-05-09 15:47
// 
// 修改标识：温朋朋2018-05-09 15:47
// 
// ------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace April.Uow.UnitOfWorks
{
    public interface IUnitOfWorkCompleteHandle:IDisposable
    {
        /// <summary>
        ///     完成工作单元
        /// </summary>
        void Complete();
        /// <summary>
        ///     异步完成工作单元
        /// </summary>
        Task CompleteAsync();
    }
}