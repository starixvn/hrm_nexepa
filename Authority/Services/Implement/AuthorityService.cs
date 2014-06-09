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
        public IList<FunctionGroupModel> GetFunctionByRole(GetFunctionByRoleCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IList<FunctionGroupModel> functionGroup = new List<FunctionGroupModel>();

            if (criteria.IsValid())
            {
                IEnumerable<FunctionModel> rawFunctions = GetData<FunctionModel>(criteria);
                IEnumerable<FunctionModel> rawParentFunctions = rawFunctions.Where<FunctionModel>(func => func.ParentFunctionId == null);

                foreach (FunctionModel rawParentFunctionItem in rawParentFunctions)
                {
                    var parentFunction = new FunctionGroupModel
                    {
                        FunctionId = rawParentFunctionItem.FunctionId,
                        FunctionKey = rawParentFunctionItem.FunctionKey,
                        Name = rawParentFunctionItem.Name,
                        IsReadOnly = rawParentFunctionItem.IsReadOnly,
                        IsRead = rawParentFunctionItem.IsRead,
                        IsHost = rawParentFunctionItem.IsHost
                    };
                    List<FunctionModel> rawChildFunctions = rawFunctions.Where(func => func.ParentFunctionId == rawParentFunctionItem.FunctionId).ToList();

                    if (rawChildFunctions != null && rawChildFunctions.Count > 0)
                    {
                        parentFunction.ChildFunctions = rawChildFunctions;
                    }

                    functionGroup.Add(parentFunction);
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
    }
}
