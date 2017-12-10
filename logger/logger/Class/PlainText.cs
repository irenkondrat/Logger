using System;
using System.IO;

namespace logger.Class
{
    class PlainText : FileBase
    {
        public PlainText(string nameFile) : base(nameFile)
        { }

        public override bool WrtFile(string logString, string logLevel, string module, DateTime date)
        {   
            var file = new FileInfo(NameFile);
            if (!file.Exists)
            {
                file.Create().Dispose();
              
            }
             lock (LockObj)
            {
            Sw = File.AppendText(NameFile);
            Sw.WriteLine($"{date} | {logLevel} | {logString} | {module}");
            }


            return true;
        }

    }
}
