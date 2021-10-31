using MyColor.Infra.Logging.Interfaces;
using NLog;
using System;
using System.IO;

namespace MyColor.Infra.Logging.Services
{
    public class LoggerService : ILoggerService
    {
        private static ILogger _logger;
        private static readonly string _parentDirectory = Directory.GetParent(Environment.CurrentDirectory).ToString();
        private const string PATH = "/MyColor.Infra.Logging/";
        private const string FILE_NAME = "NLog.config";

        public LoggerService()
        {
            string pathToNLogConfig = $"{_parentDirectory}{PATH}{FILE_NAME}";
            LogManager.LoadConfiguration(pathToNLogConfig);
            _logger = LogManager.GetCurrentClassLogger();
        }
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }
    }
}
