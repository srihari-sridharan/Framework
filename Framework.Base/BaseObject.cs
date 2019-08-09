// <copyright file="BaseObject.cs" company="">
//
// </copyright>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Framework.Interfaces.Modules;

namespace Framework.Base
{
    /// <summary>
    ///     Represents the base object. All other objects derive from the base object.
    ///     Encapsulates the common functionality for all objects.
    /// </summary>
    public abstract class BaseObject
    {
        /// <summary>
        ///     Gets the Logger implementation for the current class.
        ///     This will retrieve the current type and check if there is a named logger for the class
        ///     in the NamedLoggers cache. If not, will retrieve the logger, cache the handle and return an instance.
        /// </summary>
        protected ILogger Logger => Application.Container.GetLibrary<ILogger>();

        /// <summary>
        ///     Logs debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="callerMethod">The caller method.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "DRY.")]
        public void LogDebug(string message, [CallerMemberName] string callerMethod = "")
        {
            Logger.LogDebug(message, GetType().FullName, callerMethod);
        }

        /// <summary>
        ///     Logs error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="callerMethod">The caller method.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "DRY.")]
        public void LogError(string message, Exception ex, [CallerMemberName] string callerMethod = "")
        {
            Logger.LogError(message, GetType().FullName, callerMethod, ex);
        }

        /// <summary>
        ///     Logs fatal message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="callerMethod">The caller method.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "DRY.")]
        public void LogFatal(string message, [CallerMemberName] string callerMethod = "")
        {
            Logger.LogFatal(message, GetType().FullName, callerMethod);
        }

        /// <summary>
        ///     Logs information message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="callerMethod">The caller method.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "DRY.")]
        public void LogInfo(string message, [CallerMemberName] string callerMethod = "")
        {
            Logger.LogInfo(message, GetType().FullName, callerMethod);
        }

        /// <summary>
        ///     Logs trace message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="callerMethod">The caller method.</param>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "DRY.")]
        public void LogTrace(string message, [CallerMemberName] string callerMethod = "")
        {
            Logger.LogTrace(message, GetType().FullName, callerMethod);
        }
    }
}