using System;
using System.IO;

namespace logger
{
    class PlainText : FileBase,IDisposable
    {
        private bool _disposed ;

        private StreamWriter _sw;

        public PlainText(string nameFile) : base(nameFile)
        { }

        public override bool WrtFile(string logString, string logLevel, string module, DateTime date)
        {   
            var file = new FileInfo(NameFile);
            if (!file.Exists)
            {
                file.Create().Dispose();
                lock (lockObj)
                {
                    _sw = new StreamWriter(NameFile);
                    _sw.Write($"{date}| {logLevel}| {logString}| {module}");                   
                }
            }
            return true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _sw.Dispose();
                    
                }
                _disposed = true;
                NameFile = null;

            }
        }

        ~PlainText()
        {
            Dispose(false);
        }

    }
}
