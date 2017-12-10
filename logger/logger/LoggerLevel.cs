using System;

namespace KLogger
{
    [Flags]
    public enum LoggerLevel
    { Debug = 0, Info = 1, Warn = 2, Error = 4, Fatal = 8 };
}
