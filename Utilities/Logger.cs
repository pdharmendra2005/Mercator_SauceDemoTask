using System;


namespace Mercator_SauceDemoTask.Utilities
{
    public static class Logger
    {
        private static readonly string LogFilePath = Path.Combine(Directory.GetCurrentDirectory(), "test_log.txt");

        public static void Log(string message)
        {
            string logMessage = $"{DateTime.Now}: {message}";
            File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
        }
    }
}
