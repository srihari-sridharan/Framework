namespace Framework.Interfaces.DataSources
{
    /// <summary>
    ///     This is an optional implementation that can be implemented when
    ///     the provider is not natively supporting MSDTC.
    /// </summary>
    public interface ITransactionProvider
    {
        /// <summary>
        ///     Gets or sets a reverse reference to the parent data source for which
        ///     this is the transaction provider.
        /// </summary>
        IDataSource DataSource { get; set; }

        /// <summary>
        ///     Commits this instance.
        /// </summary>
        /// <returns>True on success.</returns>
        bool Commit();

        /// <summary>
        ///     Enrolls the current data source in a transaction.
        /// </summary>
        /// <returns>True on success.</returns>
        bool Enlist();

        /// <summary>
        ///     Determines whether this instance is completed.
        /// </summary>
        /// <returns>True if completed.</returns>
        bool IsCompleted();

        /// <summary>
        ///     Determines whether this instance is enrolled.
        /// </summary>
        /// <returns>True on success.</returns>
        bool IsEnrolled();

        /// <summary>
        ///     Determines whether [is in error].
        /// </summary>
        /// <returns>True on error.</returns>
        bool IsInError();

        /// <summary>
        ///     Rollbacks this instance.
        /// </summary>
        /// <returns>True on rollback.</returns>
        bool Rollback();
    }
}