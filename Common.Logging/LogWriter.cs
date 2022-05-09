using Serilog;
using System;

namespace Common.Logging
{
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

    }
}
