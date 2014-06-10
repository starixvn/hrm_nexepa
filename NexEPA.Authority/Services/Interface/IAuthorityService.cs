// -----------------------------------------------------------------------
// <copyright file="IAuthorityService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.06.04">Created</change>
// </history>

namespace NexEPA.Authority.Services.Interface
{
    using System.Collections.Generic;
    using Authority.Criteria;
    using Authority.Models;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The interface of Authority service.
    /// </summary>
    public interface IAuthorityService
    {
        IEnumerable<FunctionModel> GetFunctionByRole(GetFunctionByRoleCriteria criteria);

        IEnumerable<RoleSystemModel> GetAllSystemRole(GetAllSystemRoleCriteria criteria);

        CommandResult AssignPermissionToRole(AssignPermissionToRoleCriteria criteria);
    }
}
