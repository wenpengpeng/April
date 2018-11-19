// 文件名：IThrowException.cs
// 
// 创建标识：温朋朋 2018-05-24 14:23
// 
// 修改标识：温朋朋2018-05-24 14:23
// 
// ------------------------------------------------------------------------------

using Autofac.Extras.DynamicProxy;

namespace WebTest
{
    [Intercept("AuditingInterceptor")]
    public interface IThrowException
    {
        double Throw();
    }
}