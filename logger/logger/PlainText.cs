using System;
using System.IO;

namespace logger
{
    class PlainText : FileBase
    {
        private bool _disposed ;

        public PlainText(string nameFile) : base(nameFile)
        { }

        public override bool WrtFile(string logString, string logLevel, string module, DateTime date)
        {   
            var file = new FileInfo(NameFile);
            if (!file.Exists)
            {
                file.Create().Dispose();
              
            }
             lock (lockObj)
            {
            _sw = new StreamWriter(NameFile);
            _sw.Write($"{date}| {logLevel}| {logString}| {module}");
            }
            return true;
        }

    }
}
