// 文件名：AprilBaseModule.cs
// 
// 创建标识：温朋朋 2018-05-08 15:44
// 
// 修改标识：温朋朋2018-05-08 15:44
// 
// ------------------------------------------------------------------------------

using April.Core.Ioc;

namespace April.Core.Module
{
    public class AprilBaseModule:Autofac.Module
    {
        /// <summary>
        ///     IocManager
        /// </summary>
        public IIocManager IocManager { get; set; }
    }
}