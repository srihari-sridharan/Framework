// <copyright file="JournalableEntity.cs" company="">
//
// </copyright>

using Framework.Entities.Interfaces;

namespace Framework.Entities.Implementation
{
    /// <summary>
    ///     Represents an entity with journal. This is auditable.
    /// </summary>
    /// <typeparam name="T">Type of identity column.</typeparam>
    public abstract class JournalableEntity<T> : AuditableEntity<T>, IJournalable<T>
    {
        /// <summary>
        ///     Gets or sets the main entity identifier.
        /// </summary>
        /// <value>
        ///     The main entity identifier.
        /// </value>
        public virtual T MainEntityId { get; set; }
    }
}