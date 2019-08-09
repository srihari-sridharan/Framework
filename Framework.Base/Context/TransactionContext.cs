// <copyright file="TransactionContext.cs" company="">
//
// </copyright>

using Framework.Interfaces.Context;

namespace Framework.Base.Context
{
    /// <summary>
    ///     Represents Transaction Context.
    /// </summary>
    public class TransactionContext : ITransactionContext
    {
        /// <summary>
        ///     Gets or sets the name of the service that transaction initiated the transaction.
        /// </summary>
        /// <value>
        ///     The name of the service that transaction initiated the transaction.
        /// </value>
        public string TransactionInitiatedByServiceName { get; set; }

        /// <summary>
        ///     Gets or sets the transaction initiator level.
        /// </summary>
        /// <value>
        ///     The transaction initiator level.
        /// </value>
        public int TransactionInitiatorLevel { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [transaction in progress].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [transaction in progress]; otherwise, <c>false</c>.
        /// </value>
        public bool TransactionInProgress { get; set; }
    }
}