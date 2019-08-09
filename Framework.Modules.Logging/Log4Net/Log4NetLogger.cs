// <copyright file="Log4NetLogger.cs" company="">
//
// </copyright>

using System;
using System.Collections.Generic;
using Framework.Interfaces.Modules;
using log4net;
using log4net.Config;

namespace Framework.Modules.Logging.Log4Net
{
    /// <summary>
    ///     Log4Net logging implementation. To be replaced with chosen logging framework.
    ///     This is a sample implementation.
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        /// <summary>
        ///     The method parameter
        /// </summary>
        private const string MethodParam = "RunningMethod";

        /// <summary>
        ///     The cache key
        /// </summary>
        private static readonly object cacheKey = new object();

        /// <summary>
        ///     Value indicating whether logger is initialized.
        /// </summary>
        private static readonly bool initialized = false;

        /// <summary>
        ///     The lock object
        /// </summary>
        private static readonly object lockObject = new object();

        /// <summary>
        ///     Gets or sets the cached loggers.
        /// </summary>
        /// <value>
        ///     The cached loggers.
        /// </value>
        private static Dictionary<string, ILog> CachedLoggers { get; set; }

        /// <summary>
        ///     Synchronizes the local log to a centralized service.
        ///     This is optional.
        /// </summary>
        /// <returns>True on success.</returns>
        /// <exception cref="System.NotImplementedException">Not implemented exception.</exception>
        public bool Flush()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Logs debugs message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        public void LogDebug(string message, string type, string methodName)
        {
            var logger = GetLogger(type);
            ThreadContext.Properties[MethodParam] = methodName;
            if (logger.IsDebugEnabled) logger.Debug(message);
        }

        /// <summary>
        ///     Logs errors message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="ex">The ex.</param>
        public void LogError(string message, string type, string methodName, Exception ex)
        {
            // TODO: iterate exception, data args and dump it into thelog
            var logger = GetLogger(type);
            ThreadContext.Properties[MethodParam] = methodName;
            if (logger.IsErrorEnabled) logger.Error(message, ex);
        }

        /// <summary>
        ///     Logs fatal message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        public void LogFatal(string message, string type, string methodName)
        {
            var logger = GetLogger(type);
            ThreadContext.Properties[MethodParam] = methodName;
            if (logger.IsFatalEnabled) logger.Fatal(message);
        }

        /// <summary>
        ///     Logs information message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        public void LogInfo(string message, string type, string methodName)
        {
            var logger = GetLogger(type);
            ThreadContext.Properties[MethodParam] = methodName;
            if (logger.IsInfoEnabled) logger.Info(message);
        }

        /// <summary>
        ///     Logs trace message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <exception cref="System.NotImplementedException">Not Implemented Exception</exception>
        public void LogTrace(string message, string type, string methodName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Bootstrap log4net.
        /// </summary>
        private static void BootStrapLog4net()
        {
            lock (lockObject)
            {
                if (!initialized)
                {
                    XmlConfigurator.Configure();
                    CachedLoggers = new Dictionary<string, ILog>();
                }
            }
        }

        /// <summary>
        ///     Gets the logger instance for the type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Logger instance for the type.</returns>
        private ILog GetLogger(string type)
        {
            if (!initialized) BootStrapLog4net();

            if (!CachedLoggers.ContainsKey(type)) InitializeAndCacheLogger(type);

            return CachedLoggers[type];
        }

        /// <summary>
        ///     Initialize and caches the logger.
        /// </summary>
        /// <param name="type">The type.</param>
        private void InitializeAndCacheLogger(string type)
        {
            lock (cacheKey)
            {
                if (!CachedLoggers.ContainsKey(type)) CachedLoggers.Add(type, LogManager.GetLogger(type));
            }
        }
    }
}