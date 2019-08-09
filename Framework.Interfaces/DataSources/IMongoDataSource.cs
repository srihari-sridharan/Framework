// <copyright file="IMongoDataSource.cs" company="">
//
// </copyright>

using System.Linq;

namespace Framework.Interfaces.DataSources
{
    /// <summary>
    ///     Mongo DB data source implementation.
    /// </summary>
    internal interface IMongoDataSource : IDataSource
    {
        /// <summary>
        ///     Exposes query for functional programming.
        /// </summary>
        /// <typeparam name="T">Type of the entity.</typeparam>
        /// <returns>Query interface.</returns>
        IQueryable<T> Query<T>();
    }
}