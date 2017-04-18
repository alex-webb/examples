namespace Log4NetExample
{
    public class EventLogAppender
    {
        public static void LogToEventLog()
        {
            log4net.Config.XmlConfigurator.Configure();

            log4net.ILog log = log4net.LogManager.GetLogger(typeof(EventLogAppender));

            log.Debug("DEBUG");
            log.Info("INFO");
            log.Warn("WARN");
            log.Error("ERROR");
            log.Fatal("FATAL");
        }
    }
}
