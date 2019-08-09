// <copyright file="AuditableEntity.cs" company="">
//
// </copyright>

using System;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Entities.Interfaces;

namespace Framework.Entities.Implementation
{
    /// <summary>
    ///     Encapsulates the columns required for auditing for an entity.
    /// </summary>
    /// <typeparam name="T">Type of identity column.</typeparam>
    public abstract class AuditableEntity<T> : Entity<T>, IAuditable<T>
    {
        /// <summary>
        ///     Gets or sets the create date.
        /// </summary>
        /// <value>
        ///     The create date.
        /// </value>
        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     Gets or sets the modify date.
        /// </summary>
        /// <value>
        ///     The modify date.
        /// </value>
        [Column(TypeName = "datetime2")]
        public DateTime? ModifyDate { get; set; }
    }
}