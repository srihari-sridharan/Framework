namespace Framework.Interfaces.DataAccess
{
    /// <summary>
    ///     Interface for mapping data from one entity to another.
    /// </summary>
    public interface IDataMapper
    {
        /// <summary>
        ///     Maps an entity to another.
        /// </summary>
        /// <typeparam name="TFrom">The type of the from entity.</typeparam>
        /// <typeparam name="TTo">The type of the to entity.</typeparam>
        /// <param name="from">Instance of 'From' entity.</param>
        /// <returns>Instance of 'To' entity</returns>
        TTo Map<TFrom, TTo>(TFrom from);
    }
}