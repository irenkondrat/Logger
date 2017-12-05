using System;
using System.Configuration;

namespace logger
{
    [FlagsAttribute]
    public enum LoggerLevel { Debug=0, Info=1, Warn=2, Error=4, Fatal=8 };

    public class Logger : ILogger
    {
        protected FileBase File ;

        public Logger()
        {
            string nameFile = GetSetting("FileName") ?? "LogFile";

            switch (GetSetting("Type"))
            {
                case "xml":
                    File = new XMLFile($"{nameFile}.xml");
                    break;
                case "plain":
                    File = new PlainText($"{nameFile}.txt");
                    break;
                case "json":
                    File = new JSONFile($"{nameFile}.json");
                    break;
                default:
                    File = new PlainText($"{nameFile}.txt");
                    return;
            }
        }

        public void Log(string logString)
        {
            var logLevel = "Info";

        /*    string minlevel = GetSetting("Minlevel");
            if (Enum.IsDefined(typeof(LoggerLevel), minlevel))
                logLevel = minlevel;*/

            File.WrtFile(logString, logLevel, "", DateTime.Now);
        }

        public void Log(string logString, LoggerLevel logLevel)
        {
            Log(logString, logLevel, "");
            
        }

        public void Log(string logString, LoggerLevel logLevel, string module)
        {
            File.WrtFile(logString, logLevel.ToString(), module, DateTime.Now);
            File.Dispose();
        }

        protected string GetSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? null;
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
