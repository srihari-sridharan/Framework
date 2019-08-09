// <copyright file="BaseDao.cs" company="">
//
// </copyright>

using System.Collections.Generic;
using Framework.Entities.Interfaces;
using Framework.Interfaces.DataAccess;
using Framework.Interfaces.DataSources;

namespace Framework.Base.DataAccess
{
    /// <summary>
    ///     Represents the base DAO which acts as the base class for any Data Access Object.
    /// </summary>
    public abstract class BaseDao : BaseObject, IDao
    {
        /// <summary>
        ///     Gets or sets the arguments.
        /// </summary>
        /// <value>
        ///     The arguments.
        /// </value>
        protected Dictionary<string, object> Args { get; set; }

        /// <summary>
        ///     Check if duplicate entity exist. This should be implemented by every Dao.
        /// </summary>
        /// <param name="baseEntity">The base entity.</param>
        /// <returns>True if duplicate exists.</returns>
        public abstract bool DoesDuplicateExist(IBaseEntity baseEntity);

        /// <summary>
        ///     Gets the registered named data source.
        /// </summary>
        /// <typeparam name="T">Type of data source.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>The named data source.</returns>
        protected T GetDS<T>(string name) where T : IDataSource
        {
            return (T)Application.Container.GetDataSource(name);
        }
    }
}