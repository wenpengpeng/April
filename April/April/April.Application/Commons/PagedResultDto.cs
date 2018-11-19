// 文件名：PagedResultDto.cs
// 
// 创建标识：温朋朋 2018-06-21 14:37
// 
// 修改标识：温朋朋2018-06-21 14:37
// 
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace April.Application.Commons
{
    [Serializable]
    public class PagedResultDto<T>
    {
        /// <summary>
        ///     结果集
        /// </summary>
        public ICollection<T> Items { get; set; }
        /// <summary>
        ///     总数
        /// </summary>
        public int TotalCount { get; set; }

        public PagedResultDto()
        {
            
        }

        public PagedResultDto(int totalCount, ICollection<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}