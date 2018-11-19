// 文件名：NullableIdDto.cs
// 
// 创建标识：温朋朋 2018-06-21 14:32
// 
// 修改标识：温朋朋2018-06-21 14:32
// 
// ------------------------------------------------------------------------------

using System;

namespace April.Application.Commons
{
    [Serializable]
    public class NullableIdDto<TId> where TId:struct
    {
        public NullableIdDto()
        {
            
        }

        public NullableIdDto(TId? id)
        {
            Id = id;
        }
        public TId? Id { get; set; }
    }
}