namespace Framework.Interfaces.Context
{
    /// <summary>
    ///     Interface for the transaction context.
    /// </summary>
    public interface ITransactionContext
    {
        /// <summary>
        ///     Gets or sets the name of the service that transaction initiated the transaction.
        /// </summary>
        /// <value>
        ///     The name of the service that transaction initiated the transaction.
        /// </value>
        string TransactionInitiatedByServiceName { get; set; }

        /// <summary>
        ///     Gets or sets the transaction initiator level.
        /// </summary>
        /// <value>
        ///     The transaction initiator level.
        /// </value>
        int TransactionInitiatorLevel { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [transaction in progress].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [transaction in progress]; otherwise, <c>false</c>.
        /// </value>
        bool TransactionInProgress { get; set; }
    }
}