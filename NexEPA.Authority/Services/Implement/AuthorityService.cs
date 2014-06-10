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
    using System.Linq;
    using Authority.Criteria;
    using Authority.Models;
    using Authority.Services.Interface;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
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
        public IEnumerable<FunctionModel> GetFunctionByRole(GetFunctionByRoleCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<FunctionModel> functionGroup = null;

            if (criteria.IsValid())
            {
                IEnumerable<FunctionModel> rawFunctions = GetData<FunctionModel>(criteria);
                functionGroup = rawFunctions.Where<FunctionModel>(func => func.ParentFunctionId == null);

                foreach (FunctionModel rawParentFunctionItem in functionGroup)
                {
                    rawParentFunctionItem.ChildFunctions = GetFunctionByRoleRecursion(rawFunctions, rawParentFunctionItem);
                }
            }

            return functionGroup;
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

        /// <summary>
        /// Assign permission to role.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult AssignPermissionToRole(AssignPermissionToRoleCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = _Repository.Insert(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Get function by role recursion.
        /// </summary>
        /// <param name="rawFunctions">The all functions.</param>
        /// <param name="rawParentFunctionItem">The current parent function.</param>
        /// <returns>The list Function Model.</returns>
        private List<FunctionModel> GetFunctionByRoleRecursion(
            IEnumerable<FunctionModel> rawFunctions,
            FunctionModel rawParentFunctionItem)
        {
            List<FunctionModel> rawChildFunctions = rawFunctions.Where(func => func.ParentFunctionId == rawParentFunctionItem.FunctionId).ToList();

            if (rawChildFunctions != null && rawChildFunctions.Count() > 0)
            {
                foreach (var rawChildFunctionItem in rawChildFunctions)
                {
                    rawChildFunctionItem.ChildFunctions = GetFunctionByRoleRecursion(rawFunctions, rawChildFunctionItem);
                }
            }
            else
            {
                return null;
            }

            return rawChildFunctions;
        }
    }
}
