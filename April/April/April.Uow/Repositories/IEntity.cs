// 文件名：IEntity.cs
// 
// 创建标识：温朋朋 2018-05-09 15:09
// 
// 修改标识：温朋朋2018-05-09 15:09
// 
// ------------------------------------------------------------------------------
namespace April.Uow.Repositories
{
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        ///     主键
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}