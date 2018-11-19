// 文件名：IConventionalDependencyRegistrar.cs
// 
// 创建标识：温朋朋 2018-05-04 11:20
// 
// 修改标识：温朋朋2018-05-04 11:20
// 
// ------------------------------------------------------------------------------
namespace April.Core.Ioc
{
    /// <summary>
    ///     根据惯例注册
    /// </summary>
    public interface IConventionalDependencyRegistrar
    {
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}