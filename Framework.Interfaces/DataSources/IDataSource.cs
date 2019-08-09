// <copyright file="IDataSource.cs" company="">
//
// </copyright>

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Framework.Entities.Implementation;

namespace Framework.Interfaces.DataSources
{
    /// <summary>
    ///     Interface for data source operations.
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        ///     Gets or sets the transaction provider.
        /// </summary>
        /// <value>
        ///     The transaction provider.
        /// </value>
        ITransactionProvider TransactionProvider { get; set; }

        /// <summary>
        ///     Deletes the entity with the specified identifier.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>Number of rows affected.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification =
            "Framework design.")]
        int Delete<T>(int id) where T : BaseEntity, new();

        /// <summary>
        ///     Should return all rows in the table for the given context (such as language/domain etc.,)
        ///     This should put a cap on max results to avoid runaway queries.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <returns>List of entities</returns>
        List<T> GetAll<T>() where T : BaseEntity, new();

        /// <summary>
        ///     Gets the entity by identifier.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity</returns>
        T GetById<T>(int id) where T : BaseEntity, new();

        /// <summary>
        ///     Inserts the specified entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">The t.</param>
        /// <returns>Number of rows affected.</returns>
        int Insert<T>(T entity) where T : BaseEntity, new();

        /// <summary>
        ///     Inserts the list of entities.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns>Number of rows affected.</returns>
        int InsertRange<T>(IEnumerable<T> entities) where T : BaseEntity, new();

        /// <summary>
        ///     This method is invoked when a persistence operation is to be done
        ///     This method shall set the audit columns such as
        ///     author, last updated, version# etc., This will increment
        ///     version# by default for optimistic locking. However,
        ///     this can be overridden depending on DataSource's requirements.
        ///     The override can call the base SetAuthentication with true as argument
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="doNotIncrementVersion">if set to <c>true</c> [do not increment version].</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification =
            "Framework design.")]
        void SetAuthentication<T>(bool doNotIncrementVersion) where T : BaseEntity, new();

        /// <summary>
        ///     Indicates if this DS supports transactions
        /// </summary>
        /// <returns>True if supports transaction.</returns>
        bool SupportsTransactions();

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>Number of rows affected.</returns>
        int Update<T>(T entity) where T : BaseEntity, new();
    }
}