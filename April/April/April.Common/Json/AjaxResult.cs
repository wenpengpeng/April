// 文件名：AjaxResult.cs
// 
// 创建标识：温朋朋 2018-05-30 16:58
// 
// 修改标识：温朋朋2018-05-30 16:58
// 
// ------------------------------------------------------------------------------
namespace April.Common.Json
{
    public class AjaxResult
    {
        /// <summary>
        ///     成功或失败（默认为true）
        /// </summary>
        public bool Successed { get; set; } = true;

        /// <summary>
        ///     成功或失败消息
        /// </summary>
        public string Message { get; set; } = "";
        /// <summary>
        ///     传输的数据
        /// </summary>
        public object Result { get; set; }
    }
}