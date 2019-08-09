using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Framework.Interfaces.Context;

namespace Framework.Base.Context
{
    /// <summary>
    ///     Represents application context, maintains the service context, user context
    ///     and other named properties to be used during execution.
    /// </summary>
    public class ApplicationContext : IApplicationContext
    {
        /// <summary>
        ///     Thread local cache. See Properties for more information.
        /// </summary>
        private Dictionary<string, object> properties;

        /// <summary>
        ///     This refers to the current user context.
        /// </summary>
        private IUserContext userContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApplicationContext" /> class.
        /// </summary>
        public ApplicationContext()
        {
            Init();
        }

        /// <summary>
        ///     Gets a value indicating whether the Application Context is initialized.
        ///     The context object is seldom null. but the user and properties
        ///     need to be initialized at the beginning of each service request
        ///     failing which the ApplicationContext Object may exist but would be null.
        ///     This property helps check that.
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        ///     Gets the named properties. This is a thread local cache. This shall exist only
        ///     for the duration of the thread.
        ///     This can be used to store any thread local information just like a HttpContext store.
        ///     Being a thread local cache, this is only visible to the current thread.
        /// </summary>
        public Dictionary<string, object> Properties
        {
            get => Initialized ? properties : null;
            private set => properties = value;
        }

        /// <summary>
        ///     Gets the service context. Need to check if this is required at all.
        /// </summary>
        /// <value>
        ///     The service context.
        /// </value>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification =
            "Forward facing requirement.")]
        public IServiceContext ServiceContext
        {
            get =>
                Properties.ContainsKey(Constants.ServiceContextKey)
                    ? Properties[Constants.ServiceContextKey] as IServiceContext
                    : null;

            private set => Properties[Constants.ServiceContextKey] = value;
        }

        /// <summary>
        ///     Gets the current user context.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification =
            "Forward facing requirement.")]
        public IUserContext UserContext
        {
            get => Initialized ? userContext : null;
            private set => userContext = value;
        }

        /// <summary>
        ///     This should be invoked at the end of a thread's service life cycle. For example at the end of a request/response
        ///     cycle
        ///     This would cleanup the thread state and any other operations
        /// </summary>
        public void Flush()
        {
            Properties.Clear();
            UserContext = null;
            Initialized = false;
        }

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
        /// <returns>
        ///     true if initialization is successful. False is not or session has expired.
        /// </returns>
        public bool Init()
        {
            // 1. Check if Properties is null and initialize - Done.
            // 2. Check if user info is available and load it - Todo.
            // 3. Set user info to _UserContext - Todo.
            // 4. Check and load user preferences etc. - Todo.
            // 5. Finally set Initialized to true. in catch set to false - Done.
            try
            {
                if (null == Properties) Properties = new Dictionary<string, object>();

                // This is temporary. Load user info here.
                userContext = Application.Container.GetInstanceOfType<IUserContext>(Constants.Context);
                Initialized = true;

                // This is temporary code for testing purpose.
                ServiceContext = Application.Container.GetInstanceOfType<IServiceContext>(Constants.Context);
                ServiceContext.TransactionContext
                    = Application.Container.GetInstanceOfType<ITransactionContext>(Constants.Context);

                return Initialized;
            }
            catch
            {
                Initialized = false;
                throw;
            }
        }
    }
}