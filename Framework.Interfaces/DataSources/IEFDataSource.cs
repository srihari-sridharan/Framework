using System.Linq;
using Framework.Entities.Implementation;

namespace Framework.Interfaces.DataSources
{
    /// <summary>
    ///     Interface for Entity Framework data source.
    /// </summary>
    public interface IEFDataSource : IDataSource, IDataSourceAsync
    {
        /// <summary>
        ///     Gets or sets the connection string.
        /// </summary>
        /// <value>
        ///     The connection string.
        /// </value>
        string ConnectionString { get; set; }

        /// <summary>
        ///     Exposes the query instance to enable functional programming.
        /// </summary>
        /// <typeparam name="T">Type of the entity.</typeparam>
        /// <returns>Instance of query interface.</returns>
        IQueryable<T> ExecuteQuery<T>() where T : BaseEntity, new();
    }
}