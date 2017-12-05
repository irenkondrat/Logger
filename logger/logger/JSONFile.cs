using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace logger
{
    class JSONFile : FileBase, IDisposable
    {
        private bool _disposed ;


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
            lock (lockObj)
            {
                File.AppendAllText(NameFile, new JObject
                {
                    {"LogString", logString},
                    {"LogLevel", logLevel},
                    {"Module", module},
                    {"Date", date}
                }.ToString());
            }
            return true;
        }

      

    }
}
