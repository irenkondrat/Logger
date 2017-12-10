using System;
using System.Configuration;
using logger.Class;
using logger.Interface;

namespace KLogger
{
   
    public class Logger : ILogger
    {
        protected FileBase File ;

        protected string NameClass;
        
        public Logger(string nameClass)
        {
            NameClass = nameClass;
            string fileFormat = GetSetting("Type") ?? ".txt";
            string fileName = GetSetting("FileName") ?? "LogFile";
            File = new LoggerFileFactory().GetLoggerFileFactory(fileFormat, fileName);
        }

        public void Log(string logString)
        {
            string logLevel = "Info";

            File.WrtFile(logString, logLevel, NameClass, DateTime.Now);
        }

        public void Log(string logString, LoggerLevel logLevel)
        {
            Log(logString, logLevel, NameClass);           
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
                string result = appSettings[key];
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return null;
            }
        }
    }

}
