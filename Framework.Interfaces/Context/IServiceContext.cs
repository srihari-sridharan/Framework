using System.Collections.Generic;

namespace Framework.Interfaces.Context
{
    /// <summary>
    ///     Interface for service context.
    /// </summary>
    public interface IServiceContext
    {
        /// <summary>
        ///     Gets or sets the current level. This indicates call depth.
        /// </summary>
        /// <value>
        ///     The current level.
        /// </value>
        int CurrentLevel { get; set; }

        /// <summary>
        ///     Gets or sets the name of the current service.
        /// </summary>
        /// <value>
        ///     The name of the current service.
        /// </value>
        string CurrentServiceName { get; set; }

        /// <summary>
        ///     Gets or sets the error in service.
        /// </summary>
        /// <value>
        ///     The error in service.
        /// </value>
        string ErrorInService { get; set; }

        /// <summary>
        ///     Gets or sets the error.
        /// </summary>
        /// <value>
        ///     The error.
        /// </value>
        string ErrorMessage { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is in error.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is in error; otherwise, <c>false</c>.
        /// </value>
        bool IsInError { get; set; }

        /// <summary>
        ///     Gets or sets the service stack.
        /// </summary>
        /// <value>
        ///     The service stack.
        /// </value>
        Stack<string> ServiceStack { get; set; }

        /// <summary>
        ///     Gets or sets the transaction context.
        /// </summary>
        /// <value>
        ///     The transaction context.
        /// </value>
        ITransactionContext TransactionContext { get; set; }
    }
}