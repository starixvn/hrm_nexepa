// --------------------------------------------------------------------------
// <copyright file="CustomRoleProvider.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.31">Created</change>
// </history>

namespace NexEPA.Web.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Security;
    using NexEPA.Core.Models;

    /// <summary>
    /// CustomRole Provider.
    /// </summary>
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                return "NexEPA";
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get roles for user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>List of role.</returns>
        public override string[] GetRolesForUser(string username)
        {
            var user = GetCurrentUser(username);
            var roles = new List<string>();

            if (user != null && user.Roles != null)
            {
                foreach (var item in user.Roles)
                {
                    roles.Add(item.Name);
                }
            }

            return roles.ToArray();
        }

        private UserSession GetCurrentUser(string username)
        {
            return SessionManager.CurrentUser;
        }
    }
}
