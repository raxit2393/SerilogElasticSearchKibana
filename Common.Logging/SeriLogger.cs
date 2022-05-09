using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;
using System.Reflection;

namespace Common.Logging
{

    /*
     * https://github.com/shazforiot/Elasticsearch-kibana-docker-compose-single-node
     * 
     * 
     * 
     * https://www.elastic.co/guide/en/elasticsearch/reference/7.5/docker.html
     * https://www.elastic.co/guide/en/kibana/current/docker.html
     * 
     * 
     * 
     * http://localhost:9200/
     * 
     * docker network create elastic
     * docker pull docker.elastic.co/elasticsearch/elasticsearch:7.16.3
     * docker run --name es01-test --net elastic -p 127.0.0.1:9200:9200 -p 127.0.0.1:9300:9300 -e "discovery.type=single-node" docker.elastic.co/elasticsearch/elasticsearch:7.16.3
     * 
     * 
     * docker pull docker.elastic.co/kibana/kibana:7.16.3
     * docker run --name kib01-test --net elastic -p 127.0.0.1:5601:5601 -e "ELASTICSEARCH_HOSTS=http://es01-test:9200" docker.elastic.co/kibana/kibana:7.16.3
     * 
     * 
     * http://localhost:5601
     * 
     * 
     */
    public static class SeriLogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
           (context, loggerConfiguration) =>
           {
               var logSetting = context.Configuration.Get<LogSettings>();
               string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
               string logPath = string.Empty, logFilePath = string.Empty, elasticUri = string.Empty;

               loggerConfiguration
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Console()
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                    .ReadFrom.Configuration(context.Configuration);

               if ((logSetting.LogType ?? string.Empty).ToUpper().Contains("FILE"))
               {
                   //logPath = context.Configuration.GetValue<string>("LogPath:Path");
                   logPath = logSetting.LogFileConfiguration.LogFilePath;
                   logFilePath = Path.Combine(logPath, string.Format("LogData_{0}.log", DateTime.Today.ToString("yyyyMMdd")));

                   loggerConfiguration.WriteTo.File(path: logFilePath, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                                rollOnFileSizeLimit: true,
                                retainedFileCountLimit: 20,
                                rollingInterval: RollingInterval.Day,
                                fileSizeLimitBytes: 10000);
               }

               if ((logSetting.LogType ?? string.Empty).ToUpper().Contains("ELASTIC"))
               {
                   elasticUri = logSetting.ElasticConfiguration.Uri;

                   loggerConfiguration.WriteTo.Elasticsearch(
                           new ElasticsearchSinkOptions(new Uri(elasticUri))
                           {
                               IndexFormat = $"applogs-{context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                               AutoRegisterTemplate = true,
                               NumberOfShards = 2,
                               NumberOfReplicas = 1
                           });
               }
           };
    }
}
