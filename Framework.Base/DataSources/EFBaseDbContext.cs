using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace Framework.Base.DataSources
{
    /// <summary>
    ///     Base class for any EF Database Context.
    /// </summary>
    public class EFBaseDbContext : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EFBaseDbContext" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public EFBaseDbContext(string connectionString)
            : base(connectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            (this as IObjectContextAdapter).ObjectContext.CommandTimeout = 120;
        }

        /// <summary>
        ///     Gets the <see cref="DbSet" /> with the specified type.
        /// </summary>
        /// <value>
        ///     The <see cref="DbSet" />.
        /// </value>
        /// <param name="type">The type.</param>
        /// <returns>The DB Set.</returns>
        [SuppressMessage("Microsoft.Design", "CA1043:UseIntegralOrStringArgumentForIndexers", Justification =
            "Framework design.")]
        public DbSet this[Type type] => Set(type);

        /// <summary>
        ///     This method is called when the model for a derived context has been initialized, but
        ///     before the model has been locked down and used to initialize the context.  The default
        ///     implementation of this method does nothing, but it can be overridden in a derived class
        ///     such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        ///     Typically, this method is called only once when the first instance of a derived context
        ///     is created.  The model for that context is then cached and is for all further instances of
        ///     the context in the app domain.  This caching can be disabled by setting the ModelCaching
        ///     property on the given ModelBuilder, but note that this can seriously degrade performance.
        ///     More control over caching is provided through use of the DB ModelBuilder and DB ContextFactory
        ///     classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Declare your entities here.
            base.OnModelCreating(modelBuilder);
        }
    }
}