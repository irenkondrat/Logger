namespace logger.Class
{
    class LoggerFileFactory
    {
         internal FileBase GetLoggerFileFactory( string formatFile, string nameFile)
        {
            switch (formatFile)
            {
                case "xml":
                    return new XmlFile($"{nameFile}.xml");
                case "txt":
                    return new PlainText($"{nameFile}.txt");
                case "doc":
                    return new PlainText($"{nameFile}.txt");
                case "json":
                    return new JsonFile($"{nameFile}.json");
                default:
                    return new PlainText($"{nameFile}.txt");

            }
        }
    }
}
