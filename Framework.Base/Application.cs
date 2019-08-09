// <copyright file="Application.cs" company="">
//
// </copyright>

using System;
using System.Threading;
using Framework.Base.Context;
using Framework.Interfaces.Containers;
using Framework.Interfaces.Context;
using Framework.Interfaces.Modules;
using Framework.Modules.Logging.Log4Net;

namespace Framework.Base
{
    /// <summary>
    ///     This is the abstract base class representing an application.
    ///     This encapsulates the application context, application container.
    /// </summary>
    public abstract class Application
    {
        /// <summary>
        ///     This creates one static IApplicationContext instance per thread.
        ///     This will be alive till it is disposed. Once disposed it is no longer available.
        ///     Instead of disposing the usage should be inclined towards calling the Start()
        ///     and Stop() to ensure that the static entity is maintained but is cleared after use.
        ///     This instance is visible only to the current thread.
        ///     The object should be visible only to the current thread.
        ///     Each thread will make sure that it initializes and flushes at the beginning and end
        ///     of each thread. Remember! The instance is same, only the content will be initialized and flushed.
        /// </summary>
        private static readonly ThreadLocal<IApplicationContext> context
            = new ThreadLocal<IApplicationContext>(() =>
                Container.GetInstanceOfType<IApplicationContext>(Constants.Context));

        /// <summary>
        ///     The lock object
        /// </summary>
        private static readonly object lockObject = new object();

        /// <summary>
        ///     The application container.
        /// </summary>
        private static IApplicationContainer container;

        /// <summary>
        ///     Gets the application specific container.
        /// </summary>
        /// <value>
        ///     The container.
        /// </value>
        public static IApplicationContainer Container => container;

        /// <summary>
        ///     Gets the application context.
        /// </summary>
        /// <value>
        ///     The context.
        /// </value>
        public static IApplicationContext Context => context.Value;

        /// <summary>
        ///     Gets or sets the current application context.
        /// </summary>
        /// <value>
        ///     The current application.
        /// </value>
        public static Application Current { get; set; }

        /// <summary>
        ///     Gets or sets the fully qualified container implementation.
        /// </summary>
        /// <value>
        ///     The fully qualified container implementation.
        /// </value>
        public string FullyQualifiedContainer { get; set; }

        /// <summary>
        ///     Gets or sets the name of the specific derived Application class.
        /// </summary>
        /// <value>
        ///     The application name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Aborts this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Not implemented exception.</exception>
        public static void Abort()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <returns>The name.</returns>
        /// <exception cref="System.NotImplementedException">Not implemented exception.</exception>
        public static string GetName()
        {
            // Read entry from Configuration and Save it in name
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Stops this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Not implemented exception.</exception>
        public static void Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public void Start()
        {
            // Initialize Container
            if (container == null)
                lock (lockObject)
                {
                    if (container == null) Current = this;
                }

            // Cache current directory.
            var currentDirectory = Environment.CurrentDirectory;

            // Set current directory to enable assembly loading.
            Environment.CurrentDirectory = AppDomain.CurrentDomain.RelativeSearchPath
                                           ?? AppDomain.CurrentDomain.BaseDirectory;

            container = Activator.CreateInstanceFrom(
                    FullyQualifiedContainer.Split(',')[0],
                    FullyQualifiedContainer.Split(',')[1]).Unwrap()
                as IApplicationContainer;

            // Reset current directory.
            Environment.CurrentDirectory = currentDirectory;

            RegisterDefaults();
            OnStart();

            // Can do any other bootstrapping activity here.
        }

        /// <summary>
        ///     Run a battery of tests to see if all necessary
        ///     Configuration settings are properly configured
        ///     for the framework to bootstrap
        /// </summary>
        /// <returns>True if configuration settings are properly configured.</returns>
        protected static bool CheckConfiguration()
        {
            return true;
        }

        /// <summary>
        ///     This is the method that shall be overwritten by the respective Application Implementations
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        ///     Registers the DI defaults for named types and libraries.
        /// </summary>
        private void RegisterDefaults()
        {
            container.RegisterNamedType<IApplicationContext, ApplicationContext>(Constants.Context, false);
            container.RegisterNamedType<IUserContext, UserContext>(Constants.Context, false);
            container.RegisterNamedType<IServiceContext, ServiceContext>(Constants.Context, false);
            container.RegisterNamedType<ITransactionContext, TransactionContext>(Constants.Context, false);
            container.RegisterLibrary<ILogger, Log4NetLogger>(Constants.DefaultContainerName);
        }
    }
}