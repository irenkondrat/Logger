using System;
using System.IO;
using System.Text;
using System.Xml;

namespace logger.Class
{
    public class XmlFile : FileBase, IDisposable
    {

        public XmlFile(string nameFile):base(nameFile)
        { }

        public override bool WrtFile(string logString, string logLevel, string module, DateTime date)
        {
            var file = new FileInfo(NameFile);
            if (!file.Exists)
            {
                XmlTextWriter textWritter = new XmlTextWriter(NameFile, Encoding.UTF8);
                textWritter.WriteStartElement("root");
                textWritter.WriteEndElement();
                textWritter.Close();
            }
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(NameFile);
           lock (LockObj)
            {
                XmlElement xRoot = xDoc.DocumentElement;

                XmlElement logElem = xDoc.CreateElement("Log");

                XmlAttribute levelAttr = xDoc.CreateAttribute("level");
               
                XmlElement dateElem = xDoc.CreateElement("Date");
                XmlElement messageElem = xDoc.CreateElement("Message");
                XmlElement moduleElem = xDoc.CreateElement("Module");

                XmlText nameText = xDoc.CreateTextNode(logLevel);
                XmlText dataText = xDoc.CreateTextNode(date.ToShortDateString());
                XmlText messageText = xDoc.CreateTextNode(logString);
                XmlText moduleText = xDoc.CreateTextNode(module);

                levelAttr.AppendChild(nameText);
                dateElem.AppendChild(dataText);
                messageElem.AppendChild(messageText);
                moduleElem.AppendChild(moduleText);

                logElem.Attributes.Append(levelAttr);
                logElem.AppendChild(dateElem);
                logElem.AppendChild(messageElem);
                logElem.AppendChild(moduleElem);

                xRoot?.AppendChild(logElem);
                xDoc.Save(NameFile);

            }
            return true;
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected new void Dispose(bool disposing)
        {
                NameFile = null;
        }

        ~XmlFile()
        {
            Dispose(false);
        }

    }
}

