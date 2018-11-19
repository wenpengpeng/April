// 文件名：IUnitOfWorkManager.cs
// 
// 创建标识：温朋朋 2018-05-15 9:15
// 
// 修改标识：温朋朋2018-05-15 9:15
// 
// ------------------------------------------------------------------------------
namespace April.Uow.UnitOfWorks
{
    public interface IUnitOfWorkManager
    {
        /// <summary>
        ///     获取当前工作单元，如不存在则为null
        /// </summary>
        IActiveUnitOfWork Current { get; }
        /// <summary>
        ///     开启一个新的工作单元
        /// </summary>
        /// <returns></returns>
        IUnitOfWorkCompleteHandle Begin();
    }
}