using Serilog;
using System;

namespace Common.Logging
{
    /// <summary>
    /// Common logging class
    /// </summary>
    public static class LogWriter
    {
        public static void LogError(Exception ex)
        {
            Log.Error(ex, ex.Message);
        }

        public static void LogInformation(string message)
        {
            Log.Information(message);
        }

        public static void LogDebug(string message)
        {
            Log.Debug(message);
        }
        
        public static void LogFatal(string message)
        {
            Log.Fatal(message);
        }
    }
}
