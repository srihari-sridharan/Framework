// <copyright file="UserContext.cs" company="">
//
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Framework.Interfaces.Context;

namespace Framework.Base.Context
{
    /// <summary>
    ///     Represents User Context.
    /// </summary>
    public class UserContext : IUserContext
    {
        /// <summary>
        ///     Gets the user name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// <exception cref="System.NotImplementedException">Not Implemented Exception.</exception>
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification =
            "Forward facing requirement.")]
        public string Name => throw new NotImplementedException();

        /// <summary>
        ///     Gets the roles.
        /// </summary>
        /// <value>
        ///     The roles.
        /// </value>
        /// <exception cref="System.NotImplementedException">Not Implemented Exception.</exception>
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification =
            "Forward facing requirement.")]
        public List<string> Roles => throw new NotImplementedException();

        /// <summary>
        ///     Gets the session expires at.
        /// </summary>
        /// <value>
        ///     The session expires at.
        /// </value>
        /// <exception cref="System.NotImplementedException">Not Implemented Exception.</exception>
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations", Justification =
            "Forward facing requirement.")]
        public DateTime SessionExpiresAt => throw new NotImplementedException();

        /// <summary>
        ///     Determines whether [is in role] [the specified role name].
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>
        ///     True if user plays a role.
        /// </returns>
        /// <exception cref="System.NotImplementedException">Not Implemented Exception.</exception>
        public bool IsInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Should renew session by resetting expiresAt
        ///     Consider doing this via aspects
        /// </summary>
        /// <exception cref="System.NotImplementedException">Not Implemented Exception.</exception>
        public void RenewSession()
        {
            throw new NotImplementedException();
        }
    }
}