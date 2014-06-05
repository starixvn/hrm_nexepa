// -----------------------------------------------------------------------
// <copyright file="AuthorityService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.04">Created</change>
// </history>

namespace NexEPA.Authority.Services.Implement
{
    using System.Collections.Generic;
    using Authority.Criteria;
    using Authority.Models;
    using Authority.Services.Interface;
    using Fanex.Common.Helper;
    using NexEPA.Core.Services;

    /// <summary>
    /// The implement of Authority service.
    /// </summary>
    public class AuthorityService : BaseService, IAuthorityService
    {
        /// <summary>
        /// Get all function.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The enumerator Function Model.</returns>
        public IEnumerable<FunctionModel> GetAllFunction(GetAllFunctionCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<FunctionModel> functions = null;

            if (criteria.IsValid())
            {
                functions = GetData<FunctionModel>(criteria);
            }

            return functions;
        }

        /// <summary>
        /// Get all role system.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>THe enumerator Role System Model.</returns>
        public IEnumerable<RoleSystemModel> GetAllSystemRole(GetAllSystemRoleCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<RoleSystemModel> roles = null;

            if (criteria.IsValid())
            {
                roles = GetData<RoleSystemModel>(criteria);
            }

            return roles;
        }
    }
}
