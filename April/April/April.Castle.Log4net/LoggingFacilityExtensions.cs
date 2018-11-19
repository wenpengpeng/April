// 文件名：LoggingFacilityExtensions.cs
// 
// 创建标识：温朋朋 2018-05-18 11:16
// 
// 修改标识：温朋朋2018-05-18 11:16
// 
// ------------------------------------------------------------------------------

using Castle.Facilities.Logging;

namespace April.Castle.Log4net
{
    public static class LoggingFacilityExtensions
    {
        public static LoggingFacility UseAprilLog4Nemt(this LoggingFacility loggingFacility)
        {
            return loggingFacility.LogUsing<Log4NetLoggerFactory>();
        }
    }
}