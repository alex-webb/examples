using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log4NetExample
{
    public class AdoNetAppender
    {
        public static void AdoNetLog()
        {
            log4net.Config.XmlConfigurator.Configure();

            log4net.ILog log = log4net.LogManager.GetLogger(typeof(AdoNetAppender));

            log.Debug("DEBUG");
            log.Info("INFO");
            log.Warn("WARN");
            log.Error("ERROR");
            log.Fatal("FATAL");
        }
    }
}
