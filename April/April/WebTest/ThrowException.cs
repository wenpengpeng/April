// 文件名：ThrowException.cs
// 
// 创建标识：温朋朋 2018-05-24 10:51
// 
// 修改标识：温朋朋2018-05-24 10:51
// 
// ------------------------------------------------------------------------------

using April.Web.Auditing;
using Autofac.Extras.DynamicProxy;

namespace WebTest
{
    [Intercept("AuditingInterceptor")]
    public class ThrowException:IThrowException
    {        
        public virtual double Throw()
        {
            var a = 0;
            return 10 / a;
        }
    }
    //[Audited]
    //public class ThrowExceptionChild: IThrowException
    //{
    //    public  double Throw()
    //    {
    //        var a = 0;
    //        return 10 / a;
    //    }
    //}
}