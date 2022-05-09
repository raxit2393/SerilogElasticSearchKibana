namespace Common.Logging
{
    public class LogSettings
    {
        public string LogType { get; set; }
        public LogFileConfiguration LogFileConfiguration { get; set; }
        public ElasticConfiguration ElasticConfiguration { get; set; }
    }

    public class ElasticConfiguration
    {
        public string Uri { get; set; }
    }
    
    public class LogFileConfiguration
    {
        public string LogFilePath { get; set; }
    }
}
