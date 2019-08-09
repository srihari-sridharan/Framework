// <copyright file="ServiceContext.cs" company="">
//
// </copyright>

using System.Collections.Generic;
using Framework.Interfaces.Context;

namespace Framework.Base.Context
{
    /// <summary>
    ///     Represents Service Context.
    /// </summary>
    public class ServiceContext : IServiceContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ServiceContext" /> class.
        /// </summary>
        public ServiceContext()
        {
            ServiceStack = new Stack<string>();
        }

        /// <summary>
        ///     Gets or sets the current level. This indicates call depth.
        /// </summary>
        /// <value>
        ///     The current level.
        /// </value>
        public int CurrentLevel { get; set; }

        /// <summary>
        ///     Gets or sets the name of the current service.
        /// </summary>
        /// <value>
        ///     The name of the current service.
        /// </value>
        public string CurrentServiceName { get; set; }

        /// <summary>
        ///     Gets or sets the error in service.
        /// </summary>
        /// <value>
        ///     The error in service.
        /// </value>
        public string ErrorInService { get; set; }

        /// <summary>
        ///     Gets or sets the error.
        /// </summary>
        /// <value>
        ///     The error.
        /// </value>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is in error.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is in error; otherwise, <c>false</c>.
        /// </value>
        public bool IsInError { get; set; }

        /// <summary>
        ///     Gets or sets the service stack.
        /// </summary>
        /// <value>
        ///     The service stack.
        /// </value>
        public Stack<string> ServiceStack { get; set; }

        /// <summary>
        ///     Gets or sets the transaction context.
        /// </summary>
        /// <value>
        ///     The transaction context.
        /// </value>
        public ITransactionContext TransactionContext { get; set; }
    }
}