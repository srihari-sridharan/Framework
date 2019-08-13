using System.Diagnostics.CodeAnalysis;
using Framework.Interfaces.Adapters;
using Framework.Interfaces.DataAccess;
using Framework.Interfaces.DataSources;
using Framework.Interfaces.Modules;
using Framework.Interfaces.Services;

namespace Framework.Interfaces.Containers
{
    /// <summary>
    ///     Interface for application container.
    /// </summary>
    public interface IApplicationContainer
    {
        /// <summary>
        ///     Maintains state.
        ///     Adapters are true to the adapter patterns.
        ///     They build service wrappers around specialist resources in a framework agnostic fashion.
        ///     For instance a SharePoint Adapter will have services such as CreateFolder, UploadDoc,GetDoc
        ///     Examples: SharePoint Adapter, CryptoAdapter, FTPAdapter
        ///     For instance, GetAdapter(ILogger) will return the Logger service provider implementation.
        ///     This is a pure generic provider and has no ties back to the framework.
        ///     Providers shall not depend on any other library except the target resource
        ///     for which this serves as an adapter
        ///     NOTE: This is a prototype (and Not a singleton)
        /// </summary>
        /// <typeparam name="T">The contract of the provider for which implementation is required</typeparam>
        /// <returns>
        ///     The concrete class that implements the service
        /// </returns>
        T GetAdapter<T>() where T : IAdapter;

        /// <summary>
        ///     Gets the data access object.
        /// </summary>
        /// <typeparam name="T">Type of the DAO contract</typeparam>
        /// <returns>Data access object.</returns>
        T GetDataAccessObject<T>() where T : IDao;

        /// <summary>
        ///     Gets the data access object.
        /// </summary>
        /// <typeparam name="T">Type of the DAO contract</typeparam>
        /// <param name="registryName">Name of the registry.</param>
        /// <returns>Data access object.</returns>
        T GetDataAccessObject<T>(string registryName) where T : IDao;

        /// <summary>
        ///     This service returns a data source (repository) that will allow
        ///     data access operations to be performed on the particular ds.
        ///     This is a thread local instance. Meaning each thread should share a single instance of a ds.
        ///     For instance, a simple ADO.Net provider that provides a connection will be shared across the thread.
        /// </summary>
        /// <param name="name">The registered data source name.</param>
        /// <returns>Data source.</returns>
        IDataSource GetDataSource(string name);

        /// <summary>
        ///     Gets the type of the instance of.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>Instance of the implementation.</returns>
        TContract GetInstanceOfType<TContract>(string name);

        /// <summary>
        ///     Stateless
        ///     Library is a module that simplifies the interfaces for
        ///     consumption of specialist resources. This will leverage other framework libraries and features
        ///     and most importantly leverage adapters to provide the service.
        ///     For instance, A IDocumentsLibrary will provide the services such as CreateFolder, GetDocument, GetHistory etc.,
        ///     The implementations of this contracts can be multiple. One for SharePoint, Another for fileSystem, Document etc.,
        ///     Each implementation can leverage appropriate adapters for extending these services.
        ///     An appropriate implementation will be wired using the container.
        ///     Libraries will further
        ///     simplify access by loading standard settings for the SharePoint library such as URI, UID, PWD, Port # etc.,
        ///     that will be passed on to the adapter. This will have logic to handle failure, specialist exceptions, error codes,
        ///     retry log etc., This will be a singleton and stateless
        /// </summary>
        /// <typeparam name="T">Type of the contract</typeparam>
        /// <returns>
        ///     Returns the implementation of the library.
        /// </returns>
        T GetLibrary<T>() where T : ILibrary;

        /// <summary>
        ///     Gets the library.
        /// </summary>
        /// <typeparam name="T">Type of the contract</typeparam>
        /// <param name="registryName">Registered name.</param>
        /// <returns>Instance of the library.</returns>
        T GetLibrary<T>(string registryName) where T : ILibrary;

        /// <summary>
        ///     Provides a single entry system for retrieving translation text.
        ///     The implementation shall automatically take into account current user context and language preference
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The resource</returns>
        /// <exception cref="System.NotImplementedException">Not Implemented Exception</exception>
        string GetResource(string key);

        /// <summary>
        ///     Gets the resource.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="registryName">Name of the registry.</param>
        /// <returns>The resource</returns>
        /// <exception cref="System.NotImplementedException">Not Implemented Exception</exception>
        string GetResource(string key, string registryName);

        /// <summary>
        ///     Stateless
        ///     This provides the contract for any business service to implement. This is just a placeholder contract to provide
        ///     decoration services for injecting aspects such as Transaction management, authorization etc.,
        /// </summary>
        /// <typeparam name="T">Type of the contract</typeparam>
        /// <returns>Instance of an implementation.</returns>
        T GetService<T>() where T : IService;

        /// <summary>
        ///     Gets the service.
        /// </summary>
        /// <typeparam name="T">Type of the contract</typeparam>
        /// <param name="registryName">Name of the registry.</param>
        /// <returns>Instance of implementation.</returns>
        T GetService<T>(string registryName) where T : IService;

        /// <summary>
        ///     Registers the adapter.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification =
            "Framework design.")]
        void RegisterAdapter<TContract, TImplementation>()
            where TContract : IAdapter
            where TImplementation : class, TContract;

        /// <summary>
        ///     Registers the DAO.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="optionalRegistryName">Name of the optional registry.</param>
        /// <param name="enableCaching">if set to <c>true</c> [enable caching].</param>
        /// <param name="cacheableMethodNames">The cacheable method names.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification =
            "Framework design.")]
        void RegisterDao<TContract, TImplementation>(string optionalRegistryName, bool enableCaching,
            params string[] cacheableMethodNames)
            where TContract : IDao
            where TImplementation : class, TContract;

        /// <summary>
        ///     This is registered as a thread local instance. Each thread will be provided with a new instance.
        ///     The Transaction Provider is wired to the data source. In case of ado.net, the connection would be the
        ///     provider. The transaction provider would be invoked by the service-transaction interceptor to enlist all
        ///     transactions
        /// </summary>
        /// <param name="registryName">Name of the registry.</param>
        /// <param name="dataSource">The library.</param>
        void RegisterDataSource(string registryName, IDataSource dataSource);

        /// <summary>
        ///     Registers the library.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="optionalRegistryName">Name of the optional registry.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification =
            "Framework design.")]
        void RegisterLibrary<TContract, TImplementation>(string optionalRegistryName)
            where TContract : ILibrary
            where TImplementation : class, TContract;

        /// <summary>
        ///     Performs a named registration for a type.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="isSingleton">if set to <c>true</c> [is singleton].</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification =
            "Framework design.")]
        void RegisterNamedType<TContract, TImplementation>(string name, bool isSingleton)
            where TImplementation : class, TContract;

        /// <summary>
        ///     Registers the service.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <typeparam name="TDaoContract">The type of the DAO contract.</typeparam>
        /// <param name="optionalRegistryName">Name of the optional registry.</param>
        /// <param name="dataAccessObjectName">Name of the DAO.</param>
        /// <param name="methodsRequiringTransactions">The methods requiring transactions.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification =
            "Framework design.")]
        void RegisterService<TContract, TImplementation, TDaoContract>(string optionalRegistryName,
            string dataAccessObjectName, params string[] methodsRequiringTransactions)
            where TContract : IService
            where TImplementation : class, TContract
            where TDaoContract : IDao;
    }
}