using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using Unity.RegistrationByConvention;

namespace Framework.Utils
{
    /// <summary>
    ///     Utility functions used by Unity dependency resolution.
    /// </summary>
    public static class UnityUtils
    {
        /// <summary>
        ///     The product name
        /// </summary>
        private static readonly string NetFrameworkProductName = GetNetFrameworkProductName();

        /// <summary>
        ///     The unity product name
        /// </summary>
        private static readonly string UnityProductName = GetUnityProductName();

        /// <summary>
        ///     Returns the list of types from all assemblies present in base path.
        /// </summary>
        /// <param name="includeSystemAssemblies">if set to <c>true</c> [include system assemblies].</param>
        /// <param name="includeUnityAssemblies">if set to <c>true</c> [include unity assemblies].</param>
        /// <param name="skipOnError">if set to <c>true</c> [skip on error].</param>
        /// <returns>List of types</returns>
        [SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed", Justification = "Simplify API")]
        public static IEnumerable<Type> FromAssembliesInBasePath(bool includeSystemAssemblies = false,
            bool includeUnityAssemblies = false, bool skipOnError = true)
        {
            return FromCheckedAssemblies(
                GetAssembliesInBasePath(includeSystemAssemblies, includeUnityAssemblies, skipOnError), skipOnError);
        }

        /// <summary>
        ///     Checks the assemblies and filters the types required for DI.
        /// </summary>
        /// <param name="assemblies">The assemblies.</param>
        /// <param name="skipOnError">if set to <c>true</c> [skip on error].</param>
        /// <returns>List of types for DI.</returns>
        private static IEnumerable<Type> FromCheckedAssemblies(IEnumerable<Assembly> assemblies, bool skipOnError)
        {
            return assemblies
                .SelectMany(a =>
                {
                    IEnumerable<TypeInfo> types;

                    try
                    {
                        types = a.DefinedTypes;
                    }
                    catch (ReflectionTypeLoadException e)
                    {
                        if (!skipOnError) throw;

                        types = e.Types.TakeWhile(t => t != null).Select(t => t.GetTypeInfo());
                    }

                    return types.Where(ti => ti.IsClass & !ti.IsAbstract && !ti.IsValueType && ti.IsVisible)
                        .Select(ti => ti.AsType());
                });
        }

        /// <summary>
        ///     Gets the assemblies in base path.
        /// </summary>
        /// <param name="includeSystemAssemblies">if set to <c>true</c> [include system assemblies].</param>
        /// <param name="includeUnityAssemblies">if set to <c>true</c> [include unity assemblies].</param>
        /// <param name="skipOnError">if set to <c>true</c> [skip on error].</param>
        /// <returns>List of assemblies in base path.</returns>
        private static IEnumerable<Assembly> GetAssembliesInBasePath(bool includeSystemAssemblies,
            bool includeUnityAssemblies, bool skipOnError)
        {
            string basePath;

            try
            {
                basePath = AppDomain.CurrentDomain.RelativeSearchPath
                           ?? AppDomain.CurrentDomain.BaseDirectory;
            }
            catch (SecurityException)
            {
                if (!skipOnError) throw;

                return new Assembly[0];
            }

            return GetAssemblyNames(basePath, skipOnError)
                .Select(an => LoadAssembly(Path.GetFileNameWithoutExtension(an), skipOnError))
                .Where(a => a != null && (includeSystemAssemblies || !IsSystemAssembly(a)) &&
                            (includeUnityAssemblies || !IsUnityAssembly(a)));
        }

        /// <summary>
        ///     Gets the names of assemblies in the given path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="skipOnError">if set to <c>true</c> [skip on error].</param>
        /// <returns>List of assembly names.</returns>
        private static IEnumerable<string> GetAssemblyNames(string path, bool skipOnError)
        {
            try
            {
                return Directory.EnumerateFiles(path, "*.dll").Concat(Directory.EnumerateFiles(path, "*.exe"));
            }
            catch (Exception e)
            {
                if (!(skipOnError && (e is DirectoryNotFoundException || e is IOException || e is SecurityException ||
                                      e is UnauthorizedAccessException))) throw;

                return new string[0];
            }
        }

        /// <summary>
        ///     Gets the name of the framework product.
        /// </summary>
        /// <returns>Framework product name.</returns>
        private static string GetNetFrameworkProductName()
        {
            var productAttribute = typeof(object).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyProductAttribute>();
            return productAttribute != null ? productAttribute.Product : null;
        }

        /// <summary>
        ///     Gets the name of the unity product.
        /// </summary>
        /// <returns>Unity product name.</returns>
        private static string GetUnityProductName()
        {
            var productAttribute =
                typeof(AllClasses).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyProductAttribute>();
            return productAttribute != null ? productAttribute.Product : null;
        }

        /// <summary>
        ///     Determines whether [is system assembly] [the specified assembly].
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>True if it is a system assembly.</returns>
        private static bool IsSystemAssembly(Assembly assembly)
        {
            if (NetFrameworkProductName != null)
            {
                var productAttribute = assembly.GetCustomAttribute<AssemblyProductAttribute>();
                return productAttribute != null && string.Compare(NetFrameworkProductName, productAttribute.Product,
                           StringComparison.Ordinal) == 0;
            }

            return false;
        }

        /// <summary>
        ///     Determines whether [is unity assembly] [the specified assembly].
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>True if it is a unity assembly.</returns>
        private static bool IsUnityAssembly(Assembly assembly)
        {
            if (UnityProductName != null)
            {
                var productAttribute = assembly.GetCustomAttribute<AssemblyProductAttribute>();
                return productAttribute != null &&
                       string.Compare(UnityProductName, productAttribute.Product, StringComparison.Ordinal) == 0;
            }

            return false;
        }

        /// <summary>
        ///     Loads the assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="skipOnError">if set to <c>true</c> [skip on error].</param>
        /// <returns>The Assembly.</returns>
        private static Assembly LoadAssembly(string assemblyName, bool skipOnError)
        {
            try
            {
                return Assembly.Load(assemblyName);
            }
            catch (Exception e)
            {
                if (!(skipOnError &&
                      (e is FileNotFoundException || e is FileLoadException || e is BadImageFormatException))) throw;

                return null;
            }
        }
    }
}