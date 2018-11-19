// 文件名：IUnitOfWork.cs
// 
// 创建标识：温朋朋 2018-05-09 15:39
// 
// 修改标识：温朋朋2018-05-09 15:39
// 
// ------------------------------------------------------------------------------
namespace April.Uow.UnitOfWorks
{
    public interface IUnitOfWork:IActiveUnitOfWork,IUnitOfWorkCompleteHandle
    {
        /// <summary>
        ///     开启一个工作单元
        /// </summary>
        void Begin(); 
        /// <summary>
        ///     引用外部Uow如果存在
        /// </summary>
        IUnitOfWork Outer { get; set; }
    }
}