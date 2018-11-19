// 文件名：PageQueryEntity.cs
// 
// 创建标识：温朋朋 2018-05-31 15:17
// 
// 修改标识：温朋朋2018-05-31 15:17
// 
// ------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Domain.Core.Pages
{
    /// <summary>
    ///     分页查询实体
    /// </summary>
    public class PageQueryEntity
    {
        public PageQueryEntity()
        {
            // 默认Id倒序排序
            if (string.IsNullOrWhiteSpace(Sorting))
                Sorting = "Id DESC";
        }
        /// <summary>
        ///     Sorting
        /// </summary>
        public string Sorting { get; set; }

        /// <summary>
        ///     SkipCount
        /// </summary>
        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        /// <summary>
        ///     MaxResultCount
        /// </summary>
        public int MaxResultCount { get; set; }
    }
}