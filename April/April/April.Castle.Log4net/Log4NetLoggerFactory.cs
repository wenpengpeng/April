// 文件名：Log4NetLoggerFactory.cs
// 
// 创建标识：温朋朋 2018-05-18 10:28
// 
// 修改标识：温朋朋2018-05-18 10:28
// 
// ------------------------------------------------------------------------------

using System;
using System.IO;
using Castle.Core.Logging;
using log4net;
using log4net.Repository;
using System.Reflection;
using System.Xml;
using log4net.Config;

namespace April.Castle.Log4net
{
    public class Log4NetLoggerFactory:AbstractLoggerFactory
    {
        internal const string DefaultConfigFileName = "log4net.config";
        private readonly ILoggerRepository _loggerRepository;

        public Log4NetLoggerFactory()
            : this(DefaultConfigFileName)
        {
            
        }
        public Log4NetLoggerFactory(string configFileName)
        {
            _loggerRepository = LogManager.CreateRepository(typeof(Log4NetLoggerFactory).GetTypeInfo().Assembly,
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            //加载配置文件
            var log4NetConfig = new XmlDocument();
            log4NetConfig.Load(File.OpenRead(configFileName));
            XmlConfigurator.Configure(_loggerRepository,log4NetConfig["log4net"]);
        }
        public override ILogger Create(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            return new Log4NetLogger(LogManager.GetLogger(_loggerRepository.Name,name),this);
        }

        public override ILogger Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }
    }
}