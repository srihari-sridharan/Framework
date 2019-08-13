using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Framework.Base;
using Framework.Interfaces.Adapters;
using Framework.Interfaces.Containers;
using Framework.Interfaces.DataAccess;
using Framework.Interfaces.DataSources;
using Framework.Interfaces.Modules;
using Framework.Interfaces.Services;
using Framework.UnityContainer.Interceptors;
using Framework.Utils;
using Microsoft.Practices.Unity.Utility;
using Unity;
using Unity.Injection;
using Unity.Interception;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.InstanceInterceptors.InterfaceInterception;
using Unity.Lifetime;
using Unity.RegistrationByConvention;
using Constants = Framework.Base.Constants;

namespace Framework.UnityContainer.Wrappers
{
    /// <summary>
    ///     Wraps unity container to provide specialized behavior.
    /// </summary>
    public class UnityContainerWrapper : Unity.UnityContainer, IApplicationContainer
    {
        /// <summary>
        ///     The empty type array.
        /// </summary>
        private static readonly Type[] EmptyTypes = new Type[0];

        /// <summary>
        ///     The lock object
        /// </summary>
        private static readonly object lockObject = new object();

        /// <summary>
        ///     The named data sources
        /// </summary>
        private static Dictionary<string, IDataSource> namedDataSources;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UnityContainerWrapper" /> class.
        /// </summary>
        public UnityContainerWrapper()
        {
            namedDataSources = new Dictionary<string, IDataSource>();
            this.AddNewExtension<Interception>();
            Bootstrap();
        }

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
        public T GetAdapter<T>() where T : IAdapter
        {
            return GetInstanceOfType<T>(Constants.DefaultContainerName);
        }

        /// <summary>
        ///     Gets the data access object.
        /// </summary>
        /// <typeparam name="T">Type of the DAO contract</typeparam>
        /// <returns>Data access object.</returns>
        public T GetDataAccessObject<T>() where T : IDao
        {
            return GetInstanceOfType<T>(Constants.DefaultContainerName);
        }

        /// <summary>
        ///     Gets the data access object.
        /// </summary>
        /// <typeparam name="T">Type of the DAO contract</typeparam>
        /// <param name="registryName">Name of the registry.</param>
        /// <returns>Data access object.</returns>
        public T GetDataAccessObject<T>(string registryName) where T : IDao
        {
            return GetInstanceOfType<T>(registryName);
        }

        /// <summary>
        ///     This service returns a data source (repository) that will allow
        ///     data access operations to be performed on the particular ds.
        ///     This is a thread local instance. Meaning each thread should share a single instance of a ds.
        ///     For instance, a simple ADO.Net provider that provides a connection will be shared across the thread.
        /// </summary>
        /// <param name="name">The registered data source name.</param>
        /// <returns>Data source.</returns>
        public IDataSource GetDataSource(string name)
        {
            return namedDataSources.ContainsKey(name)
                ? namedDataSources[name]
                : null;
        }

        /// <summary>
        ///     Gets the type of the instance of.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>Instance of the implementation.</returns>
        public TContract GetInstanceOfType<TContract>(string name)
        {
            return this.Resolve<TContract>(name);
        }

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
        public T GetLibrary<T>() where T : ILibrary
        {
            return GetInstanceOfType<T>(Constants.DefaultContainerName);
        }

        /// <summary>
        ///     Gets the library.
        /// </summary>
        /// <typeparam name="T">Type of the contract</typeparam>
        /// <param name="registryName">Registered name.</param>
        /// <returns>Instance of the library.</returns>
        public T GetLibrary<T>(string registryName) where T : ILibrary
        {
            return GetInstanceOfType<T>(registryName);
        }

        /// <summary>
        ///     Provides a single entry system for retrieving translation text.
        ///     The implementation shall automatically take into account current user context and language preference
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The resource</returns>
        /// <exception cref="System.NotImplementedException">Not Implemented Exception</exception>
        public string GetResource(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the resource.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="registryName">Name of the registry.</param>
        /// <returns>The resource</returns>
        /// <exception cref="System.NotImplementedException">Not Implemented Exception</exception>
        public string GetResource(string key, string registryName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Stateless
        ///     This provides the contract for any business service to implement. This is just a placeholder contract to provide
        ///     decoration services for injecting aspects such as Transaction management, authorization etc.,
        /// </summary>
        /// <typeparam name="T">Type of the contract</typeparam>
        /// <returns>Instance of an implementation.</returns>
        public T GetService<T>() where T : IService
        {
            return GetInstanceOfType<T>(Constants.DefaultContainerName);
        }

        /// <summary>
        ///     Gets the service.
        /// </summary>
        /// <typeparam name="T">Type of the contract</typeparam>
        /// <param name="registryName">Name of the registry.</param>
        /// <returns>Instance of implementation.</returns>
        public T GetService<T>(string registryName) where T : IService
        {
            return GetInstanceOfType<T>(registryName);
        }

        /// <summary>
        ///     Registers the adapter.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        public void RegisterAdapter<TContract, TImplementation>()
            where TContract : IAdapter
            where TImplementation : class, TContract
        {
            // Temporary. to change when interceptors are to be wired in
            RegisterNamedType<TContract, TImplementation>(Constants.DefaultContainerName, true);
        }

        /// <summary>
        ///     Registers the DAO.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="optionalRegistryName">Name of the optional registry.</param>
        /// <param name="enableCaching">if set to <c>true</c> [enable caching].</param>
        /// <param name="cacheableMethodNames">The cacheable method names.</param>
        public void RegisterDao<TContract, TImplementation>(
            string optionalRegistryName,
            bool enableCaching,
            params string[] cacheableMethodNames)
            where TContract : IDao
            where TImplementation : class, TContract
        {
            var lifetime = GetManager(true);
            this.RegisterType<TContract, TImplementation>(string.IsNullOrEmpty(optionalRegistryName)
                    ? Constants.DefaultContainerName
                    : optionalRegistryName,
                lifetime,
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<DataAccessInterceptor>());
        }

        /// <summary>
        ///     This is registered as a thread local instance. Each thread will be provided with a new instance.
        ///     The Transaction Provider is wired to the data source. In case of ado.net, the connection would be the
        ///     provider. The transaction provider would be invoked by the service-transaction interceptor to enlist all
        ///     transactions
        /// </summary>
        /// <param name="registryName">Name of the registry.</param>
        /// <param name="dataSource">The library.</param>
        /// <exception cref="System.Exception">The Exception</exception>
        public void RegisterDataSource(string registryName, IDataSource dataSource)
        {
            if (string.IsNullOrWhiteSpace(registryName)) throw new BaseException(Constants.DataSourceNameRequired);

            if (null == dataSource) throw new BaseException(Constants.DataSourceLibraryRequired);

            if (!namedDataSources.ContainsKey(registryName))
                lock (lockObject)
                {
                    if (!namedDataSources.ContainsKey(registryName)) namedDataSources.Add(registryName, dataSource);
                }
        }

        /// <summary>
        ///     Registers the library.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="optionalRegistryName">Name of the optional registry.</param>
        public void RegisterLibrary<TContract, TImplementation>(string optionalRegistryName)
            where TContract : ILibrary
            where TImplementation : class, TContract
        {
            // Temporary. To change when interceptors are to be wired in.
            RegisterNamedType<TContract, TImplementation>(
                string.IsNullOrEmpty(optionalRegistryName)
                    ? Constants.DefaultContainerName
                    : optionalRegistryName,
                true);
        }

        /// <summary>
        ///     Performs a named registration for a type.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="isSingleton">if set to <c>true</c> [is singleton].</param>
        public void RegisterNamedType<TContract, TImplementation>(string name, bool isSingleton)
            where TImplementation : class, TContract
        {
            var lifetime = GetManager(isSingleton);
            this.RegisterType<TContract, TImplementation>(name, lifetime);
        }

        /// <summary>
        ///     Registers the service.
        /// </summary>
        /// <typeparam name="TContract">The type of the contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <typeparam name="TDaoContract">The type of the DAO contract.</typeparam>
        /// <param name="optionalRegistryName">Name of the optional registry.</param>
        /// <param name="dataAccessObjectName">Name of the DAO.</param>
        /// <param name="methodsRequiringTransactions">The methods requiring transactions.</param>
        public void RegisterService<TContract, TImplementation, TDaoContract>(
            string optionalRegistryName,
            string dataAccessObjectName,
            params string[] methodsRequiringTransactions)
            where TContract : IService
            where TImplementation : class, TContract
            where TDaoContract : IDao
        {
            var lifetime = GetManager(true);
            this.RegisterType<TContract, TImplementation>(
                optionalRegistryName,
                lifetime,
                new InjectionProperty("ConcreteDataAccessObject",
                    GetDataAccessObject<TDaoContract>(dataAccessObjectName)),
                new InjectionProperty("RequiresTransaction", methodsRequiringTransactions.ToList()),
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<ServiceInterceptor>());
        }

        /// <summary>
        ///     Checks for service or DAO interface.
        /// </summary>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns>List of interfaces.</returns>
        private static IEnumerable<Type> CheckForServiceOrDaoInterface(Type implementationType)
        {
            Guard.ArgumentNotNull(implementationType, "implementationType");
            var interfaces = GetImplementedInterfacesToMap(implementationType).ToList();
            var ignoreCase = StringComparison.InvariantCultureIgnoreCase;

            // Check if this is required.
            interfaces.RemoveAll(x => x.FullName.StartsWith("system", ignoreCase)
                                      || x.FullName.StartsWith("log4net", ignoreCase));

            Type @interface = null;
            if (interfaces.Exists(x => x.Name.Equals(typeof(IService).Name)
                                       || x.Name.Equals(typeof(IDao).Name)))
                @interface = interfaces.FirstOrDefault(y => y.Name.Contains(implementationType.Name));
            else
                @interface = null;

            return @interface != null ? new[] { @interface } : EmptyTypes;
        }

        /// <summary>
        ///     Filters the matching generic interfaces.
        /// </summary>
        /// <param name="typeInfo">The type information.</param>
        /// <returns>List of interfaces.</returns>
        private static IEnumerable<Type> FilterMatchingGenericInterfaces(TypeInfo typeInfo)
        {
            var parameters = typeInfo.GenericTypeParameters;
            foreach (var @interface in typeInfo.ImplementedInterfaces)
            {
                var interfaceTypeInfo = @interface.GetTypeInfo();
                if (!(interfaceTypeInfo.IsGenericType && interfaceTypeInfo.ContainsGenericParameters)) continue;

                if (GenericParametersMatch(parameters, interfaceTypeInfo.GenericTypeArguments))
                    yield return interfaceTypeInfo.GetGenericTypeDefinition();
            }
        }

        /// <summary>
        ///     Matches the parameters with the interface arguments.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="interfaceArguments">The interface arguments.</param>
        /// <returns>True upon match.</returns>
        private static bool GenericParametersMatch(Type[] parameters, Type[] interfaceArguments)
        {
            if (parameters.Length != interfaceArguments.Length) return false;

            for (var i = 0; i < parameters.Length; i++)
                if (parameters[i] != interfaceArguments[i])
                    return false;

            return true;
        }

        /// <summary>
        ///     Gets the default list of ignored assemblies.
        /// </summary>
        /// <returns>List of assembly names.</returns>
        private static List<string> GetDefaultIngoredAssemblies()
        {
            return new List<string>
            {
                "System", "Microsoft", "Enterprise", "Mongo", "log4net", "Newtonsoft", "ServiceStack"
            };
        }

        /// <summary>
        ///     Gets the implemented interfaces to map.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>List of interface types</returns>
        private static IEnumerable<Type> GetImplementedInterfacesToMap(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            if (!typeInfo.IsGenericType)
                return typeInfo.ImplementedInterfaces;
            if (!typeInfo.IsGenericTypeDefinition)
                return typeInfo.ImplementedInterfaces;
            return FilterMatchingGenericInterfaces(typeInfo);
        }

        /// <summary>
        ///     Gets the types for registration.
        /// </summary>
        /// <returns>List of types.</returns>
        private static IEnumerable<Type> GetTypesForRegistration()
        {
            var allTypes = UnityUtils.FromAssembliesInBasePath(false).ToList();

            // Retrieve assemblies to ignore from AppConfig. If not exists, use a default setting.
            var setting = ConfigurationManager.AppSettings["IgnoreAssemblies"];
            var ignoreAssemblies = setting.IsNullOrWhiteSpace()
                ? GetDefaultIngoredAssemblies()
                : setting.Split(',').ToList();
            var removables = new List<Type>();

            // Find out all unwanted assemblies matching the starting names read from
            // settings file with the assembly name.
            allTypes.ForEach(y =>
            {
                if (ignoreAssemblies.Any(x => y.FullName.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
                    removables.Add(y);
            });
            allTypes.RemoveAll(x => removables.Contains(x));
            return allTypes;
        }

        /// <summary>
        ///     Bootstraps this instance.
        /// </summary>
        private void Bootstrap()
        {
            var types = GetTypesForRegistration();
            RegisterControllers(types);
            RegisterServicesAndDaos(types);
        }

        /// <summary>
        ///     Gets the injection members.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>List of injection members.</returns>
        private IEnumerable<InjectionMember> GetInjectionMembers(Type type)
        {
            if (type.GetInterface(typeof(IService).FullName, true) != null)
                return new List<InjectionMember>
                {
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<ServiceInterceptor>(),
                    new InjectionProperty("RequiresTransaction", new List<string> {"*"})
                };
            if (type.GetInterface(typeof(IDao).FullName, true) != null)
                return new List<InjectionMember>
                {
                    new Interceptor<InterfaceInterceptor>(),
                    new InterceptionBehavior<DataAccessInterceptor>()
                };

            return new List<InjectionMember>();
        }

        /// <summary>
        ///     Gets the life-time manager.
        /// </summary>
        /// <param name="isSingleton">if set to <c>true</c> [is singleton].</param>
        /// <returns>Life time manager.</returns>
        private ITypeLifetimeManager GetManager(bool isSingleton)
        {
            return isSingleton
                ? new ContainerControlledLifetimeManager()
                : new PerResolveLifetimeManager() as ITypeLifetimeManager;
        }

        /// <summary>
        ///     Registers the controllers.
        /// </summary>
        /// <param name="types">The types.</param>
        private void RegisterControllers(IEnumerable<Type> types)
        {
            IEnumerable<Type> controllers
                = types.Where(type => null != type.BaseType
                                      && type.BaseType.Name.Equals("apicontroller", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            this.RegisterTypes(controllers, null, WithName.Default, WithLifetime.PerResolve, null, true);
        }

        /// <summary>
        ///     Registers the services and data access objects.
        /// </summary>
        /// <param name="types">The types.</param>
        private void RegisterServicesAndDaos(IEnumerable<Type> types)
        {
            IEnumerable<Type> servicesAndDaos
                = types.Where(type => null != type.BaseType
                                      && type.GetTypeInfo().ImplementedInterfaces.Any(
                                          @interface => @interface.Name.Equals(typeof(IDao).Name)
                                                        || @interface.Name.Equals(typeof(IService).Name))).ToList();

            this.RegisterTypes(
                servicesAndDaos,
                CheckForServiceOrDaoInterface,
                WithName.Default,
                WithLifetime.ContainerControlled,
                GetInjectionMembers,
                true);
        }
    }
}