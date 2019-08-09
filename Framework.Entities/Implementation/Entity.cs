// <copyright file="Entity.cs" company="">
//
// </copyright>

using Framework.Entities.Interfaces;

namespace Framework.Entities.Implementation
{
    /// <summary>
    ///     Represents a generic entity.
    /// </summary>
    /// <typeparam name="T">Type of identity column.</typeparam>
    public abstract class Entity<T> : IEntity<T>
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public virtual T Id { get; set; }
    }
}