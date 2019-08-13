using System.Collections.Generic;
using Framework.Interfaces.DataAccess;

namespace Framework.Interfaces.Services
{
    /// <summary>
    ///     Base interface for service.
    /// </summary>
    public interface IService
    {
        /// <summary>
        ///     Gets or sets the concrete data access object.
        /// </summary>
        /// <value>
        ///     The concrete data access object.
        /// </value>
        IDao ConcreteDataAccessObject { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating the name for the implementation.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets requires transaction.
        /// </summary>
        /// <value>
        ///     The requires transaction.
        /// </value>
        List<string> RequiresTransaction { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether exceptions should result in rollback
        ///     Applicable only  if RequiresTransaction is true
        ///     Defaults to true
        /// </summary>
        bool RollbackOnError { get; set; }
    }
}