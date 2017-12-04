using System;

namespace logger
{
   public abstract class FileBase
    {
        public string NameFile;

        protected readonly object lockObj = new object();


        protected FileBase(string nameFile)
        {
            NameFile = nameFile;
        }

        public abstract bool WrtFile(string logString, string logLevel, string module, DateTime date);
    }
}
