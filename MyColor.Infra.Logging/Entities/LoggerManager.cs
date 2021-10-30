using System;
using System.IO;
using MyColor.Infra.Logging.Interfaces;
using NLog;

namespace MyColor.Infra.Logging.Entities
{
    public sealed class LoggerManager : ILoggerManager
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private const string PATH = "/MyColor.Infra.Logging/";
        private static readonly string _parentDirectory = Directory.GetParent(Environment.CurrentDirectory).ToString();
        private const string FILE_NAME = "NLog.config";

        public LoggerManager()
        {
            //LogManager.LoadConfiguration(Path.Combine(_parentDirectory, $"{PATH}{FILE_NAME}"));
            LogManager.LoadConfiguration(Path.Combine(Environment.CurrentDirectory, $"{FILE_NAME}"));
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
