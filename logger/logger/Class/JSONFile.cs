using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace logger.Class
{
    class JsonFile : FileBase
    {

        public JsonFile(string nameFile) : base(nameFile)
        {
        }

        public override bool WrtFile(string logString, string logLevel, string module, DateTime date)
        {
            lock (LockObj)
            {
                    using (var fStream = new FileStream(NameFile, FileMode.Append, FileAccess.Write))
                    {
                        Sw = new StreamWriter(fStream);
                        
                            Sw.WriteLine(new JObject
                            {
                                {"LogString", logString},
                                {"LogLevel", logLevel},
                                {"Module", module},
                                {"Date", date}
                            });
                          Dispose(true);                  
                    }             
            }
            return true;
        }
    }

}
