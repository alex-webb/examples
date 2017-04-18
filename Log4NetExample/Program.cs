using System;

namespace Log4NetExample
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

            log.Debug("DEBUG");
            log.Info("INFO");
            log.Warn("WARN");
            log.Error("ERROR");
            log.Fatal("FATAL");

            RollingFileAppender.LogThings();
            EventLogAppender.LogToEventLog();
            AdoNetAppender.AdoNetLog();

            //Console.ReadLine();
        }
    }
}
