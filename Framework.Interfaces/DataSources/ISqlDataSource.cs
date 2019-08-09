// <copyright file="ISqlDataSource.cs" company="">
//
// </copyright>

using System.Collections.Generic;
using System.Data.SqlClient;

namespace Framework.Interfaces.DataSources
{
    /// <summary>
    ///     Interface for Sql data source.
    /// </summary>
    public interface ISqlDataSource : IDataSource
    {
        /// <summary>
        ///     Gets the connection.
        /// </summary>
        /// <value>
        ///     The connection.
        /// </value>
        SqlConnection Connection { get; }

        /// <summary>
        ///     Sets the connection string.
        /// </summary>
        /// <value>
        ///     The connection string.
        /// </value>
        string ConnectionString { set; }

        /// <summary>
        ///     Executes the query and returns a data reader.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>Data reader.</returns>
        SqlDataReader ExecuteQuery(string statement, Dictionary<string, object> arguments);
    }
}