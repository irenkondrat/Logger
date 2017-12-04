using System;
using System.Configuration;

namespace logger
{
    [FlagsAttribute]
    public enum LoggerLevel { Debug=0, Info=1, Warn=2, Error=4, Fatal=8 };

    public class Logger : ILogger
    {
        protected FileBase file ;

        public Logger()
        {
            string nameFile = GetSetting("FileName") ?? "LogFile";

            switch (GetSetting("Type"))
            {
                case "xml":
                    file = new XMLFile($"{nameFile}.xml");
                    break;
                case "plain":
                    file = new PlainText($"{nameFile}.txt");
                    break;
                case "json":
                    file = new JSONFile($"{nameFile}.json");
                    break;
                default:
                    file = new XMLFile($"{nameFile}.xml");
                    return;
            }
        }

        public void Log(string logString)
        {
            var logLevel = "Info";

            string minlevel = GetSetting("Minlevel");
            if (Enum.IsDefined(typeof(LoggerLevel), minlevel))
                logLevel = minlevel;

            file.WrtFile(logString, logLevel, "", DateTime.Now);
        }

        public void Log(string logString, LoggerLevel logLevel)
        {
            file.WrtFile(logString, logLevel.ToString(), "", DateTime.Now);
        }

        public void Log(string logString, LoggerLevel logLevel, string module)
        {
            file.WrtFile(logString, logLevel.ToString(), module, DateTime.Now);
        }

        protected string GetSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                //Console.WriteLine("Error reading app settings");
                return null;
            }
        }
    }

}
