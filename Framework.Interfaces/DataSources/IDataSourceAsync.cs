using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Framework.Entities.Implementation;

namespace Framework.Interfaces.DataSources
{
    /// <summary>
    ///     Async variant of IDataSource.
    /// </summary>
    public interface IDataSourceAsync
    {
        /// <summary>
        ///     Deletes the entity with the specified identifier, asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>Number of rows affected.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification =
            "Framework design.")]
        Task<int> DeleteAsync<T>(int id) where T : BaseEntity, new();

        /// <summary>
        ///     Should return all rows in the table for the given context (such as language/domain etc.,)
        ///     This should put a cap on max results to avoid runaway queries.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <returns>List of entities</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification =
            "Framework design.")]
        Task<List<T>> GetAllAsync<T>() where T : BaseEntity, new();

        /// <summary>
        ///     Gets the entity by identifier, asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity</returns>
        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity, new();

        /// <summary>
        ///     Inserts the specified entity, asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">The t.</param>
        /// <returns>Number of rows affected.</returns>
        Task<int> InsertAsync<T>(T entity) where T : BaseEntity, new();

        /// <summary>
        ///     Inserts the list of entities, asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns>Number of rows affected.</returns>
        Task<int> InsertRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity, new();

        /// <summary>
        ///     Updates the specified entity, asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>Number of rows affected.</returns>
        Task<int> UpdateAsync<T>(T entity) where T : BaseEntity, new();
    }
}