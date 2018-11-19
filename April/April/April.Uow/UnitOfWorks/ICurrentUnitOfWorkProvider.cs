// 文件名：ICurrentUnitOfWorkProvider.cs
// 
// 创建标识：温朋朋 2018-05-15 9:21
// 
// 修改标识：温朋朋2018-05-15 9:21
// 
// ------------------------------------------------------------------------------
namespace April.Uow.UnitOfWorks
{
    public interface ICurrentUnitOfWorkProvider
    {
        /// <summary>
        ///     获取或设置工作单元
        /// </summary>
        IUnitOfWork Current { get; set; }
    }
}