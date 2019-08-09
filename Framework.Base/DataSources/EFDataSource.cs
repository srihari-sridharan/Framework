// <copyright file="EFDataSource.cs" company="">
//
// </copyright>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Framework.Entities.Implementation;
using Framework.Interfaces.DataSources;

namespace Framework.Base.DataSources.Implementation
{
    /// <summary>
    ///     Use EntityDefinitionContext to instantiate the Ds with references to your project/ module
    /// </summary>
    /// <typeparam name="TEntityDefinitionsContext">Specialization of Database Context specific to the application.</typeparam>
    public class EFDataSource<TEntityDefinitionsContext>
        : IEFDataSource where TEntityDefinitionsContext : EFBaseDbContext
    {
        /// <summary>
        ///     Gets or sets the connection string.
        /// </summary>
        /// <value>
        ///     The connection string.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     Gets or sets the name of the database.
        /// </summary>
        /// <value>
        ///     The name of the database.
        /// </value>
        public string DatabaseName { get; set; }

        /// <summary>
        ///     Gets or sets the transaction provider.
        /// </summary>
        /// <value>
        ///     The transaction provider.
        /// </value>
        public ITransactionProvider TransactionProvider { get; set; }

        /// <summary>
        ///     Gets the database context.
        /// </summary>
        /// <value>
        ///     The database context.
        /// </value>
        protected TEntityDefinitionsContext DbContext
        {
            get
            {
                TEntityDefinitionsContext databaseContext = null;
                databaseContext = GetEFContextInstance(databaseContext);
                return databaseContext;
            }
        }

        /// <summary>
        ///     Deletes the entity with the specified identifier.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     Number of rows affected.
        /// </returns>
        public int Delete<T>(int id) where T : BaseEntity, new()
        {
            var entity = new T();
            entity.Id = id;
            entity = DbSet<T>().Attach(entity);
            DbSet<T>().Remove(entity);
            return DbContext.SaveChanges();
        }

        /// <summary>
        ///     Deletes the entity with the specified identifier, asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     Number of rows affected.
        /// </returns>
        public async Task<int> DeleteAsync<T>(int id) where T : BaseEntity, new()
        {
            var entity = new T();
            entity.Id = id;
            entity = DbSet<T>().Attach(entity);
            DbSet<T>().Remove(entity);
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Exposes the query instance to enable functional programming.
        /// </summary>
        /// <typeparam name="T">Type of the entity.</typeparam>
        /// <returns>
        ///     Instance of query interface.
        /// </returns>
        public IQueryable<T> ExecuteQuery<T>() where T : BaseEntity, new()
        {
            return DbSet<T>().AsQueryable();
        }

        /// <summary>
        ///     Should return all rows in the table for the given context (such as language/domain etc.,)
        ///     This should put a cap on max results to avoid runaway queries.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <returns>
        ///     List of entities
        /// </returns>
        public List<T> GetAll<T>() where T : BaseEntity, new()
        {
            return DbSet<T>().ToList();
        }

        /// <summary>
        ///     Should return all rows in the table for the given context (such as language/domain etc.,)
        ///     This should put a cap on max results to avoid runaway queries.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <returns>
        ///     List of entities
        /// </returns>
        public async Task<List<T>> GetAllAsync<T>() where T : BaseEntity, new()
        {
            return await DbSet<T>().ToListAsync();
        }

        /// <summary>
        ///     Gets the entity by identifier.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     The entity
        /// </returns>
        public T GetById<T>(int id) where T : BaseEntity, new()
        {
            return DbSet<T>().FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        ///     Gets the entity by identifier, asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///     The entity
        /// </returns>
        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity, new()
        {
            return await DbSet<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        ///     Inserts the specified entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>Number of rows affected.</returns>
        public int Insert<T>(T entity) where T : BaseEntity, new()
        {
            Add(entity);
            return DbContext.SaveChanges();
        }

        /// <summary>
        ///     Inserts the asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>Number of rows affected.</returns>
        public async Task<int> InsertAsync<T>(T entity) where T : BaseEntity, new()
        {
            Add(entity);
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Inserts the list of entities.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns> Number of rows affected.</returns>
        public int InsertRange<T>(IEnumerable<T> entities) where T : BaseEntity, new()
        {
            AddRange(entities);
            return DbContext.SaveChanges();
        }

        /// <summary>
        ///     Inserts the list of entities, asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entities">The entities.</param>
        /// <returns> Number of rows affected.</returns>
        public async Task<int> InsertRangeAsync<T>(IEnumerable<T> entities) where T : BaseEntity, new()
        {
            AddRange(entities);
            return await DbContext.SaveChangesAsync();
        }

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
        /// <exception cref="System.NotImplementedException">Not Implemented Exception.</exception>
        public void SetAuthentication<T>(bool doNotIncrementVersion) where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Indicates if this DS supports transactions
        /// </summary>
        /// <returns> True if supports transaction. </returns>
        /// <exception cref="System.NotImplementedException">Not Implemented Exception.</exception>
        public bool SupportsTransactions()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>Number of rows affected.</returns>
        public int Update<T>(T entity) where T : BaseEntity, new()
        {
            entity = DbSet<T>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            return DbContext.SaveChanges();
        }

        /// <summary>
        ///     Updates the asynchronous.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>Number of rows affected.</returns>
        public async Task<int> UpdateAsync<T>(T entity) where T : BaseEntity, new()
        {
            entity = DbSet<T>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <typeparam name="T">Type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        private void Add<T>(T entity) where T : BaseEntity, new()
        {
            DbSet<T>().Add(entity);
        }

        /// <summary>
        ///     Adds the range.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <param name="entities">The entities.</param>
        private void AddRange<T>(IEnumerable<T> entities) where T : BaseEntity, new()
        {
            DbSet<T>().AddRange(entities);
        }

        /// <summary>
        ///     Databases the set.
        /// </summary>
        /// <typeparam name="T">Type of entity.</typeparam>
        /// <returns>The DB Set.</returns>
        private DbSet<T> DbSet<T>() where T : BaseEntity, new()
        {
            return DbContext.Set<T>();
        }

        /// <summary>
        ///     Gets the EF context instance.
        /// </summary>
        /// <param name="databaseContext">The database context.</param>
        /// <returns>Database context.</returns>
        private TEntityDefinitionsContext GetEFContextInstance(TEntityDefinitionsContext databaseContext)
        {
            if (Application.Context.Properties.ContainsKey(DatabaseName))
                databaseContext =
                    (TEntityDefinitionsContext)Application.Context.Properties[DatabaseName];

            if (databaseContext == null)
            {
                // NOTE: This is threadsafe as the same thread cannot be instantiating
                // new databaseContext in 2 diff places at the same time
                // TODO: If this type EntityDefnitionsContext is in a different assembly.
                // We need to load it and then instantiate it
                databaseContext = (TEntityDefinitionsContext)Activator.CreateInstance(
                    typeof(TEntityDefinitionsContext),
                    ConnectionString);
                Application.Context.Properties.Add(DatabaseName, databaseContext);
            }

            return databaseContext;
        }
    }
}