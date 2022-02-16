using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net;
using log4net.Config;

namespace CheckZ2ForProximityCardAndSend2WS
{
    public static class Logger
    {
        private static ILog _log = null;
        public static ILog Log
        {
            get
            {
                if (_log == null)
                {
                    XmlConfigurator.Configure();
                    _log = LogManager.GetLogger("LOGGER");
                }

                return _log;
            }
        } 
    }
}
