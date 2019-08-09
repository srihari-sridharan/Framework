// <copyright file="BaseService.cs" company="">
//
// </copyright>

using System.Collections.Generic;
using Framework.Interfaces.DataAccess;
using Framework.Interfaces.Services;

namespace Framework.Base.Services
{
    /// <summary>
    ///     Base class for all services.
    /// </summary>
    public abstract class BaseService : BaseObject, IService
    {
        /// <summary>
        ///     Gets or sets the concrete data access object.
        /// </summary>
        /// <value>
        ///     The concrete data access object.
        /// </value>
        public IDao ConcreteDataAccessObject { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating the name for the implementation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the requires transaction.
        /// </summary>
        /// <value>
        ///     The requires transaction.
        /// </value>
        public List<string> RequiresTransaction { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether exceptions should result in rollback
        ///     Applicable only  if RequiresTransaction is true
        ///     Defaults to true
        /// </summary>
        public bool RollbackOnError { get; set; }

        /// <summary>
        ///     Return the data access object.
        /// </summary>
        /// <typeparam name="T">Represents the Dao interface.</typeparam>
        /// <returns>Data access object</returns>
        protected T DataAccessObject<T>() where T : IDao
        {
            // If explicitly registered, ConcreteDataAccessObject is likely to have been DI'd.
            // But, when auto registrations run, we need a mechanism to JIT-DI the DAL implementation.
            // This block does just that.
            // Note: Unlike explicit registration, this wires up a default 'unnamed' implementation for the interface.
            if (ConcreteDataAccessObject == null)
                ConcreteDataAccessObject = Application.Container.GetDataAccessObject<T>();

            return (T)ConcreteDataAccessObject;
        }
    }
}