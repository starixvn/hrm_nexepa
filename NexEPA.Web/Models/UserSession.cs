// --------------------------------------------------------------------------
// <copyright file="UserSession.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.28">Created</change>
// </history>

namespace NexEPA.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Current Session.
    /// </summary>
    [Serializable]
    public class UserSession
    {
        public int UserId { get; set; }

        public string Code { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public int GroupId { get; set; }

        public int EmployeeLevelId { get; set; }

        public IList<UserRole> Roles { get; set; }

        public bool IsDisabled { get; set; }

        public bool HasRole(short roleId)
        {
            var hasRole = Roles.Any(t => t.RoleId == roleId);
            return hasRole;
        }

        /// <summary>
        /// Check user is in specific roles.
        /// </summary>
        /// <param name="roles">Role of user.</param>
        /// <returns>Return Boolean.</returns>
        public bool IsInRoles(IList<string> roles)
        {
            if (roles == null || roles.Count == 0)
            {
                return false;
            }

            var userRoles = System.Web.Security.Roles.GetRolesForUser();
            return roles.Any(role => userRoles.Contains(role));
        }
    }
}