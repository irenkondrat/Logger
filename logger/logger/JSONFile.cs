using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace logger
{
    class JSONFile : FileBase, IDisposable
    {
        private bool _disposed ;

        private StreamWriter _sw;

        public JSONFile(string nameFile) : base(nameFile)
        {}

        public override bool WrtFile(string logString, string logLevel, string module, DateTime date)
        {
            var file = new FileInfo(NameFile);
            if (!file.Exists)
            {
                _sw = File.CreateText(NameFile);
            }
            else
            {
               _sw = File.AppendText(NameFile);
            }
            File.AppendAllText(NameFile,new JObject
            {
                {"LogString", logString},
                {"LogLevel", logLevel},
                {"Module", module},
                {"Date", date}
            }.ToString());

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

        ~JSONFile()
        {
            Dispose(false);
        }


    }
}
