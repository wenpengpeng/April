// 文件名：IActiveUnitOfWork.cs
// 
// 创建标识：温朋朋 2018-05-09 15:43
// 
// 修改标识：温朋朋2018-05-09 15:43
// 
// ------------------------------------------------------------------------------

using System.Threading.Tasks;

namespace April.Uow.UnitOfWorks
{
    public interface IActiveUnitOfWork
    {
        /// <summary>
        /// Is this UOW disposed?
        /// </summary>
        bool IsDisposed { get; }
        /// <summary>
        ///     保存工作单元中的更改
        /// </summary>
        void SaveChanges();
        /// <summary>
        ///     异步保存工作单元中的更改
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}