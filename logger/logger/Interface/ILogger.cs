using KLogger;

namespace logger.Interface
{
    interface ILogger
    {
        void Log(string logString);

        void Log(string logString, LoggerLevel logLevel);

        void Log(string logString, LoggerLevel logLevel, string module);

    }
}
