namespace Log4NetExample
{
    public class RollingFileAppender
    {
        public static void LogThings()
        {
            log4net.Config.XmlConfigurator.Configure();

            log4net.ILog log = log4net.LogManager.GetLogger(typeof(RollingFileAppender));

            log.Debug("DEBUG");
            log.Info("INFO");
            log.Warn("WARN");
            log.Error("ERROR");
            log.Fatal("FATAL");
        }
    }
}
