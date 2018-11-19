// 文件名：IConventionalRegistrationContext.cs
// 
// 创建标识：温朋朋 2018-05-04 11:22
// 
// 修改标识：温朋朋2018-05-04 11:22
// 
// ------------------------------------------------------------------------------

using System.Reflection;

namespace April.Core.Ioc
{
    public interface IConventionalRegistrationContext
    {
        /// <summary>
        ///     程序集
        /// </summary>
        Assembly Assembly { get; }
        /// <summary>
        ///     Ioc管理对象
        /// </summary>
        IIocManager IocManager { get; }
    }
}