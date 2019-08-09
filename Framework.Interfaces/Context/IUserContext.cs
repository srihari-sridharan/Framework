// <copyright file="IUserContext.cs" company="">
//
// </copyright>

using System;
using System.Collections.Generic;

namespace Framework.Interfaces.Context
{
    /// <summary>
    ///     This object provides all information about the current user.
    /// </summary>
    public interface IUserContext
    {
        /// <summary>
        ///     Gets the user name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; }

        /// <summary>
        ///     Gets the roles.
        /// </summary>
        /// <value>
        ///     The roles.
        /// </value>
        List<string> Roles { get; }

        /// <summary>
        ///     Gets the session expires at.
        /// </summary>
        /// <value>
        ///     The session expires at.
        /// </value>
        DateTime SessionExpiresAt { get; }

        /// <summary>
        ///     Determines whether [is in role] [the specified role name].
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>True if user plays a role.</returns>
        bool IsInRole(string roleName);

        /// <summary>
        ///     Should renew session by resetting expiresAt
        ///     Consider doing this via aspects
        /// </summary>
        void RenewSession();
    }
}