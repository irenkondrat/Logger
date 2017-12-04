using System;
using System.IO;
using System.Xml;

namespace logger
{
    public class XMLFile : FileBase, IDisposable
    {
        private bool _disposed;

        private XmlWriter _xmlWriter;

        public XMLFile(string nameFile):base(nameFile)
        { }

        public override bool WrtFile(string logString, string logLevel, string module, DateTime date)
        {
            var file = new FileInfo(NameFile);
            if (!file.Exists)
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.NewLineOnAttributes = true;
                _xmlWriter = XmlWriter.Create(NameFile, xmlWriterSettings);
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(NameFile);
                _xmlWriter = doc.CreateNavigator().AppendChild();
            }
            lock (lockObj)
            {
                _xmlWriter.WriteElementString("logString", logString);
                _xmlWriter.WriteEndElement();
                _xmlWriter.WriteElementString("loglevel", logLevel.ToString());
                _xmlWriter.WriteEndElement();
                _xmlWriter.WriteElementString("module", module);
                _xmlWriter.WriteEndElement();
                _xmlWriter.WriteElementString("date", date.ToString());
                _xmlWriter.WriteEndElement();
                _xmlWriter.WriteEndDocument();
                _xmlWriter.Flush();
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
                    _xmlWriter.Dispose();
                }
                _disposed = true;
                NameFile = null;
            }
        }

        ~XMLFile()
        {
            Dispose(false);
        }

    }
}

