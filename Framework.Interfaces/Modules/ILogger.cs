using System;

namespace Framework.Interfaces.Modules
{
    /// <summary>
    ///     Interface for logger. Logger is a library.
    /// </summary>
    public interface ILogger : ILibrary
    {
        /// <summary>
        ///     Synchronizes the local log to a centralized service.
        ///     This is optional
        /// </summary>
        /// <returns>True on success.</returns>
        bool Flush();

        /// <summary>
        ///     Logs debugs message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        void LogDebug(string message, string type, string methodName);

        /// <summary>
        ///     Logs errors message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="ex">The ex.</param>
        void LogError(string message, string type, string methodName, Exception ex);

        /// <summary>
        ///     Logs fatal message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        void LogFatal(string message, string type, string methodName);

        /// <summary>
        ///     Logs information message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        void LogInfo(string message, string type, string methodName);

        /// <summary>
        ///     Logs trace message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        void LogTrace(string message, string type, string methodName);
    }
}