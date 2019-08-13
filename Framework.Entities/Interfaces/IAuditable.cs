using System;

namespace Framework.Entities.Interfaces
{
    /// <summary>
    ///     Interface for an auditable entity.
    /// </summary>
    /// <typeparam name="T">Type of identity column.</typeparam>
    public interface IAuditable<T> : IEntity<T>
    {
        /// <summary>
        ///     Gets or sets the create date.
        /// </summary>
        /// <value>
        ///     The create date.
        /// </value>
        DateTime CreateDate { get; set; }

        /// <summary>
        ///     Gets or sets the modify date.
        /// </summary>
        /// <value>
        ///     The modify date.
        /// </value>
        DateTime? ModifyDate { get; set; }
    }
}