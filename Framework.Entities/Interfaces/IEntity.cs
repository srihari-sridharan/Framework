namespace Framework.Entities.Interfaces
{
    /// <summary>
    ///     Interface for an entity.
    /// </summary>
    /// <typeparam name="T">Type of identity column.</typeparam>
    public interface IEntity<T>
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        T Id { get; set; }
    }
}