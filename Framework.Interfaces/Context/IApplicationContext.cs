using System.Collections.Generic;

namespace Framework.Interfaces.Context
{
    /// <summary>
    ///     Represents application context, maintains the service context, user context
    ///     and other named properties to be used during execution.
    /// </summary>
    public interface IApplicationContext
    {
        /// <summary>
        ///     Gets a value indicating whether the Application Context is initialized.
        ///     The context object is seldom null. but the user and properties
        ///     need to be initialized at the beginning of each service request
        ///     failing which the ApplicationContext Object may exist but would be null.
        ///     This property helps check that.
        /// </summary>
        bool Initialized { get; }

        /// <summary>
        ///     Gets the named properties. This is a thread local cache. This shall exist only
        ///     for the duration of the thread.
        ///     This can be used to store any thread local information just like a HttpContext store.
        ///     Being a thread local cache, this is only visible to the current thread.
        /// </summary>
        Dictionary<string, object> Properties { get; }

        /// <summary>
        ///     Gets the service context. Need to check if this is required at all.
        /// </summary>
        /// <value>
        ///     The service context.
        /// </value>
        IServiceContext ServiceContext { get; }

        /// <summary>
        ///     Gets the current user context.
        /// </summary>
        IUserContext UserContext { get; }

        /// <summary>
        ///     This should be invoked at the end of a thread's service life cycle. For example at the end of a request/response
        ///     cycle
        ///     This would cleanup the thread state and any other operations
        /// </summary>
        void Flush();

        /// <summary>
        ///     This should be invoked at the beginning of every thread.
        ///     If a request is the beginning of the thread, this should be invoked before any request is serviced.
        ///     Typically using an Http Intercepting Filter pattern.
        ///     This also needs to be initialized for every background thread initiated in parallel processes.
        ///     This will also initialize the properties
        ///     This will typically initialize the IUserContext from some session store.
        ///     This should also initialize the thread level cache Properties
        ///     NOTE: is it possible to automatically tag this to the beginning of each background thread?
        /// </summary>
        /// <returns>true if initialization is successful. False is not or session has expired.</returns>
        bool Init();
    }
}